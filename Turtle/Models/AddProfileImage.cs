namespace Turtle.Models
{
    public class AddProfileImage
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public string AccountType { get; set; }
        public bool UserStatus { get; set; }
    }
}
