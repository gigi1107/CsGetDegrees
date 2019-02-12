
using WDown.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WDown.Views.Items;

namespace WDown.Views.Items
{
    public partial class ItemsListPage : ContentPage
    {
        List<Item> items;
        public ItemsListPage()
        {
            InitializeComponent();

            //ItemsList.ItemsSource = items;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            await Navigation.PushAsync(new ItemsListDetail(new ViewModels.ItemDetailViewModel(data)));

            //ItemsList.SelectedItem = null;
        }
    }
}


