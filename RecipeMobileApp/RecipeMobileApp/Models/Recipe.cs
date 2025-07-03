namespace RecipeMobileApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string Type { get; set; } // "Veg" or "Non-Veg"
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string ImageUrl { get; set; }
    }
}
