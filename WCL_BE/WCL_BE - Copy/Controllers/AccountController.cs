using Microsoft.AspNetCore.Mvc;
using WCL_BE.Processors;
using Newtonsoft.Json;
using static WCL_BE.Model.APIRequests;
using static WCL_BE.Model.APIResponses;
using WCL_BE.Security;
using static WCL_BE.Helpers.APIHelper;
namespace WCL_BE.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {

        private AccountProcessor _proc;
        private Encryptor _security;

        public AccountController(IConfiguration config)
        {
            _proc = new AccountProcessor(config);
            _security = new(config);
        }

        [HttpPost]
        public string Login(LoginRequest req)
        {
            GenericResponse ret = _proc.Login(req.Email, req.Password);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);
        }

        [HttpPost]
        public string Signup(NewAccountRequest req)
        {
            GenericResponse ret = _proc.SignUp(req.Email, req.DisplayName, req.Password, req.GymName, req.Country, req.City);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);
        }

        [HttpPost]
        public string GetGymForToken(TokenOnlyRequest req)
        {
            long accountId = _security.CheckToken(req.Token);
            if (accountId < 1)
            {
                return JsonConvert.SerializeObject(new GenericResponse() { Result = false, ErrorMessage = TOKEN_ERROR });
            }
            GenericResponse ret = _proc.GetGymFromAccount(accountId);
            return JsonConvert.SerializeObject(ret, Formatting.Indented);

        }
    }
}
