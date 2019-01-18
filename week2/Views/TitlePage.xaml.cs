using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace week2.Views
{
    public partial class TitlePage : ContentPage
    {
        public TitlePage()
        {
            InitializeComponent();

        }

        async void OnHistoryClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        async void OnCharacterClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CharacterPage());
        }

        async void OnItemsClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ItemsListPage());
        }
    }
}
