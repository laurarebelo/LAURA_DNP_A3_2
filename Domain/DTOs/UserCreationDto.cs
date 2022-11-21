namespace Domain.DTOs
{
    public class UserCreationDto
    {
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }
        public string Role { get; }

        public UserCreationDto(string userName, string password, string email, string role)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Role = role;
        }
    }
}