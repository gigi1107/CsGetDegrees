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

        // Handle when user chooses to start Auto Battle
        private async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.AutoBattlePage());
        }
        // Handle when user chooses to start Regular Battle
        private async void RegularBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleOpeningPage());
        }


        // These are temporary buttons to help developing the Battle screens 

        // This starts the Character Select Page    
        private async void Character_Select_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleCharacterSelectPage());
        }

        // This starts the Monster List Page
        private async void Monster_Select_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleMonsterListTest());
        }
    }
}
