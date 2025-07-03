using Xamarin.Forms;
using RecipeMobileApp.Models;

namespace RecipeMobileApp.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = recipe;
        }
    }
}

