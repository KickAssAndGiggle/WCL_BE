namespace WCL_BE.Model
{
    public class APIRequests
    {

        public struct LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public struct NewAccountRequest
        {
            public string Email {  get; set; }
            public string DisplayName { get; set; }
            public string Password { get; set; }
            public string GymName { get; set; }
            public long Country { get; set; }
            public long City { get; set; }
        }

    }
}
