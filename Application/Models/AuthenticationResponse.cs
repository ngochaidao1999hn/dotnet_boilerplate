namespace Application.Models
{
    public class AuthenticationResponse
    {
        public string accessToken { get; set; } = default!;
        public DateTime expiredDate { get; set; }
    }
}