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
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;

namespace WDown.Views.Battle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleMainPage : ContentPage
    {

        BattleCharacterSelectPage _myModalCharacterSelectPage;
        BattleMonsterListPage _myModalBattleMonsterListPage;

        private BattleViewModel _viewModel;

        Models.Monster SelectedMonster;
        bool ableToSelectMonster;
        bool attackButtonPressed;
        bool started;

        

        /// <summary>
        /// Stand up the Page and initiate state
        /// </summary>
        public BattleMainPage(BattleViewModel viewModel)
        {
            InitializeComponent();


            BindingContext = _viewModel = viewModel;

            
            //initialize button controls
            GameStartButton.IsVisible = true;
            GameNextButton.IsVisible = false;

            GameOverButton.IsVisible = false;

            SelectedMonster = null;
            ableToSelectMonster = false;
            started = false;


            //            var browser = new WebView();

            //var htmlSource = new HtmlWebViewSource();

            //htmlSource.Html = @"<html><body><h1>Xamarin.Forms</h1><p>Welcome to WebView.</p></body></html>";

            //HtmlBox.Source = htmlSource;


        }

        private async void OnSelectedMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if(ableToSelectMonster)
            {
                Debug.WriteLine("able to select monster");
                var data = args.SelectedItem as WDown.Models.Monster;
                SelectedMonster = data;

            }


        }

        private async void AttackClicked(object sender, EventArgs args)
        {
            var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            string filename1 = "attack.mp3";
            //player1.Load(GetStreamFromFile(filename1));
            player1.Load(filename1);
            player1.Play();
            RestButton.IsEnabled = false;
            UseItemButton.IsEnabled = false;
            ableToSelectMonster = true;
            attackButtonPressed = true;
            GameNextButton.IsEnabled = true;
            _viewModel.BattleEngine.turnType = Round.MoveEnum.Attack;
        }


        public async void StartGame(object sender, EventArgs args)
        {
            GameStartButton.IsVisible = false;
            GameNextButton.IsVisible = true;
            GameNextButton.IsEnabled = true;


            //initialize next turn's players, but don't play all the way through
            MessagingCenter.Send(this, "SetPlayerCurrent");

            if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == Round.PlayerTypeEnum.Character)
            {
                GameNextButton.IsEnabled = false;
                AttackButton.IsEnabled = true;
                RestButton.IsEnabled = true;
                UseItemButton.IsEnabled = true;

            }

            else
            {
                GameNextButton.IsEnabled = true;
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                UseItemButton.IsEnabled = false;
            }

        }
        /// <summary>
        /// Next Turn Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 

         // Handles when user chooses an option and hit submit
        public async void SubmitClicked(object sender, EventArgs args)
        {

            //TODO put message on screen that says target monster 
            //and what happened in that turn
            // Do the turn...

            
            //send the selected monster info into target
            _viewModel.BattleEngine.Target = SelectedMonster;
            if(SelectedMonster != null)

                Debug.WriteLine("backend monster selected: " + _viewModel.BattleEngine.Target.Name);

            MessagingCenter.Send(this, "RoundNextTurn");
            if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == Round.PlayerTypeEnum.Character)
            {
                GameNextButton.IsEnabled = false;
                AttackButton.IsEnabled = true;
                RestButton.IsEnabled = true;
                UseItemButton.IsEnabled = true;

            }

            else
            {
                GameNextButton.IsEnabled = true;
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                UseItemButton.IsEnabled = false;
            }

            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;

            OnPropertyChanged();

            //reset all these for next turn
            ableToSelectMonster = false;
            attackButtonPressed = false;
            SelectedMonster = null;

            // If the round is over start a new one...
            if (CurrentRoundState == Round.RoundEnum.NewRound)
            {
                MessagingCenter.Send(this, "NewRound");

                // Show new round and Round count
                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                // Show name of current player
                ShowModalPageMonsterList();
                Debug.WriteLine("current player: " + _viewModel.BattleEngine.PlayerCurrent.Name);


                if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == Round.PlayerTypeEnum.Character)
                {
                    GameNextButton.IsEnabled = false;
                    AttackButton.IsEnabled = true;
                    RestButton.IsEnabled = true;
                    UseItemButton.IsEnabled = true;

                }

                else
                {
                    GameNextButton.IsEnabled = true;
                    AttackButton.IsEnabled = false;
                    RestButton.IsEnabled = false;
                    UseItemButton.IsEnabled = false;
                }
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
        /// 
        // Handles when the characters is all dead and the game is over
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
            
        }

        // Handle when the character chooses to rest
        // Rest is only allowed when at least 1 character in party has warren

        public async void RestClicked(object sender, EventArgs args)
        {

            var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            string filename1 = "rest.mp3";
            //player1.Load(GetStreamFromFile(filename1));
            player1.Load(filename1);
            player1.Play();
            RestButton.IsEnabled = true;
            //UseItemButton.IsEnabled = false;
            //ableToSelectMonster = false;
            //attackButtonPressed = false;
            GameNextButton.IsEnabled = true;
            _viewModel.BattleEngine.turnType = Round.MoveEnum.Rest;

            // Find out whether there is a Warren already from party
            // If yes, allow resting

            bool canRest = false;
            for (int i = 0; i < CharactersViewModel.Instance.Dataset.Count; i++)
            {
                if (CharactersViewModel.Instance.Dataset[i].HasWarren == true)
                {
                    canRest = true;
                    break;
                }
                
            }
            if (canRest)
            {
                //var myPlayer = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();
                //bool result = _viewModel.BattleEngine.PlayerCurrent.PlayerRest();

                // Call Rest function 
                Debug.WriteLine("Choosing Rest... Need to call Rest from Character.cs");

            }
            else
            {
                Debug.WriteLine("Rest cannot be done. Please build warren first.");
            }

        }

        // Handles when user chooses to use item 
        public async void UseItemClicked(object sender, EventArgs args)
        {
            var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            string filename1 = "item.mp3";
            //player1.Load(GetStreamFromFile(filename1));
            player1.Load(filename1);
            player1.Play();
            Debug.WriteLine("Switching to Item Pool...");
            await Navigation.PushAsync(new BattleUseItemPage(_viewModel));

        }
        // Handle modal popping
            private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalBattleMonsterListPage)
            {
                _myModalBattleMonsterListPage = null;

                // remember to remove the event handler
                WDown.App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalCharacterSelectPage)
            {
                _myModalCharacterSelectPage = null;

                // remember to remove the event handler
                WDown.App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        // Show modal page and showing all current monsters in the list
        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalBattleMonsterListPage = new BattleMonsterListPage();
            await Navigation.PushModalAsync(_myModalBattleMonsterListPage);
        }

        // Show modal page and letting user pick 6 characters for battle
        private async void ShowModalPageCharcterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalCharacterSelectPage = new BattleCharacterSelectPage();
            await Navigation.PushModalAsync(_myModalCharacterSelectPage);
        }

        public Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WDown." + filename);

            return stream;
        }
    }
}