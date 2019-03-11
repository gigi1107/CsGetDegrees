using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WDown.Models;
using WDown.ViewModels;
using WDown.GameEngine;

namespace WDown.Views.Battle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleMainPage : ContentPage
    {

        BattleCharacterSelectPage _myModalCharacterSelectPage;
        BattleMonsterListPage _myModalBattleMonsterListPage;

        private BattleViewModel _viewModel;


        /// <summary>
        /// Stand up the Page and initiate state
        /// </summary>
        public BattleMainPage(BattleViewModel viewModel)
        {
            InitializeComponent();

            // Show the Next button, hide the Game Over button
            GameNextButton.IsVisible = true;
            GameOverButton.IsVisible = false;
          



            BindingContext = _viewModel = viewModel;


            //            var browser = new WebView();

            //var htmlSource = new HtmlWebViewSource();

            //htmlSource.Html = @"<html><body><h1>Xamarin.Forms</h1><p>Welcome to WebView.</p></body></html>";

            //HtmlBox.Source = htmlSource;

        
        }

    

        /// <summary>
        /// Next Turn Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnNextClicked(object sender, EventArgs args)
        {
            // Do the turn...
            MessagingCenter.Send(this, "RoundNextTurn");

            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;

            Debug.WriteLine("current player: " + _viewModel.currentPlayerName);
            Debug.WriteLine("current player imageUrI: " + _viewModel.currentPlayerURI);

            OnPropertyChanged();


            // If the round is over start a new one...
            if (CurrentRoundState == Round.RoundEnum.NewRound)
            {
                MessagingCenter.Send(this, "NewRound");

                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                ShowModalPageMonsterList();
                Debug.WriteLine("current player: " + _viewModel.BattleEngine.PlayerCurrent.Name);
                OnPropertyChanged();

            }

            // Check for Game Over
            if (CurrentRoundState == Round.RoundEnum.GameOver)
            {
                MessagingCenter.Send(this, "EndBattle");
                Debug.WriteLine("End Battle");

                // Output Formatted Results 
                var myResult = _viewModel.BattleEngine.GetResultsOutput();
                Debug.Write(myResult);
                OnPropertyChanged();

                // Let the user know the game is over
                GameMessage("Game Over\n");

                // Change to the Game Over Button
                GameNextButton.IsVisible = false;
                GameOverButton.IsVisible = true;

                return;
            }

            // Output The Message that happened.
            GameMessage(_viewModel.BattleEngine.BattleMessages.TurnMessage);
        }

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage(string message)
        {
            Debug.WriteLine("Message: " + message);

            MessageText.Text = message + "\n" + MessageText.Text;
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnGameOverClicked(object sender, EventArgs args)
        {
            var myScore = _viewModel.BattleEngine.BattleScore.ScoreTotal;
            var outputString = "Battle Over! Score " + myScore.ToString();
            Debug.WriteLine(outputString);

            // Alternative way to have Game Over Page

            //var myScoreObject = _viewModel.BattleEngine.BattleScore;
            //await Navigation.PushAsync(new Scores.ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
            // Back up to the Start of Battle
            //await Navigation.PopToRootAsync()


            await Navigation.PushAsync(new BattleGameOverPage());
            ;
        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalBattleMonsterListPage)
            {
                _myModalBattleMonsterListPage = null;

                // remember to remove the event handler:
                WDown.App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalCharacterSelectPage)
            {
                _myModalCharacterSelectPage = null;

                // remember to remove the event handler:
                WDown.App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalBattleMonsterListPage = new BattleMonsterListPage();
            await Navigation.PushModalAsync(_myModalBattleMonsterListPage);
        }

        private async void ShowModalPageCharcterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalCharacterSelectPage = new BattleCharacterSelectPage();
            await Navigation.PushModalAsync(_myModalCharacterSelectPage);
        }
    }
}