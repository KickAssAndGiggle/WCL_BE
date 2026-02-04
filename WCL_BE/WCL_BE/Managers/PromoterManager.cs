using WCL_BE.Connectivity;

namespace WCL_BE.Managers
{
    public class PromoterManager
    {
        private IConfiguration _config;
        private WCLDB _db;
        private Random _rnd;
        public PromoterManager(IConfiguration config)
        {
            _config = config;
            _db = new(config.GetSection("ConnectionStrings:WCLDB").Get<string>()!, false);
            _rnd = new Random(Environment.TickCount);
        }
        public void GenerateNewEvent(long promoterId)
        {
            string name = "big event";
            DateTime signUpDate = DateTime.Today.AddDays(1);
            DateTime fightNightDate = DateTime.Today.AddDays(2);
            _db.GenerateNewEvent(promoterId, name, signUpDate, fightNightDate);
        }
    }
}
