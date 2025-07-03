using System.Collections.Generic;
using System.Linq;

namespace RecipeMobileApp
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public static class UserStore
    {
        // This list will be cleared when the app restarts (demo only)
        public static List<User> Users = new List<User>
        {
            new User { Username = "user", Password = "pass", Phone = "1234567890" } // default user for testing
        };

        public static bool ValidateUser(string username, string password)
        {
            return Users.Any(u => u.Username == username && u.Password == password);
        }

        public static bool UserExists(string username)
        {
            return Users.Any(u => u.Username == username);
        }

        public static void AddUser(string username, string password, string phone)
        {
            Users.Add(new User { Username = username, Password = password, Phone = phone });
        }
    }
}
