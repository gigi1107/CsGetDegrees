using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WDown.Models;
using WDown.ViewModels;

namespace WDown.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
        private ItemsViewModel _viewModel;

        public ItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ItemsViewModel.Instance;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(data)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemNewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0)
            {

                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}