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
                new CuisineCategory { Name = "Nepali", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Dal_bhat_tarkari.jpg" },
                new CuisineCategory { Name = "Chinese", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Chow_mein.jpg" },
                new CuisineCategory { Name = "Japanese", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/60/Sushi_platter.jpg" },
                new CuisineCategory { Name = "Continental", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5a/Continental_breakfast.jpg" },
                new CuisineCategory { Name = "Italian", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0b/Spaghetti_alla_Carbonara.jpg" },
                new CuisineCategory { Name = "Indian", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Indian_food.jpg" }
            };
        }
    }
}
