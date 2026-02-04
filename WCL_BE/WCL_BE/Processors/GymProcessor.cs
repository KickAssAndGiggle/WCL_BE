using Microsoft.Identity.Client;
using System.Data;
using System.Reflection;
using WCL_BE.Connectivity;
using WCL_BE.Managers;
using static WCL_BE.Helpers.APIHelper;
using static WCL_BE.Model.APIResponses;
using static WCL_BE.Model.Model;
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

        public GenericResponse GetPropsects(long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                DataTable dt = _db.GetProspects(gymId);
                Fighter[] ret = (Fighter[])ModelMaker(dt, typeof(Fighter));
                return CreateSuccessResponseWithData(ret);
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse AcceptProspectToGym(long prospectId, long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                _db.AcceptFighterToGym(prospectId, gymId);
                return CreateSuccessResponseNoData();
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse GetUnemployedStaff(long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                DataTable dt = _db.GetUnemployedStaff(gymId);
                Staff[] ret = (Staff[])ModelMaker(dt, typeof(Staff));
                return CreateSuccessResponseWithData(ret);
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse HireStaffMember(long staffId, long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                _db.HireStaffMember(gymId, staffId);
                return CreateSuccessResponseNoData();
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                 ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse GetHiredStaff(long accountId)
        {
            try
            {
                long gymId = _db.GetGymFromAccount(accountId);
                DataTable dt = _db.GetHiredStaff(gymId);
                Staff[] ret = (Staff[])ModelMaker(dt, typeof(Staff));
                return CreateSuccessResponseWithData(ret);
            }
            catch (Exception ex)
            {
                _db.LogError(MethodBase.GetCurrentMethod()?.Module + "/" + MethodBase.GetCurrentMethod()?.Name!,
                    ex.Message + " " + ex.StackTrace, accountId);
                return CreateFailureResponse(GENERIC_ERROR);
            }
        }

        public GenericResponse GenerateNewEvent(long promoterId, long accountId)
        {
            try
            {
            PromoterManager pm = new(_config);
            pm.GenerateNewEvent(promoterId);
            return CreateSuccessResponseNoData();
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
