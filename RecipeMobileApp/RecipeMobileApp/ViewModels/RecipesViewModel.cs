using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using Xamarin.Forms;

namespace RecipeMobileApp.ViewModels
{
    public class RecipesViewModel : INotifyPropertyChanged
    {
        private readonly DataService _db = new DataService();

        public ObservableCollection<Recipe> FilteredRecipes { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> FavoriteRecipes { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> UserRecipes { get; } = new ObservableCollection<Recipe>();

        public int CurrentUserId { get; }

        private string selectedCategory;
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged();
                    _ = LoadRecipesAsync();
                }
            }
        }

        private bool showVeg = true;
        public bool ShowVeg
        {
            get => showVeg;
            set
            {
                if (showVeg != value)
                {
                    showVeg = value;
                    OnPropertyChanged();
                    _ = LoadRecipesAsync();
                }
            }
        }

        public ICommand ShowVegCommand { get; }
        public ICommand ShowNonVegCommand { get; }

        public RecipesViewModel(int userId)
        {
            CurrentUserId = userId;

            ShowVegCommand = new Command(() => ShowVeg = true);
            ShowNonVegCommand = new Command(() => ShowVeg = false);
        }

        // ✅ All recipes for selected category and veg/non-veg
        public async Task LoadRecipesAsync()
        {
            var allRecipes = await _db.GetRecipesAsync();

            System.Diagnostics.Debug.WriteLine($"[LoadRecipes] Loaded: {allRecipes.Count}");

            var filtered = allRecipes
                .Where(r =>
                    r.Category == SelectedCategory &&
                    r.IsVeg == ShowVeg &&
                    (r.IsDefault || r.CreatedByUserId == CurrentUserId))
                .ToList();

            System.Diagnostics.Debug.WriteLine($"[Filter] Category={SelectedCategory}, ShowVeg={ShowVeg}, Filtered={filtered.Count}");

            FilteredRecipes.Clear();
            foreach (var r in filtered)
                FilteredRecipes.Add(r);
        }

        // ✅ All favorites for this user
        public async Task LoadFavoriteRecipesAsync()
        {
            var favs = await _db.GetFavoriteRecipesAsync(CurrentUserId);
            FavoriteRecipes.Clear();
            foreach (var r in favs)
                FavoriteRecipes.Add(r);
        }

        // ✅ Load all recipes created by the user (for Profile)
        public async Task LoadUserRecipesAsync()
        {
            var userRecipes = await _db.GetUserRecipesAsync(CurrentUserId);
            UserRecipes.Clear();
            foreach (var r in userRecipes)
                UserRecipes.Add(r);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
