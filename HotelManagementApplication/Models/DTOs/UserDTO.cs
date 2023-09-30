namespace FirstAPI.Models.DTOs
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? address { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
