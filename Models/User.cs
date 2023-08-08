namespace Poc.Authentication.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}