namespace pos.users.Models
{
    public class UserToken
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => FirstName + " " + LastName;

        public string Role { get; set; } = string.Empty;
    }
}
