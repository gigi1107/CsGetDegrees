using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var data = new WDown.Models.Item();

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }

        // Handles when user click Edit
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEditPage(_viewModel));
        }

        // Handle when user click Delete
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDeletePage(_viewModel));
        }

    }
}