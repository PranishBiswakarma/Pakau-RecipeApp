using System.Collections.ObjectModel;
using System.Linq;
using RecipeMobileApp.Models;

namespace RecipeMobileApp.ViewModels
{
    public class RecipeViewModel
    {
        public ObservableCollection<string> Cuisines { get; set; }
        public ObservableCollection<CuisineCategory> CuisineCategories { get; set; }
        public ObservableCollection<Recipe> AllRecipes { get; set; }

        public RecipeViewModel()
        {
            Cuisines = new ObservableCollection<string>
            {
                "Nepali", "Indian", "Chinese", "Italian"
            };

            CuisineCategories = new ObservableCollection<CuisineCategory>
            {
                new CuisineCategory { Cuisine = "Nepali", Category = "Snacks", ImageUrl = "https://img.icons8.com/color/96/000000/french-fries.png" },
                new CuisineCategory { Cuisine = "Nepali", Category = "Main Course", ImageUrl = "https://img.icons8.com/color/96/000000/steak.png" },
                new CuisineCategory { Cuisine = "Nepali", Category = "Dessert", ImageUrl = "https://img.icons8.com/color/96/000000/cheesecake.png" },
                new CuisineCategory { Cuisine = "Nepali", Category = "Soup", ImageUrl = "https://img.icons8.com/color/96/000000/soup-plate.png" },
                // Add more for other cuisines as needed...
            };

            AllRecipes = new ObservableCollection<Recipe>
            {
                // Nepali - Snacks - Veg
                new Recipe { Id=1, Name="Veg Momo", Cuisine="Nepali", Category="Snacks", Type="Veg", ImageUrl="https://www.archanaskitchen.com/images/archanaskitchen/1-Author/shaheen_ali/Veg_Momos_Recipe-1.jpg" },
                new Recipe { Id=2, Name="Paneer Pakora", Cuisine="Nepali", Category="Snacks", Type="Veg", ImageUrl="https://www.vegrecipesofindia.com/wp-content/uploads/2021/03/paneer-pakora-1.jpg" },
                new Recipe { Id=3, Name="Samosa", Cuisine="Nepali", Category="Snacks", Type="Veg", ImageUrl="https://www.indianhealthyrecipes.com/wp-content/uploads/2021/07/samosa-recipe.jpg" },
                new Recipe { Id=4, Name="Aloo Chop", Cuisine="Nepali", Category="Snacks", Type="Veg", ImageUrl="https://www.nepalicookingcourse.com/wp-content/uploads/2016/01/aloo-chop.jpg" },

                // Nepali - Snacks - Non-Veg
                new Recipe { Id=5, Name="Chicken Momo", Cuisine="Nepali", Category="Snacks", Type="Non-Veg", ImageUrl="https://www.cookwithmanali.com/wp-content/uploads/2020/04/Chicken-Momos.jpg" },
                new Recipe { Id=6, Name="Egg Roll", Cuisine="Nepali", Category="Snacks", Type="Non-Veg", ImageUrl="https://www.cookwithmanali.com/wp-content/uploads/2017/07/Egg-Roll-500x500.jpg" },
                new Recipe { Id=7, Name="Chicken Pakora", Cuisine="Nepali", Category="Snacks", Type="Non-Veg", ImageUrl="https://www.indianhealthyrecipes.com/wp-content/uploads/2021/07/chicken-pakora-recipe.jpg" },
                new Recipe { Id=8, Name="Fish Fry", Cuisine="Nepali", Category="Snacks", Type="Non-Veg", ImageUrl="https://www.indianhealthyrecipes.com/wp-content/uploads/2021/10/fish-fry-recipe.jpg" },

                // Add more for other categories, types, and cuisines...
            };
        }

        public void AddRecipe(Recipe recipe)
        {
            recipe.Id = AllRecipes.Any() ? AllRecipes.Max(r => r.Id) + 1 : 1;
            AllRecipes.Add(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var old = AllRecipes.FirstOrDefault(r => r.Id == recipe.Id);
            if (old != null)
            {
                old.Name = recipe.Name;
                old.Cuisine = recipe.Cuisine;
                old.Category = recipe.Category;
                old.Type = recipe.Type;
                old.Ingredients = recipe.Ingredients;
                old.Steps = recipe.Steps;
                old.ImageUrl = recipe.ImageUrl;
            }
        }

        public void DeleteRecipe(Recipe recipe)
        {
            AllRecipes.Remove(recipe);
        }
    }
}
