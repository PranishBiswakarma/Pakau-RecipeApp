using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeMobileApp.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Type { get; set; } // "Veg" or "NonVeg"
        public string Cuisine { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource ImageSource => ImageSource.FromUri(new Uri(ImageUrl));
    }
}
