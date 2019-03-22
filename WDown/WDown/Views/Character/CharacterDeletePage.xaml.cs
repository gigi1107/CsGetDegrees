using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using WDown.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// This page allows user to delete a character
namespace WDown.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDeletePage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public WDown.Models.Character Data { get; set; }

        public CharacterDeletePage(CharacterDetailViewModel viewModel)
        {
            // Save the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Data.Name;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        // When user hits delete, remove the character from database
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Character Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        // When user hits cancel, it brings back to previous page
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}

