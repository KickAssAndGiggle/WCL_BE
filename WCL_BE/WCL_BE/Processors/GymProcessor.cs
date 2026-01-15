using WCL_BE.Connectivity;

namespace WCL_BE.Processors
{
    public class GymProcessor
    {

        private IConfiguration _config;
        private WCLDB _db;

        public GymProcessor(IConfiguration config)
        {
            _config = config;
            _db = new(config.GetSection("ConnectionStrings:WCLDB").Get<string>()!, false);
        }

    }
}
