
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
                new Item{Id = "Fresh Carrot", Description= "+ health"},
                new Item{Id = "Wet Grass", Description="+ 20% health"},
                new Item{Id = "Magical Dew", Description="+3% XP"},
                new Item{Id = "Tree Bark of Agility", Description = "+10% defense"},
                new Item{Id = "Rope of Vengeance", Description = "+3 strength"},
                new Item{Id = "Collar of Transcendence", Description = "+3 speed"},
                new Item{Id = "Necklace of Alacrity", Description = "+2 speed"},
                new Item{Id = "Jewel of Gibberish", Description="-3 on all stats"}
             };

            ItemsList.ItemsSource = items;
        }
    }
}

