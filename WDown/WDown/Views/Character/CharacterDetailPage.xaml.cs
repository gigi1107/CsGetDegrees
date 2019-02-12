using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Character
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

            var data = new WDown.Models.Character();

            _viewModel = new CharacterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        async void Delete_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CharacterDeletePage(_viewModel));
        }

        async void Edit_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CharacterEditPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }



    }
}
