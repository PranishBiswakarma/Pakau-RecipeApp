using System.Collections.ObjectModel;
using RecipeMobileApp.Models;

namespace RecipeMobileApp.ViewModels
{
    public class MainPageViewModel
    {
        public string ProfileName { get; set; }
        public string ProfileImageUrl { get; set; }
        public ObservableCollection<CuisineCategory> CuisineCategories { get; set; }

        public MainPageViewModel()
        {
            // For demo, random profile image and name
            ProfileName = "Pranish";
            ProfileImageUrl = "https://randomuser.me/api/portraits/men/10.jpg";

            CuisineCategories = new ObservableCollection<CuisineCategory>
            {
                new CuisineCategory { Cuisine = "Nepali", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Dal_bhat_tarkari.jpg" },
                new CuisineCategory { Cuisine = "Chinese", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Chow_mein.jpg" },
                new CuisineCategory { Cuisine = "Japanese", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/60/Sushi_platter.jpg" },
                new CuisineCategory { Cuisine = "Continental", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5a/Continental_breakfast.jpg" },
                new CuisineCategory { Cuisine = "Italian", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0b/Spaghetti_alla_Carbonara.jpg" },
                new CuisineCategory { Cuisine = "Indian", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Indian_food.jpg" }
            };
        }
    }
}
