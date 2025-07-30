using SQLite;

namespace RecipeMobileApp.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; } // newline-separated
        public string Steps { get; set; }       // newline-separated
        public string ImageUrl { get; set; }
        public bool IsVeg { get; set; }
        public bool IsDefault { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
