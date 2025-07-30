using SQLite;

namespace RecipeMobileApp.Models
{
    public class Favorite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
