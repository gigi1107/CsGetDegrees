using System;
using System.Collections.Generic;
using week2.Models;
using week2.ViewModels;
using Xamarin.Forms;

namespace week2.Views
{
    public partial class CharacterDetailPage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public CharacterDetailPage(CharacterDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        public CharacterDetailPage()
        {
            InitializeComponent();

            var data = new Character();

            _viewModel = new CharacterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        async void Delete_Clicked (object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CharacterDeletePage(_viewModel));
        }

        async void Edit_Clicked (object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CharacterEditPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

       

    }
}
