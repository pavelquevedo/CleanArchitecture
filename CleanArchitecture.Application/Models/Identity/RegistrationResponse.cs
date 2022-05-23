namespace CleanArchitecture.Application.Models.Identity
{
    public class RegistrationResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
