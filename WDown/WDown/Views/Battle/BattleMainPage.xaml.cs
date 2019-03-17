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

        /// <summary>
        /// Stand up the Page and initiate state
        /// </summary>
        public BattleMainPage(BattleViewModel viewModel)
        {
            InitializeComponent();


            BindingContext = _viewModel = viewModel;

            
            //initialize button controls
            //GameStartButton.IsVisible = true;
            GameNextButton.IsVisible = false;

            GameOverButton.IsVisible = false;

            SelectedMonster = null;
            ableToSelectMonster = false;
           

            StartGameSetting();
            DrawGameBoardAttackerDefender();
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
           
            player1.Load(filename1);
            player1.Play();

            RestButton.IsEnabled = false;
            UseItemButton.IsEnabled = false;
            ableToSelectMonster = true;

            GameNextButton.IsEnabled = true;
            _viewModel.BattleEngine.TurnType = MoveEnum.Attack;
        }


        public async void StartGame(object sender, EventArgs args)
        {
            StartGameSetting();
        }

        public void StartGameSetting()
        { 
            //GameStartButton.IsVisible = false;
            GameNextButton.IsVisible = true;
            GameNextButton.IsEnabled = true;


            //initialize next turn's players, but don't play all the way through
            _viewModel.SetPlayerCurrent();

            if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                //GameNextButton.IsEnabled = false;
             
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
    

         // Handles when user chooses an option and hits submit
        public async void SubmitClicked(object sender, EventArgs args)
        {

            Debug.WriteLine("submit button clicked\n");
            //send the selected monster info into target
            if(_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                _viewModel.BattleEngine.Target = SelectedMonster;
                if (SelectedMonster != null)
                {
                    Debug.WriteLine("backend monster selected: " + _viewModel.BattleEngine.Target.Name);
                }
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
            //do the turn 
            _viewModel.RoundNextTurn();

            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;
            Debug.WriteLine("Current round state after this turn: " + CurrentRoundState.ToString());

            //updates current player up in frontend
            OnPropertyChanged();

            //reset all these for next turn
            ableToSelectMonster = false;

            SelectedMonster = null;
          

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                
                _viewModel.NewRound();

                // Show new round and Round count
                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                // Show name of current player
                ShowModalPageMonsterList();
                Debug.WriteLine("current player: " + _viewModel.BattleEngine.PlayerCurrent.Name);



                if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
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
            if (CurrentRoundState == RoundEnum.GameOver)
            {
                //MessagingCenter.Send(this, "EndBattle");
                _viewModel.EndBattle();
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

            // Draw the Game Board
            DrawGameBoardAttackerDefender();
        }

        public void DrawGameBoardAttackerDefender()
        {
            CPName.Text = _viewModel.BattleEngine.PlayerCurrent.Name;
            CPImage.Source = _viewModel.BattleEngine.PlayerCurrent.ImageURI;
            CPHPCurr.Text = _viewModel.BattleEngine.PlayerCurrent.RemainingHP.ToString();
            CPHPTotal.Text = _viewModel.BattleEngine.PlayerCurrent.TotalHP.ToString();
            CPAttack.Text = _viewModel.BattleEngine.PlayerCurrent.Attack.ToString();
            CPDefense.Text = _viewModel.BattleEngine.PlayerCurrent.Defense.ToString();
            CPSpeed.Text = _viewModel.BattleEngine.PlayerCurrent.Speed.ToString();
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
        public async void RestClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("current player's HP before heal: ", _viewModel.BattleEngine.PlayerCurrent.RemainingHP);
            // This part of code is to populate the sound to play
            // when user clicks button 
            var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            string filename1 = "rest.mp3";
            player1.Load(filename1);
            player1.Play();

            //after the rest button is clicked, we want to disable all other actions other than submit
            RestButton.IsEnabled = false;
            UseItemButton.IsEnabled = false;
            ableToSelectMonster = false;

            AttackButton.IsEnabled = false;
            GameNextButton.IsEnabled = true;
            //TODO
            _viewModel.BattleEngine.TurnType = MoveEnum.Rest;

            //send message to backend to rest. this means writing a function in the backend for resting
            _viewModel.BattleEngine.Rest();


            Debug.WriteLine("current player's HP: ",_viewModel.BattleEngine.PlayerCurrent.RemainingHP);

           
      
            

        }
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

        public Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WDown." + filename);

            return stream;
        }
    }
}
