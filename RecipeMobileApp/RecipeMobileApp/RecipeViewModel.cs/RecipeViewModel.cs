using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using RecipeMobileApp.Models;

namespace RecipeMobileApp.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recipe> AllRecipes { get; set; }
        public ObservableCollection<Recipe> FilteredRecipes { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RecipeViewModel()
        {
            AllRecipes = new ObservableCollection<Recipe>
            {
                // Nepali
                new Recipe { Name = "Dal Bhat", Type = "Veg", Cuisine = "Nepali", Ingredients = "Rice, lentils, vegetables", Steps = "Cook rice, cook dal, serve together.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Dal_bhat_tarkari.jpg" },
                new Recipe { Name = "Paneer Curry", Type = "Veg", Cuisine = "Nepali", Ingredients = "Paneer, spices", Steps = "Cook paneer with spices.", ImageUrl = "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/05/paneer-curry-recipe.jpg" },
                new Recipe { Name = "Chicken Momo", Type = "NonVeg", Cuisine = "Nepali", Ingredients = "Chicken, flour, spices", Steps = "Prepare filling, wrap, steam.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4d/Momo_nepal.jpg" },
                new Recipe { Name = "Fish Curry", Type = "NonVeg", Cuisine = "Nepali", Ingredients = "Fish, spices", Steps = "Cook fish with spices.", ImageUrl = "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/05/fish-curry-recipe.jpg" },
                // Chinese
                new Recipe { Name = "Veg Chow Mein", Type = "Veg", Cuisine = "Chinese", Ingredients = "Noodles, vegetables, soy sauce", Steps = "Boil noodles, stir-fry veggies, mix.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Chow_mein.jpg" },
                new Recipe { Name = "Spring Rolls", Type = "Veg", Cuisine = "Chinese", Ingredients = "Cabbage, carrot, wrappers", Steps = "Wrap and fry.", ImageUrl = "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/05/spring-rolls-recipe.jpg" },
                new Recipe { Name = "Chicken Manchurian", Type = "NonVeg", Cuisine = "Chinese", Ingredients = "Chicken, sauce", Steps = "Fry chicken, toss in sauce.", ImageUrl = "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/05/chicken-manchurian.jpg" },
                new Recipe { Name = "Egg Fried Rice", Type = "NonVeg", Cuisine = "Chinese", Ingredients = "Egg, rice, veggies", Steps = "Cook rice, fry with egg and veggies.", ImageUrl = "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/05/egg-fried-rice.jpg" },
                // Add more recipes for other cuisines as needed
            };
            FilteredRecipes = new ObservableCollection<Recipe>();
        }

        public void FilterByCuisineAndType(string cuisine, string type)
        {
            FilteredRecipes.Clear();
            foreach (var recipe in AllRecipes.Where(r => r.Cuisine == cuisine && r.Type == type))
                FilteredRecipes.Add(recipe);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredRecipes)));
        }
    }
}
