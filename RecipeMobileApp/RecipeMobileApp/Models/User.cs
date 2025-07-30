using SQLite;

namespace RecipeMobileApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Bio { get; set; }
        public string Hobbies { get; set; }
        public string ProfileImageUrl { get; set; }
    }

}
