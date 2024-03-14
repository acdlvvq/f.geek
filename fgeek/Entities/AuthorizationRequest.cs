namespace fgeek.Entities
{
    public class AuthorizationRequest
    {
        public string Email    { get; set; }
        public string Password { get; set; }
        public static Func<User, bool> IsCorrectRequest(AuthorizationRequest authorizationRequest)
        {
            return user =>
            {
                return user.Email == authorizationRequest.Email &&
                       user.Password == authorizationRequest.Password;
            };
        }

        public AuthorizationRequest() 
        {
            Email = Password = string.Empty;
        }

        public AuthorizationRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
