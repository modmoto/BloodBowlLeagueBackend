using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Users.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public HomeController(
            IIdentityServerInteractionService interaction,
            IEventService events,
            TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(TestUsers.Users);

            _interaction = interaction;
            _events = events;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model, string button)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (button == "cancel")
            {
                return Redirect(model.ReturnUrl);
            }

            if (ModelState.IsValid)
            {
                if (_users.ValidateCredentials(model.Username, model.Password))
                {
                    var user = _users.FindByUsername(model.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username, clientId: context?.ClientId));

                    var isuser = new IdentityServerUser(user.SubjectId)
                    {
                        DisplayName = user.Username
                    };

                    await HttpContext.SignInAsync(isuser);

                    return Redirect(model.ReturnUrl);
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId:context?.ClientId));
            }

            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            return vm;
        }
    }
}