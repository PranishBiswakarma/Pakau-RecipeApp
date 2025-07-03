using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditRecipePage : ContentPage
    {
        private RecipeViewModel viewModel;
        private Recipe editingRecipe;

        public AddOrEditRecipePage(RecipeViewModel vm, Recipe recipe = null)
        {
            InitializeComponent();
            viewModel = vm;
            editingRecipe = recipe;

            if (editingRecipe != null)
            {
                NameEntry.Text = editingRecipe.Name;
                IngredientsEntry.Text = editingRecipe.Ingredients;
                StepsEntry.Text = editingRecipe.Steps;
                ImageUrlEntry.Text = editingRecipe.ImageUrl;
                SaveButton.Text = "Update";
            }
            else
            {
                SaveButton.Text = "Add";
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Name is required.", "OK");
                return;
            }

            if (editingRecipe != null)
            {
                editingRecipe.Name = NameEntry.Text;
                editingRecipe.Ingredients = IngredientsEntry.Text;
                editingRecipe.Steps = StepsEntry.Text;
                editingRecipe.ImageUrl = ImageUrlEntry.Text;
            }
            else
            {
                var newRecipe = new Recipe
                {
                    Name = NameEntry.Text,
                    Ingredients = IngredientsEntry.Text,
                    Steps = StepsEntry.Text,
                    ImageUrl = ImageUrlEntry.Text,
                    Cuisine = "Nepali", // Default, you can extend this
                    Type = "Veg"        // Default, you can extend this
                };
                viewModel.AllRecipes.Add(newRecipe);
                viewModel.FilteredRecipes.Add(newRecipe);
            }
            await Navigation.PopAsync();
        }
    }
}
