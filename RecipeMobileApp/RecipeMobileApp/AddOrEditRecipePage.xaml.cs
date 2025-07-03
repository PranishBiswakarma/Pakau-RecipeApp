using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;

namespace RecipeMobileApp.Views
{
    public partial class AddEditRecipePage : ContentPage
    {
        private RecipeViewModel viewModel;
        private Recipe editingRecipe;

        public AddEditRecipePage(RecipeViewModel vm, string cuisine, string category, Recipe recipe = null, string type = null)
        {
            InitializeComponent();
            viewModel = vm;
            editingRecipe = recipe;

            if (editingRecipe != null)
            {
                NameEntry.Text = editingRecipe.Name;
                CuisineEntry.Text = editingRecipe.Cuisine;
                CategoryEntry.Text = editingRecipe.Category;
                TypeEntry.Text = editingRecipe.Type;
                IngredientsEntry.Text = editingRecipe.Ingredients;
                StepsEditor.Text = editingRecipe.Steps;
                ImageUrlEntry.Text = editingRecipe.ImageUrl;
            }
            else
            {
                CuisineEntry.Text = cuisine;
                CategoryEntry.Text = category;
                if (!string.IsNullOrEmpty(type))
                    TypeEntry.Text = type;
            }
        }

        private async void OnSaveClicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text)) return;

            if (editingRecipe == null)
            {
                var newRecipe = new Recipe
                {
                    Name = NameEntry.Text,
                    Cuisine = CuisineEntry.Text,
                    Category = CategoryEntry.Text,
                    Type = TypeEntry.Text,
                    Ingredients = IngredientsEntry.Text,
                    Steps = StepsEditor.Text,
                    ImageUrl = ImageUrlEntry.Text
                };
                viewModel.AddRecipe(newRecipe);
            }
            else
            {
                editingRecipe.Name = NameEntry.Text;
                editingRecipe.Cuisine = CuisineEntry.Text;
                editingRecipe.Category = CategoryEntry.Text;
                editingRecipe.Type = TypeEntry.Text;
                editingRecipe.Ingredients = IngredientsEntry.Text;
                editingRecipe.Steps = StepsEditor.Text;
                editingRecipe.ImageUrl = ImageUrlEntry.Text;
                viewModel.UpdateRecipe(editingRecipe);
            }
            await Navigation.PopAsync();
        }
    }
}
