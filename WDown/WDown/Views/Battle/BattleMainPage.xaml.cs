﻿using System;
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
    // This is the main battle page 
    // for manual battle mode
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleMainPage : ContentPage
    {

        // List of Monster, Character, UseItem and ItemPool
        BattleCharacterSelectPage _myModalCharacterSelectPage;
        BattleMonsterListPage _myModalBattleMonsterListPage;
        BattleUseItemPage _myModalUseItemPage;
        BattleItemPool _myModalItemPoolPage;


        private BattleViewModel _viewModel;

        Models.Monster SelectedMonster;
        bool ableToSelectMonster;

        //this is for telling the player when theyve rested, used an item, etc
        string localMessages;

        /// <summary>
        // Stand up the Page and initiate state
        /// </summary>
        /// 
        public BattleMainPage(BattleViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            //Adds consumable items for chars to use
            _viewModel.BattleEngine.AddItemsToList();

            Debug.WriteLine("Items added to list in mainpage: ");
            foreach(Item item in _viewModel.BattleEngine.ItemPool)
            {
                Debug.WriteLine(item.Name);
            }

            //initialize button controls
            //GameStartButton.IsVisible = true;
            GameNextButton.IsVisible = false;

            GameOverButton.IsVisible = false;

            SelectedMonster = null;
            ableToSelectMonster = false;
           

            StartGameSetting();
            DrawGameBoardAttackerDefender();
            RefreshMonsters();
            RefreshCharacters();
        }

       
        // Upon selected, the monster is chose for target
        private async void OnSelectedMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if(ableToSelectMonster)
            {
                Debug.WriteLine("able to select monster");
                var data = args.SelectedItem as WDown.Models.Monster;
                SelectedMonster = data;

            }


        }

        // If item Pool is clicked, takes user to item pool page to equip new items
        private async void OnItemPoolClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("Switching to Item Pool...");
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalItemPoolPage = new BattleItemPool(_viewModel);
            await Navigation.PushModalAsync(_myModalItemPoolPage);
        }


        // When user clicks a monster and then click attacks, 
        // backend will roll dice and calculate the damage and the XP earned.
        private async void AttackClicked(object sender, EventArgs args)
        {
            // This is the sound implementation
            //var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            //string filename1 = "attack.mp3";
           
            //player1.Load(filename1);
            //player1.Play();

            RestButton.IsEnabled = false;
            UseItemButton.IsEnabled = false;
            ableToSelectMonster = true;
            ItemPool.IsEnabled = false;

            GameNextButton.IsEnabled = true;
            _viewModel.BattleEngine.TurnType = MoveEnum.Attack;
        }


        // Start the game
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
                ItemPool.IsEnabled = true;
                GameNextButton.IsEnabled = false;
                AttackButton.IsEnabled = true;
                RestButton.IsEnabled = true;
                UseItemButton.IsEnabled = true;

            }

            else
            {
                ItemPool.IsEnabled = false;
                GameNextButton.IsEnabled = true;
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                UseItemButton.IsEnabled = false;
            }

        }
    

         // Handles when user chooses an option and hits submit
        public async void SubmitClicked(object sender, EventArgs args)
        {

           
            //send the selected monster info into target
            if(_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                _viewModel.BattleEngine.ManualTarget = SelectedMonster;
              

            }

            //update xaml front end local message section
            if(_viewModel.BattleEngine.TurnType == MoveEnum.Rest)
            {
                LocalMessageText.Text = localMessages;
            }

            else if( _viewModel.BattleEngine.TurnType == MoveEnum.UseItem)
            {
                LocalMessageText.Text = localMessages;
            }

            else
            {
                LocalMessageText.Text = null;
            }
            //do the turn 
            _viewModel.RoundNextTurn();
            

            //update front end options after turn taken


            if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                ItemPool.IsEnabled = true;
                GameNextButton.IsEnabled = false;
                AttackButton.IsEnabled = true;
                RestButton.IsEnabled = true;
                UseItemButton.IsEnabled = true;
            }
            else
            {
                ItemPool.IsEnabled = false;
                GameNextButton.IsEnabled = true;
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                UseItemButton.IsEnabled = false;
            }
            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;
            Debug.WriteLine("Current round state after this turn: " + CurrentRoundState.ToString());

            // Draw the Game Board
            DrawGameBoardAttackerDefender();
            RefreshMonsters();
            RefreshCharacters();

            //used if im using an observable collection
            //_viewModel.SyncMonsterAndCharacterLists();


            //updates current player up in frontend
            OnPropertyChanged();

            //reset all these for next turn
            ableToSelectMonster = false;

            SelectedMonster = null;
          

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                Debug.WriteLine("NEW ROUND TRIGGERED");
                _viewModel.NewRound();

                // Show new round and Round count
                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                //push up modal monster list page to show new monsters youre fighting
                ShowModalPageMonsterList();
                //StartGameSetting();

                // Show name of current player

                Debug.WriteLine("current player: " + _viewModel.BattleEngine.PlayerCurrent.Name);

                

                if (_viewModel.BattleEngine.PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
                {
                    GameNextButton.IsEnabled = false;
                    AttackButton.IsEnabled = true;
                    RestButton.IsEnabled = true;
                    UseItemButton.IsEnabled = true;
                    ItemPool.IsEnabled = true;
                }
                else
                {
                    GameNextButton.IsEnabled = true;
                    AttackButton.IsEnabled = false;
                    RestButton.IsEnabled = false;
                    UseItemButton.IsEnabled = false;
                    ItemPool.IsEnabled = false;
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


            Debug.WriteLine("List of characters and HP remaining: ");
            foreach (Models.Character character in _viewModel.BattleEngine.CharacterList)
            {

                Debug.WriteLine("Name : " + character.Name);
                Debug.WriteLine("HP : " + character.CharacterAttribute.getCurrentHealth() + "/" + character.CharacterAttribute.getMaxHealth());
               
            }
            Debug.WriteLine("\n");
            Debug.WriteLine("List of monsters and HP remaining: ");
            foreach (Models.Monster monster in _viewModel.BattleEngine.MonsterList)
            {

                Debug.WriteLine("Name : " + monster.Name);
                Debug.WriteLine("HP : " + monster.MonsterAttribute.getCurrentHealth() + "/" + monster.MonsterAttribute.getMaxHealth());

            }

           



        }

        // Populate the CP Info for the XAML front end
        public void DrawGameBoardAttackerDefender()
        {
            CPName.Text = _viewModel.BattleEngine.PlayerCurrent.Name;
            CPImage.Source = _viewModel.BattleEngine.PlayerCurrent.ImageURI;
            CPHPCurr.Text = _viewModel.BattleEngine.PlayerCurrent.RemainingHP.ToString();
            CPHPTotal.Text = _viewModel.BattleEngine.PlayerCurrent.TotalHP.ToString();
            CPAttack.Text = _viewModel.BattleEngine.PlayerCurrent.Attack.ToString();
            CPDefense.Text = _viewModel.BattleEngine.PlayerCurrent.Defense.ToString();
            CPSpeed.Text = _viewModel.BattleEngine.PlayerCurrent.Speed.ToString();
            CPLevel.Text = _viewModel.BattleEngine.PlayerCurrent.Level.ToString();
        }

        // Refresh characters to show updated stats
        public void RefreshCharacters()
        {
            SelectedCharactersView.ItemsSource = null;
            SelectedCharactersView.ItemsSource = _viewModel.BattleEngine.CharacterList;

            Debug.WriteLine("This is what the characterList looks like in the backend at refreshCharacters Called: ");
            foreach(Models.Character character in _viewModel.BattleEngine.CharacterList)
            {
                Debug.WriteLine(character.Name + " Level: " + character.Level);
            }
            

            //SelectedCharactersView.ItemsSource = _viewModel.SelectedCharacters;
        }

        // REgresh monsters to show updated stats
        public void RefreshMonsters()
        {
            SelectedMonstersView.ItemsSource = null;
            SelectedMonstersView.ItemsSource = _viewModel.BattleEngine.MonsterList;

            

           

        }

        //monster variables for front end
    
        

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage(string message)
        {
            Debug.WriteLine("Message: " + message);

            //MessageText.Text = message + " \n" + MessageText.Text;
            MessageText.Text = message + " \n" ;
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
            Debug.WriteLine("current player's HP before heal: "+ _viewModel.BattleEngine.PlayerCurrent.RemainingHP);
            // This part of code is to populate the sound to play
            // when user clicks button 
            //var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            //string filename1 = "rest.mp3";
            //player1.Load(filename1);
            //player1.Play();

            //after the rest button is clicked, we want to disable all other actions other than submit
            RestButton.IsEnabled = false;
            UseItemButton.IsEnabled = false;
            ableToSelectMonster = false;

            AttackButton.IsEnabled = false;
            GameNextButton.IsEnabled = true;
            ItemPool.IsEnabled = false;


            _viewModel.BattleEngine.ManualTarget = null;
            SelectedMonster = null;
            SelectedMonstersView.SelectedItem = null;

            //change turn type to be rest
            _viewModel.BattleEngine.TurnType = MoveEnum.Rest;


            _viewModel.BattleEngine.PlayerCurrent.RemainingHP = _viewModel.BattleEngine.PlayerCurrent.TotalHP;

            Models.Character character =  _viewModel.BattleEngine.CharacterList.FirstOrDefault(x => x.Name == _viewModel.BattleEngine.PlayerCurrent.Name);

            foreach(Models.Character c in _viewModel.BattleEngine.CharacterList)
            {
                if(c == character)
                {
                    c.CharacterAttribute.CurrentHealth = c.CharacterAttribute.MaxHealth;
                }
            }
            Debug.WriteLine("current player's HP: "+_viewModel.BattleEngine.PlayerCurrent.RemainingHP);
            DrawGameBoardAttackerDefender();
            localMessages = "Character Rested and regained full HP!";

        }
        public async void ShowUseItemModal(object sender, EventArgs args)
        {
            //var player1 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            //string filename1 = "item.mp3";
            ////player1.Load(GetStreamFromFile(filename1));
            //player1.Load(filename1);
            //player1.Play();
           
            Debug.WriteLine("Switching to Item Inventory...");
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalUseItemPage = new BattleUseItemPage(_viewModel);
            await Navigation.PushModalAsync(_myModalUseItemPage);
            localMessages = "Items equipped! ";

            //swithcing turn type
            _viewModel.BattleEngine.TurnType = MoveEnum.UseItem;

            //button handling is handled in handle modal popping method

        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalBattleMonsterListPage)
            {
                DrawGameBoardAttackerDefender();
                StartGameSetting();
                RefreshMonsters();
                RefreshCharacters();
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

            if(e.Modal == _myModalUseItemPage)
            {
               

                _myModalUseItemPage = null;
                WDown.App.Current.ModalPopping -= HandleModalPopping;
                localMessages = "Character Healed!!";

                //refresh curr player stats
                //cannot attack or rest
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                GameNextButton.IsEnabled = true;
                DrawGameBoardAttackerDefender();
                ItemPool.IsEnabled = false;
            }

            if(e.Modal == _myModalItemPoolPage)
            {
                _myModalUseItemPage = null;
                WDown.App.Current.ModalPopping -= HandleModalPopping;
                localMessages = "Items Equipped! ";

                //refresh currPlayer stats
                DrawGameBoardAttackerDefender();

               
                //cannot attack or rest
                AttackButton.IsEnabled = false;
                RestButton.IsEnabled = false;
                GameNextButton.IsEnabled = true;
                //refresh
                DrawGameBoardAttackerDefender();
            }
        }

        // Show the modal for monsters
        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalBattleMonsterListPage = new BattleMonsterListPage();
            await Navigation.PushModalAsync(_myModalBattleMonsterListPage);
        }

        // Show the modal for characters
        private async void ShowModalPageCharacterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            WDown.App.Current.ModalPopping += HandleModalPopping;
            _myModalCharacterSelectPage = new BattleCharacterSelectPage();
            await Navigation.PushModalAsync(_myModalCharacterSelectPage);
        }

        // This function is for the sound functionality 
        // Implemented in Hackathon
        public Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WDown." + filename);

            return stream;
        }
    }
}
