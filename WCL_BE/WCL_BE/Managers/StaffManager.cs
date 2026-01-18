using WCL_BE.Connectivity;

namespace WCL_BE.Managers
{
    public class StaffManager
    {
        private IConfiguration _config;
        private WCLDB _db;
        private Random _rnd;

        public StaffManager(IConfiguration config)
        {
            _config = config;
            _db = new(config.GetSection("ConnectionStrings:WCLDB").Get<string>()!, false);
            _rnd = new Random(Environment.TickCount);
        }
        public void CreateStaff(long? gymId)
        {
            LocationSelector(gymId, out long country, out long city);
            int judgingAbility = _rnd.Next(1, 100);
            int fitnessCoaching = _rnd.Next(1, 100);
            int boxingCoaching = _rnd.Next(1, 100);
            int wrestlingCoaching = _rnd.Next(1, 100);
            int kickboxingCoaching = _rnd.Next(1, 100);
            int submissionCoaching = _rnd.Next(1, 100);
            int proffesionalism = _rnd.Next(1, 100);
            //staff creation age is anywhere between 18-30. rare to have staff under the age of 25.
            int age = _rnd.Next(18,30);
            if(age < 25)
            {
                age += _rnd.Next(0, 10);
            }
            _db.CreateStaff(gymId, country, city, judgingAbility, fitnessCoaching,
                boxingCoaching, wrestlingCoaching, kickboxingCoaching, submissionCoaching, proffesionalism, age,"Adam","Wiggins");
        }
        private void LocationSelector(long? gymId, out long countryId, out long cityId)
        {
            if (gymId == null)
            {
                // ###TODO: pick randomly
                countryId = 1;
                cityId = 1;
            }
            else
            {
                countryId = _db.GetGymCountry(gymId.Value);
                cityId = _db.GetGymCity(gymId.Value);
            }
        }

    }
}
