using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeMobileApp.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly DataService _db = new DataService();

        public ProfileViewModel(User user)
        {
            User = user ?? new User();

            if (string.IsNullOrWhiteSpace(User.ProfileImageUrl))
                User.ProfileImageUrl = "defaultimage.png";

            FavoriteRecipes = new ObservableCollection<Recipe>();
            UserRecipes = new ObservableCollection<Recipe>();
        }

        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Recipe> FavoriteRecipes { get; set; }
        public ObservableCollection<Recipe> UserRecipes { get; set; }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChanged(); }
        }

        public async Task InitAsync()
        {
            if (User == null) return;
            IsBusy = true;

            var favs = await _db.GetFavoriteRecipesAsync(User.Id);
            FavoriteRecipes.Clear();
            foreach (var f in favs) FavoriteRecipes.Add(f);

            var mine = await _db.GetUserRecipesAsync(User.Id);
            UserRecipes.Clear();
            foreach (var r in mine) UserRecipes.Add(r);

            if (string.IsNullOrWhiteSpace(User.Bio)) User.Bio = "No bio added.";
            if (string.IsNullOrWhiteSpace(User.Hobbies)) User.Hobbies = "No hobbies listed.";

            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(FavoriteRecipes));
            OnPropertyChanged(nameof(UserRecipes));
            IsBusy = false;
        }

        public async Task<bool> SaveProfileAsync()
        {
            if (string.IsNullOrWhiteSpace(User.Username))
                return false;

            await _db.UpdateUserAsync(User);
            return true;
        }

        // ✅ ✅ ✅ Delete Recipe Method
        public async Task DeleteRecipeAsync(Recipe recipe)
        {
            if (recipe == null) return;

            IsBusy = true;

            await _db.DeleteRecipeAsync(recipe);

            if (UserRecipes.Contains(recipe))
                UserRecipes.Remove(recipe);

            if (FavoriteRecipes.Contains(recipe))
                FavoriteRecipes.Remove(recipe);

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
