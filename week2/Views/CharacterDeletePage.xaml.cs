using System;
using System.Collections.Generic;
using week2.Models;
using week2.ViewModels;
using Xamarin.Forms;

namespace week2.Views
{

    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDeletePage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public Character Data { get; set; }

        public CharacterDeletePage(CharacterDetailViewModel viewModel)
        {
            // Save the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Data.Name;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Character Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}

