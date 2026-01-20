using Microsoft.AspNetCore.Mvc;
using WCL_BE.Processors;
using Newtonsoft.Json;
using static WCL_BE.Model.APIRequests;
using static WCL_BE.Model.APIResponses;
using WCL_BE.Security;
using static WCL_BE.Helpers.APIHelper;
using WCL_BE.Managers;
using Azure.Core;
namespace WCL_BE.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GymController : ControllerBase
    {

        private GymProcessor _proc;
        private Encryptor _security;

        public GymController(IConfiguration config) 
        {
            _proc = new GymProcessor(config);
            _security = new(config);
        }

        [HttpPost]
        public string GetProspects(TokenOnlyRequest req)
        {
            long accountId = _security.CheckToken(req.Token);
            if (accountId < 1)
            {
                return JsonConvert.SerializeObject(new GenericResponse() { Result = false, ErrorMessage = TOKEN_ERROR });
            }
            GenericResponse ret = _proc.GetPropsects(accountId);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);

        }

        [HttpPost]
        public string InviteToGym(IdOnlyRequest req)
        {
            long accountId = _security.CheckToken(req.Token);
            if (accountId < 1)
            {
                return JsonConvert.SerializeObject(new GenericResponse() { Result = false, ErrorMessage = TOKEN_ERROR });
            }
            GenericResponse ret = _proc.AcceptProspectToGym(req.Id, accountId);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);
        }
        [HttpPost]
        public string GetUnemployedStaff(TokenOnlyRequest req)
        {
            long accountId = _security.CheckToken(req.Token);
            if (accountId < 1)
            {
                return JsonConvert.SerializeObject(new GenericResponse() { Result = false, ErrorMessage = TOKEN_ERROR });
            }
            GenericResponse ret = _proc.GetUnemployedStaff(accountId);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);
        }

    }
}
