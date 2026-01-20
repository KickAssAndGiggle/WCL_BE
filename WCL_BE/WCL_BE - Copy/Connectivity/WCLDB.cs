using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace WCL_BE.Connectivity
{
    public class WCLDB
    {

        private DatabaseConnectivity _conn;

        public WCLDB(string connectionString, bool keepAlive) 
        {
            _conn = new(connectionString, keepAlive);
        }

        public long TokenValid(string token)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputString("@Token", token, 50));
            return _conn.ExecuteStoredProcedureAsScalarLong("account.CheckToken", sqlParams.ToArray());
        }

        public void AddAccessToken(long accountId, string token)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@AccountId", accountId));
            sqlParams.Add(_conn.GenerateInputString("@Token", token, 50));
            _conn.ExecuteStoredProcedureNoReturn("account.AddAccessToken", sqlParams.ToArray());
        }

        public void ExtendAccessToken(string token)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputString("@Token", token, 50));
            _conn.ExecuteStoredProcedureNoReturn("account.ExtendAccessToken", sqlParams.ToArray());
        }

        public void Signup(string email, string displayName, string password, string salt, string gymName, long countryId, long cityId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputString("@Email", email, 255));
            sqlParams.Add(_conn.GenerateInputString("@DisplayName", displayName, 25));
            sqlParams.Add(_conn.GenerateInputString("@Password", password, 255));
            sqlParams.Add(_conn.GenerateInputString("@Salt", salt, 12));
            sqlParams.Add(_conn.GenerateInputString("@GymName", gymName, 50));
            sqlParams.Add(_conn.GenerateInputLong("@CountryId", countryId));
            sqlParams.Add(_conn.GenerateInputLong("@CityId", cityId));
            _conn.ExecuteStoredProcedureNoReturn("account.CreateNew", sqlParams.ToArray());
        }

        public void LogError(string source, string message, long accountId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputString("@SourceLocation", source, 512));
            sqlParams.Add(_conn.GenerateInputString("@ErrorMessage", message, 4000));
            sqlParams.Add(_conn.GenerateInputLong("@AccountId", accountId));
            _conn.ExecuteStoredProcedureNoReturn("logging.LogError", sqlParams.ToArray());
        }

        public DataRow GetAccountByEmail(string email)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputString("@Email", email, 255));
            return _conn.ExecuteStoredProcedureAsDataRow("account.GetByEmail", sqlParams.ToArray());
        }

        public long GetGymFromAccount(long accountId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@AccountId", accountId));
            return _conn.ExecuteStoredProcedureAsScalarLong("account.GetGymFromAccount", sqlParams.ToArray());
        }

        public int GetGymSpecificProspectCount(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            return _conn.ExecuteStoredProcedureAsScalarInt("fighter.GetGymSpecificProspectCount", sqlParams.ToArray());
        }

        public int GetStaffSpecificProspectCount(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId",gymId));
            return _conn.ExecuteStoredProcedureAsScalarInt("Staff.GetGymSpecificProspectCount", sqlParams.ToArray());
        }

        public long GetGymCountry(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            return _conn.ExecuteStoredProcedureAsScalarInt("gym.GetGymCountry", sqlParams.ToArray());            
        }
        public long GetGymCity(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            return _conn.ExecuteStoredProcedureAsScalarInt("gym.GetGymCity", sqlParams.ToArray());
        }


        public void CreateFighter(long? gymId, long countryId, long cityId, long backgroundId, long weightId, long heightId, 
            string firstName, string surname, int age, int chin, int heart, int strength, int agility, int stamina, 
            int jabs, int crosses, int hooks, int uppercuts, int legKicks, int bodyKicks, int headKicks, int backfists, 
            int elbows, int kneestrikes, int takedowns, int clinch, int takedownDefence, int headMovement, int footwork, 
            int wrestling, int groundGuard, int chokes, int armbars, int leglocks)
        {
            List<SqlParameter> sqlParams = new();
            if (gymId != null)
            {
                sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId.Value));
            }
            sqlParams.Add(_conn.GenerateInputLong("@CountryId", countryId));
            sqlParams.Add(_conn.GenerateInputLong("@CityId", cityId));
            sqlParams.Add(_conn.GenerateInputLong("@BackgroundId", backgroundId));
            sqlParams.Add(_conn.GenerateInputLong("@WeightId", weightId));
            sqlParams.Add(_conn.GenerateInputLong("@HeightId", heightId));
            sqlParams.Add(_conn.GenerateInputString("@FirstName", firstName, 50));
            sqlParams.Add(_conn.GenerateInputString("@Surname", surname, 50));
            sqlParams.Add(_conn.GenerateInputInteger("@Age", age));
            sqlParams.Add(_conn.GenerateInputInteger("@Chin", chin));
            sqlParams.Add(_conn.GenerateInputInteger("@Heart", heart));
            sqlParams.Add(_conn.GenerateInputInteger("@Strength", strength));
            sqlParams.Add(_conn.GenerateInputInteger("@Agility", agility));
            sqlParams.Add(_conn.GenerateInputInteger("@Stamina", stamina));
            sqlParams.Add(_conn.GenerateInputInteger("@Jabs", jabs));
            sqlParams.Add(_conn.GenerateInputInteger("@Crosses", crosses));
            sqlParams.Add(_conn.GenerateInputInteger("@Hooks", hooks));
            sqlParams.Add(_conn.GenerateInputInteger("@Uppercuts", uppercuts));
            sqlParams.Add(_conn.GenerateInputInteger("@Legkicks", legKicks));
            sqlParams.Add(_conn.GenerateInputInteger("@Bodykicks", bodyKicks));
            sqlParams.Add(_conn.GenerateInputInteger("@Headkicks", headKicks));
            sqlParams.Add(_conn.GenerateInputInteger("@Backfists", backfists));
            sqlParams.Add(_conn.GenerateInputInteger("@Elbows", elbows));
            sqlParams.Add(_conn.GenerateInputInteger("@Kneestrikes", kneestrikes));
            sqlParams.Add(_conn.GenerateInputInteger("@Takedowns", takedowns));
            sqlParams.Add(_conn.GenerateInputInteger("@Clinch", clinch));
            sqlParams.Add(_conn.GenerateInputInteger("@TakedownDefence", takedownDefence));
            sqlParams.Add(_conn.GenerateInputInteger("@HeadMovement", headMovement));
            sqlParams.Add(_conn.GenerateInputInteger("@Footwork", footwork));
            sqlParams.Add(_conn.GenerateInputInteger("@Wrestling", wrestling));
            sqlParams.Add(_conn.GenerateInputInteger("@Groundguard", groundGuard));
            sqlParams.Add(_conn.GenerateInputInteger("@Chokes", chokes));
            sqlParams.Add(_conn.GenerateInputInteger("@Armbars", armbars));
            sqlParams.Add(_conn.GenerateInputInteger("@Leglocks", leglocks));
            _conn.ExecuteStoredProcedureNoReturn("fighter.CreateNew", sqlParams.ToArray());

        }

        public void CreateStaff(long? gymId, long countryId, long cityId, int judgingAbility, int fitnessCoaching,
            int boxingCoaching, int wrestlingCoaching, int kickboxingCoaching, int submissionCoaching, int professionalism, int age, string firstName, string lastName)
        {
            List<SqlParameter> sqlParams = new();
            if (gymId != null)
            {
                sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId.Value));
            }
            sqlParams.Add(_conn.GenerateInputLong("@CountryId", countryId));
            sqlParams.Add(_conn.GenerateInputLong("@CityId", cityId));
            sqlParams.Add(_conn.GenerateInputInteger("@JudgingAbility", judgingAbility));
            sqlParams.Add(_conn.GenerateInputInteger("@FitnessCoaching", fitnessCoaching));
            sqlParams.Add(_conn.GenerateInputInteger("@BoxingCoaching", boxingCoaching));
            sqlParams.Add(_conn.GenerateInputInteger("@WrestlingCoaching", wrestlingCoaching));
            sqlParams.Add(_conn.GenerateInputInteger("@KickboxingCoaching", kickboxingCoaching));
            sqlParams.Add(_conn.GenerateInputInteger("@SubmissionCoaching", submissionCoaching));
            sqlParams.Add(_conn.GenerateInputInteger("@Professionalism", professionalism));
            sqlParams.Add(_conn.GenerateInputInteger("@Age", age));
            sqlParams.Add(_conn.GenerateInputString("@FirstName", firstName, 50));
            sqlParams.Add(_conn.GenerateInputString("@Surname", lastName, 50));
            _conn.ExecuteStoredProcedureNoReturn("staff.CreateNew", sqlParams.ToArray());
        }
        public DataTable GetProspects(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            return _conn.ExecuteStoredProcedureAsDataTable("fighter.GetProspects", sqlParams.ToArray());
        }
<<<<<<< Updated upstream
=======

        public void AcceptFighterToGym(long fighterId, long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@FighterId", gymId));
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            _conn.ExecuteStoredProcedureNoReturn("fighter.AssignToGym", sqlParams.ToArray());
        }
        public DataTable GetUnemployedStaff(long gymId)
        {
            List<SqlParameter> sqlParams = new();
            sqlParams.Add(_conn.GenerateInputLong("@GymId", gymId));
            return _conn.ExecuteStoredProcedureAsDataTable("staff.GetUnemployed", sqlParams.ToArray());   
        }
>>>>>>> Stashed changes
    }
}
