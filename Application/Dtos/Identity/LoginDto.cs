namespace Application.Dtos.Identity
{
    public class LoginDto
    {
        public string userName { get; set; } = default!;
        public string password { get; set; } = default!;
    }
}