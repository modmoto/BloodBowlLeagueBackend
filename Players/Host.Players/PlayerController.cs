using System;
using System.Threading.Tasks;
using Application.Players;
using Microsoft.AspNetCore.Mvc;

namespace Host.Players
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly PlayerCommandHandler _commandHandler;

        public PlayerController(PlayerCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("{playerId}/choose-skill")]
        public async Task<ActionResult> LevelUpPlayer(
            Guid playerId,
            [FromBody] LevelUpPlayerComand levelUpCommand)
        {
            await _commandHandler.ChooseSkill(playerId, levelUpCommand);
            return Ok();
        }

        [HttpPost("{playerId}/register-skill")]
        public async Task<ActionResult> RegisterFreeSkillPoint(
            Guid playerId,
            [FromBody] RegisterLevelUpSkillPointRollCommand command)
        {
            await _commandHandler.RegisterLevelUpSkillPointRoll(playerId, command);
            return Ok();
        }
    }
}