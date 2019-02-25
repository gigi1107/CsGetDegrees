using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WDown.Views;

namespace WDown.Views
{
    public partial class GameOpeningPage : ContentPage
    {
        public GameOpeningPage()
        {
            InitializeComponent();
        }

        private async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.AutoBattlePage());
        }
    }
}
