using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RecipeMobileApp.ViewModels
{
    public class AddEditUserRecipeViewModel : INotifyPropertyChanged
    {
        private readonly DataService _db = new DataService();
        private readonly User CurrentUser;

        public Recipe Recipe { get; set; }

        // ✅ Predefined categories to bind to Picker
        public List<string> Categories { get; } = new List<string>
        {
            "Nepalese",
            "Chinese",
            "Japanese",
            "Italian"
        };

        public AddEditUserRecipeViewModel(Recipe recipe, User user)
        {
            CurrentUser = user;

            // Initialize recipe (for new entry) or copy values (for edit)
            Recipe = recipe ?? new Recipe
            {
                CreatedByUserId = user.Id,
                IsVeg = true,
                ImageUrl = "default.jpg"
            };

            // Ensure category is always initialized
            if (string.IsNullOrWhiteSpace(Recipe.Category))
                Recipe.Category = Categories[0];
        }

        public async Task<bool> SaveRecipeAsync()
        {
            if (string.IsNullOrWhiteSpace(Recipe.Name) || string.IsNullOrWhiteSpace(Recipe.Category))
                return false;

            if (Recipe.Id == 0)
                await _db.AddUserRecipeAsync(Recipe);
            else
                await _db.UpdateUserRecipeAsync(Recipe);

            return true;
        }

        // Optional: implement INotifyPropertyChanged if needed for dynamic binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
