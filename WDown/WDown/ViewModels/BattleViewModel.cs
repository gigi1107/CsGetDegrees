using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WDown.Models;
using WDown.Views;
using System.Linq;
using WDown.Controllers;
using WDown.GameEngine;
using WDown.Views.Battle;

namespace WDown.ViewModels
{
    // Battle View Model
    public class BattleViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static BattleViewModel _instance;

        // Initialize
        public static BattleViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleViewModel();
                }
                return _instance;
            }
        }

        // Hold a copy of the Battle Engine
        public BattleEngine BattleEngine;

        // The Character List ot interact with
        // Class for the SelectedCharacters
        public ObservableCollection<Character> SelectedCharacters { get; set; }

        // Class for the AvailableCharacters
        public ObservableCollection<Character> AvailableCharacters { get; set; }

        //added class for the fightingmonsters to display in UI
        public ObservableCollection<Monster> FightingMonsters { get; set; }



        // Load the Data command
        public Command LoadDataCommand { get; set; }

        // Flag to check if the data needs refreshing
        private bool _needsRefresh;

        // Constructor
        public BattleViewModel()
        {
            Title = "Battle";

            SelectedCharacters = new ObservableCollection<Character>();
            AvailableCharacters = new ObservableCollection<Character>();
            FightingMonsters = new ObservableCollection<Monster>();
          
            
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            BattleEngine = new BattleEngine();
       


            // Load Data
            ExecuteLoadDataCommand().GetAwaiter().GetResult();


            // Data operations
            MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "AddSelectedCharacter", async (obj, data) =>
            {
                SelectedListAdd(data);
            });

            MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "RemoveSelectedCharacter", async (obj, data) =>
            {
                SelectedListRemove(data);
            });

            MessagingCenter.Subscribe<BattleMonsterListPage>(this, "StartBattle", async (obj) =>
            {
                StartBattle();
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "EndBattle", async (obj) =>
            {
                EndBattle();
            });

            MessagingCenter.Subscribe<BattleMonsterListPage>(this, "StartRound", async (obj) =>
            {
                StartRound();
            });

            MessagingCenter.Subscribe<BattleMonsterListPage>(this, "LoadCharacters", async (obj) =>
            {
                LoadCharacters();
            });
            MessagingCenter.Subscribe<BattleMainPage>(this, "SetPlayerCurrent", async (obj) =>
            {
                SetPlayerCurrent();

            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "RoundNextTurn", async (obj) =>
            {
                RoundNextTurn();
            

            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "NewRound", async (obj) =>
            {
                NewRound();
            });
            MessagingCenter.Subscribe<BattleMonsterListPage>(this, "NewRound", async (obj) =>
            {
                NewRound();
            });
        }

        /// <summary>
        /// Call to the Engine to Start the Battle
        /// </summary>
        // Start a new battle
        public void StartBattle()
        {
            BattleEngine.StartBattle(false);
        }

        /// <summary>
        /// Call to the Engine to End the Battle
        /// </summary>
        // End Battle
        public void EndBattle()
        {
           BattleEngine.EndBattle();
        }

        // Set PlayerCurrent
        public void SetPlayerCurrent()
        {
           BattleEngine.SetPlayerCurrent();
        }

        /// <summary>
        /// Call to the Engine to Start the First Round
        /// </summary>
        // Start a round, clear lists
        public void StartRound()
        {
            BattleEngine.StartRound();
            FightingMonsters.Clear();
            foreach (var data in BattleEngine.MonsterList)
            {
                FightingMonsters.Add(new Monster(data));
            }
        }

        // Used to sync observables to actual
        public void SyncMonsterAndCharacterLists()
        {
            FightingMonsters.Clear();
            // Add monsters to battle
            foreach (var monster in BattleEngine.MonsterList)
            {
                FightingMonsters.Add(monster);


            }

            SelectedCharacters.Clear();
            // Add characters to battle
            foreach (var character in BattleEngine.CharacterList)
            {
                SelectedCharacters.Add(character);
            }
            return;
        }


        /// <summary>
        /// Load the Characters from the Selected List into the Battle Engine
        /// Making a copy of the character.
        /// </summary>
        // Load all characters
        public void LoadCharacters()
        {

            //differentiate multiples of the same character by appending a number
            //and add them to the characterList
            for(int i = 0; i < SelectedCharacters.Count; i++)
            {
                if(i == 0)
                {
                    SelectedCharacters[i].Name = SelectedCharacters[i].Name + " " + (i+1).ToString();
                    
                }
                else
                {
                    //take off last number and append current number
                    string word = SelectedCharacters[i].Name;
                    string[] words = word.Split(' ');
                    string keep = words[0];
                    SelectedCharacters[i].Name = keep + ' ' + (i + 1).ToString();

                }
               
               
                BattleEngine.CharacterList.Add(new Character(SelectedCharacters[i]));
               

               
            }
            //some weird bug with population, so cleared selected chars and repopulated
            SelectedCharacters.Clear();
            foreach (Character character in BattleEngine.CharacterList)
            {
                SelectedCharacters.Add(new Character(character));
            }



        }

        /// <summary>
        /// Call to the engine for the NextRound to Happen
        /// </summary>
        public void RoundNextTurn()
        {
            BattleEngine.RoundNextTurn();
        }

        /// <summary>
        /// Call to the Engine for a New Round to Happen
        /// </summary>
        public void NewRound()
        {
            BattleEngine.NewRound();

        }

        #region DataOperations
        // Call to database operation for delete
        public bool SelectedListRemove(Character data)
        {
            SelectedCharacters.Remove(data);
            return true;
        }

        // Call to database operation for add
        public bool SelectedListAdd(Character data)
        {
            SelectedCharacters.Add(data);
            return true;
        }

        // Call to database to ensure most recent
        public Character Get(string id)
        {
            var myData = SelectedCharacters.FirstOrDefault(arg => arg.Id == id);
            if (myData == null)
            {
                return null;
            }

            return myData;

        }
        // Call to database to ensure most recent
        public Character GetByName(string name)
        {
            var myData = SelectedCharacters.FirstOrDefault(arg => arg.Name == name);
            if (myData == null)
            {
                return null;
            }

            return myData;

        }
        #endregion DataOperations


        // Clear current lists so they can get rebuilt
        public void ClearCharacterLists()
        {
            AvailableCharacters.Clear();
            SelectedCharacters.Clear();

            ExecuteLoadDataCommand();
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        // Command that Loads the Data
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {


                // SelectedCharacters, no need to change them.

                // Reload the Character List from the Character View Moel
                AvailableCharacters.Clear();
                var availableCharacters = CharactersViewModel.Instance.Dataset;
                foreach (var data in availableCharacters)
                {
                    AvailableCharacters.Add(data);
                }

                //for refreshing characters and monsters on battle main page
                SelectedCharacters.Clear();
                var selectedChars = BattleEngine.CharacterList;
                foreach (var data in selectedChars)
                {
                    SelectedCharacters.Add(data);
                }

                FightingMonsters.Clear();
                var fightingMonsters = BattleEngine.MonsterList;
                foreach (var data in fightingMonsters)
                {
                    FightingMonsters.Add(data);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }

        // Refresh
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }
    }
}