using System.Reflection;
using WCL_BE.Connectivity;
using WCL_BE.Security;
using static WCL_BE.Model.APIResponses;
using static WCL_BE.Helpers.APIHelper;
using System.Data;
using WCL_BE.Managers;
using System.Net;
namespace WCL_BE.Processors
{
    public class AccountProcessor
    {

        private IConfiguration _config;
        private WCLDB _db;

        public AccountProcessor(IConfiguration config) 
        { 
            _config = config;
            _db = new(config.GetSection("ConnectionStrings:WCLDB").Get<string>()!, false);
        }

        public GenericResponse Login(string email, string password)
        {
            try
            {

                // ###TODO: log IP address? (for multiple account detection, new device login check)
                // ###TODO: log browser agent? (for multiple account detection, new device login check)

                DataRow dr = _db.GetAccountByEmail(email);
                if (dr == null)
                {
                    return CreateFailureResponse("Either the email specified is not signed up, or the password is incorrect");
                }
                
                Encryptor enc = new(_config);
                string salt = dr.Field<string>("Salt")!;
                string pwdEncrypted = enc.Encrypt(password, salt);
                if (pwdEncrypted != dr.Field<string>("Password"))
                {
                    return CreateFailureResponse("Either the email specified is not signed up, or the password is incorrect");
                }
                
                string token = Guid.NewGuid().ToString();
                _db.AddAccessToken(dr.Field<long>("Id"), token);

                return CreateSuccessResponseWithData(token);
            }
            catch (Exception ex)
            {
                // We do not have a userId at this point, so log under account 0
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, 0);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse SignUp(string email, string displayName, string password, string gymName, 
            long countryId, long cityId)
        {
            try
            {

                // ###TODO check to make sure the email has not been used before
                // ###TODO check to make sure Display Name and Gym Name are not in use
                // ###TODO check for profanity in displayName or gymName

                Encryptor enc = new(_config);
                string salt = enc.GenerateRandomSalt();
                string pwdEncrypted = enc.Encrypt(password, salt);

                _db.Signup(email, displayName, pwdEncrypted, salt, gymName, countryId, cityId);
                return CreateSuccessResponseNoData();
            }
            catch (Exception ex)
            {
                // We do not have a userId at this point, so log under account 0
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!, 
                    ex.Message + " " + ex.StackTrace, 0);
                return CreateFailureResponse(GENERIC_ERROR);
            }          
        }

        public GenericResponse GetGymFromAccount(long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                int prospectCount = _db.GetGymSpecificProspectCount(gymId);
                int staffProspectCount = _db.GetStaffSpecificProspectCount(gymId);
                if (prospectCount < 5)
                {
                    // If a gym has less than 5 dedicated prospects, create some
                    // ###TODO - maybe stop people gaming the system by logging in repeatedly to generate more?
                    FighterManager fm = new(_config);
                    for (int nn = 0; nn < (5 - prospectCount); nn++)
                    {
                        fm.CreateFighter(gymId);
                    }
                }
                if(staffProspectCount < 5)
                {
                    StaffManager sm = new(_config);
                    for(int nn = 0; nn < (5 - staffProspectCount); nn++)
                    {
                        sm.CreateStaff(gymId);
                    }
                }
                return CreateSuccessResponseWithData(gymId);
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }


    }
}
