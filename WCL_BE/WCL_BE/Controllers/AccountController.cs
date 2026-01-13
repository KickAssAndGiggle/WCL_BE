using Microsoft.AspNetCore.Mvc;
using WCL_BE.Processors;
using Newtonsoft.Json;
using static WCL_BE.Model.APIRequests;
using static WCL_BE.Model.APIResponses;
namespace WCL_BE.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {

        private AccountProcessor _proc;

        public AccountController(IConfiguration config)
        {
            _proc = new AccountProcessor(config);
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
    }
}
