namespace WCL_BE.Model
{
    public class APIResponses
    {

        public struct GenericResponse
        {
            public bool Result;
            public string ErrorMessage;
            public object Data;
        }

    }
}
