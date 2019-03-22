using System;
using System.Collections.Generic;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.GameEngine;
using WDown.Models;
using WDown.ViewModels;
using WDown.Views;

namespace WDown.Views.Battle
{
    // Auto Battle Page
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoBattlePage : ContentPage
    {


        public AutoBattlePage()
        {
            InitializeComponent();
        }

        // Upon Auto Battle clicked, run a new Auto Battle
        // Return a score after the battle end.
        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            // Can create a new battle engine...
            var myEngine = new AutoBattleEngine();

            var result = myEngine.RunAutoBattle();

            if (result == false)
            {
                await DisplayAlert("Error", "No Characters Available", "OK");
                return;
            }

            if (myEngine.GetRoundsValue() < 1)
            {
                await DisplayAlert("Error", "No Rounds Fought", "OK");
                return;
            }

            var myResult = myEngine.GetResultsOutput();
            var myScore = myEngine.GetScoreValue();

            var outputString = "Battle Over! Score " + myScore.ToString();

            var action = await DisplayActionSheet(outputString, "Cancel", null, "View Score");
            if (action == "View Score")
            {
                var myScoreObject = myEngine.GetScoreObject();
                await Navigation.PushAsync(new Views.Scores.ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
            }
        }

        // Handles when user click Cancel
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}
