namespace Application.Dtos.Identity
{
    public class RegisterDto
    {
        public string userName { get; set; } = default!;
        public string password { get; set; } = default!;
        public string street { get; set; } = default!;
        public string city { get; set; } = default!;
        public string country { get; set; } = default!;
        public string zipCode { get; set; } = default!;
        public string name { get; set; } = default!;
    }
}