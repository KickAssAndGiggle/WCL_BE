using System.Collections.Generic;
using WCL_BE.Connectivity;

namespace WCL_BE.Managers
{
    public class FighterManager
    {

        private const long BACKGROUND_NONE = 1;
        private const long BACKGROUND_BOXING = 2;
        private const long BACKGROUND_KICKBOXING = 3;
        private const long BACKGROUND_KARATE = 4;
        private const long BACKGROUND_WRESTLING = 5;
        private const long BACKGROUND_JUJITSU = 6;
        private const long BACKGROUND_JUDO = 7;
        private const long BACKGROUND_MUAYTHAI = 8;

        private IConfiguration _config;
        private WCLDB _db;
        private Random _rnd;

        public FighterManager(IConfiguration config) 
        { 
            _config = config;
            _db = new(config.GetSection("ConnectionStrings:WCLDB").Get<string>()!, false);
            _rnd = new Random(Environment.TickCount);
        }

        public void CreateFighter(long? gymId)
        {

            LocationSelector(gymId, out long country, out long city);
            int age;
            if (gymId != null)
            {
                // Gym specific prospects are youngsters
                age = _rnd.Next(18, 24);
            }
            else
            {
                // A non-gym specific prospect could be a veteran OR a youngster
                age = _rnd.Next(18, 34);
            }

            int physicalModifier = _rnd.Next(0, 10);

            // Chin
            int chin = _rnd.Next(1, 100);
            // Heart
            int heart = _rnd.Next(1, 100);
            
            // Strength
            int strength = _rnd.Next(1, 40) + physicalModifier;
            if (age > 25)
            {
                if (FiftyFifty())
                {
                    strength += physicalModifier;
                }
            }

            // Agility
            int agility = _rnd.Next(1, 40) + physicalModifier;
            if (age <= 25)
            {
                if (FiftyFifty())
                {
                    agility += physicalModifier;
                }
            }

            // Stamina
            int stamina = _rnd.Next(1, 50) + physicalModifier;

            // Background
            int diceroll = _rnd.Next(1, 9);
            long background = Convert.ToInt64(diceroll);

            // Punching stats
            int jabs = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING || background == BACKGROUND_KICKBOXING || 
                background == BACKGROUND_KARATE || background == BACKGROUND_MUAYTHAI)
            {
                jabs += _rnd.Next(0, 20);
            }

            int crosses = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING || background == BACKGROUND_KICKBOXING ||
                background == BACKGROUND_KARATE || background == BACKGROUND_MUAYTHAI)
            {
                crosses += _rnd.Next(0, 20);
            }

            int hooks = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING || background == BACKGROUND_KICKBOXING)
            {
                hooks += _rnd.Next(0, 20);
            }

            int uppercuts = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING)
            {
                uppercuts += _rnd.Next(0, 20);
            }

            // Kicking stats
            int legKicks = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI || background == BACKGROUND_KICKBOXING ||  background == BACKGROUND_KARATE)
            {
                legKicks += (_rnd.Next(0, 20));
            }

            int bodyKicks = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI || background == BACKGROUND_KICKBOXING || background == BACKGROUND_KARATE)
            {
                bodyKicks += (_rnd.Next(0, 20));
            }

            int headKicks = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI || background == BACKGROUND_KICKBOXING || background == BACKGROUND_KARATE)
            {
                headKicks += (_rnd.Next(0, 20));
            }

            // Specialist strikes
            int backFists = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI)
            {
                backFists += _rnd.Next(0, 20);
            }

            int elbows = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI)
            {
                elbows += _rnd.Next(0, 20);
            }

            int kneeStrikes = _rnd.Next(1, 40);
            if (background == BACKGROUND_MUAYTHAI)
            {
                kneeStrikes += _rnd.Next(0, 20);
            }

            // Grappling/defence skills
            int takedowns = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU || background == BACKGROUND_WRESTLING)
            {
                takedowns += _rnd.Next(0, 20);
            }

            int clinch = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_BOXING || background == BACKGROUND_WRESTLING)
            {
                clinch += _rnd.Next(1, 20);
            }

            int takedownDefence = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU || background == BACKGROUND_WRESTLING)
            {
                takedownDefence += _rnd.Next(1, 40);
            }

            int headMovement = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING || background == BACKGROUND_KICKBOXING)
            {
                headMovement += _rnd.Next(1, 40);
            }

            int footwork = _rnd.Next(1, 40);
            if (background == BACKGROUND_BOXING || background == BACKGROUND_KARATE || background == BACKGROUND_KICKBOXING ||
                background == BACKGROUND_MUAYTHAI)
            {
                footwork += _rnd.Next(0, 20);
            }

            int wrestling = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU || background == BACKGROUND_WRESTLING)
            {
                wrestling += _rnd.Next(0, 20);
            }

            int groundGuard = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU || background == BACKGROUND_WRESTLING)
            {
                groundGuard += _rnd.Next(0, 20);
            }

            // Submissions
            int chokes = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU)
            {
                chokes += _rnd.Next(0, 20);
            }

            int armbars = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUDO || background == BACKGROUND_JUJITSU)
            {
                chokes += _rnd.Next(0, 20);
            }

            int legLocks = _rnd.Next(1, 40);
            if (background == BACKGROUND_JUJITSU)
            {
                legLocks += _rnd.Next(0, 20);
            }

            // ###TODO - generate a sensible name for the country
            _db.CreateFighter(gymId, country, city, background, "Jim", "Jackson", age, chin, heart, strength, agility,
                stamina, jabs, crosses, hooks, uppercuts, legKicks, bodyKicks, headKicks, backFists, elbows, kneeStrikes,
                takedowns, clinch, takedownDefence, headMovement, footwork, wrestling, groundGuard, chokes, armbars, legLocks);

        }


        private bool FiftyFifty()
        {
            if (_rnd.Next(0, 100) < 50)
            {
                return true;
            }
            return false;
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
