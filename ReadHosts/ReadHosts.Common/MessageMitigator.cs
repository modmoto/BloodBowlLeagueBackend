using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microwave.Domain;
using Microwave.Domain.Validation;
using Microwave.WebApi.Filters;
using Newtonsoft.Json;

namespace ReadHosts.Common
{
    public class MessageMitigator
    {
        public async Task PostAsync(Uri requestUri, object ob)
        {
            var httpClient = new HttpClient();
            var teamObject = JsonConvert.SerializeObject(ob);
            var content = new StringContent(teamObject, Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(requestUri, content);
            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                var problemDocument = JsonConvert.DeserializeObject<ProblemDocument>(body);
                throw new DomainValidationException(problemDocument.DomainErrors);
            }
        }
    }
}