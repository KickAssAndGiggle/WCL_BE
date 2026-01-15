using Microsoft.AspNetCore.Mvc;
using WCL_BE.Processors;
using Newtonsoft.Json;
using static WCL_BE.Model.APIRequests;
using static WCL_BE.Model.APIResponses;
namespace WCL_BE.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GymController : ControllerBase
    {

        private GymProcessor _proc;

        public GymController(IConfiguration config) 
        {
            _proc = new GymProcessor(config);
        }


    }
}
