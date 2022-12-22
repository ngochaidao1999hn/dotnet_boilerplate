namespace Application.Dtos.Identity
{
    public class RegisterDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
        public string name { get; set; }
        public int roleId { get; set; }
    }
}