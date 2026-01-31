using System.Data;
using WCL_BE.Connectivity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WCL_BE.Model.APIResponses;

namespace WCL_BE.Helpers
{
    public static class APIHelper
    {

        public const string GENERIC_ERROR = "A non-specific error occurred. Please contact support for assistance.";
        public const string TOKEN_ERROR = "Token invalid or expired";

        public static GenericResponse CreateSuccessResponseNoData()
        {
            GenericResponse resp = new GenericResponse()
            {
                Result = true
            };

            return resp;
        }

        public static GenericResponse CreateSuccessResponseWithData(object data)
        {
            GenericResponse resp = new GenericResponse()
            {
                Result = true,
                Data = data
            };

            return resp;
        }

        public static GenericResponse CreateFailureResponse(string message)
        {
            GenericResponse resp = new GenericResponse()
            {
                Result = false,
                ErrorMessage = message
            };

            return resp;
        }

        public static void GenerateNames(long countryId, WCLDB database, out string firstName, out string surname)
        {
            DataTable firstNameDT = database.GenerateFirstNames(countryId);
            DataTable surnameDT = database.GenerateSurnames(countryId);
            // TODO : need to seed using new Random(Environment.TickCount)
            Random rnd = new();
            int firstNameRandomNo = rnd.Next(1, firstNameDT.Rows.Count);
            int surnameRandomNo = rnd.Next(1, surnameDT.Rows.Count);
            firstName = firstNameDT.Rows[firstNameRandomNo]["FirstName"].ToString()!;
            surname = surnameDT.Rows[surnameRandomNo]["Surname"].ToString()!;
        }

    }
}
