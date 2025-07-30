using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class RecipeDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly DataService _dataService = new DataService();

        public Recipe Recipe { get; set; }
        public User CurrentUser { get; set; }
        public List<string> IngredientsList { get; set; }
        public List<string> StepsList { get; set; }

        public bool CanEdit => !Recipe.IsDefault && Recipe.CreatedByUserId == CurrentUser.Id;

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
                OnPropertyChanged(nameof(FavButtonText));
            }
        }

        public string FavButtonText => IsFavorite ? "Remove from Favorites" : "Add to Favorites";

        public RecipeDetailPage(Recipe recipe, User user)
        {
            InitializeComponent();

            Recipe = recipe;
            CurrentUser = user;
            IngredientsList = (Recipe.Ingredients ?? "").Split('\n').ToList();
            StepsList = (Recipe.Steps ?? "").Split('\n').ToList();

            BindingContext = this;
            Title = recipe.Name;

            _ = LoadFavoriteStatusAsync();
        }

        private async Task LoadFavoriteStatusAsync()
        {
            var favorites = await _dataService.GetFavoritesAsync(CurrentUser.Id);
            IsFavorite = favorites.Any(f => f.RecipeId == Recipe.Id);
        }

        private async void OnFavoriteClicked(object sender, System.EventArgs e)
        {
            await _dataService.ToggleFavoriteAsync(CurrentUser.Id, Recipe.Id);
            await LoadFavoriteStatusAsync();
        }

        private async void OnEditClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddEditUserRecipePage(Recipe, CurrentUser));
        }

        // 🔙 Fast back to main
        private void OnBackToMainClicked(object sender, System.EventArgs e)
        {
            // Replace the navigation stack with MainPage immediately
            Application.Current.MainPage = new NavigationPage(new MainPage(CurrentUser));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected new void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
