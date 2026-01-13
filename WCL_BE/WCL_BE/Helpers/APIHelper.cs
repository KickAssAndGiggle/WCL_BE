using static System.Runtime.InteropServices.JavaScript.JSType;
using static WCL_BE.Model.APIResponses;

namespace WCL_BE.Helpers
{
    public static class APIHelper
    {

        public const string GENERIC_ERROR = "A non-specific error occurred. Please contact support for assistance.";

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

    }
}
