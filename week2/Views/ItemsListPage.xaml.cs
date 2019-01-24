
using week2.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace week2.Views
{
    public partial class ItemsListPage : ContentPage
    {
        List<Item> items;
        public ItemsListPage()
        {
            InitializeComponent();
            items = new List<Item>()
            {
                new Item{Name = "Fresh Carrot", Description= "+ health"},
                new Item{Name = "Wet Grass", Description="+ 20% health"},
                new Item{Name = "Magical Dew", Description="+3% XP"},
                new Item{Name = "Tree Bark of Agility", Description = "+10% defense"},
                new Item{Name = "Rope of Vengeance", Description = "+3 strength"}

             };

            ItemsList.ItemsSource = items;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            await Navigation.PushAsync(new ItemsListDetail(new ViewModels.ItemDetailViewModel(data)));

            ItemsList.SelectedItem = null;
        }
    }
}


