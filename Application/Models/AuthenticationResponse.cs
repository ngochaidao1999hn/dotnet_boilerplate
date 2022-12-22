namespace Application.Models
{
    public class AuthenticationResponse
    {
        public string accessToken { get; set; }
        public DateTime expiredDate { get; set; }
    }
}