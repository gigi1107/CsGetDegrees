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
    public class BattleViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static BattleViewModel _instance;

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

        private string _currentPlayerName;
        public string currentPlayerName
        {
            get
            {
                return _currentPlayerName;
            }
            set
            {
                if (_currentPlayerName != BattleEngine.PlayerCurrent.Name)
                {
                    _currentPlayerName = BattleEngine.PlayerCurrent.Name;
                    OnPropertyChanged();
                }
            }
        }


        private string _currentPlayerURI;

        public string currentPlayerURI
        {
            get
            {
                return _currentPlayerURI;
            }

            set
            {
                if (_currentPlayerURI != BattleEngine.PlayerCurrent.ImageURI)
                {
                    _currentPlayerURI = BattleEngine.PlayerCurrent.ImageURI;
                    OnPropertyChanged();
                }
            }
        }



        private int _currentPlayerHPTotal;
        public int currentPlayerHPTotal
        {
            get
            {
                return _currentPlayerHPTotal;
            }
            set
            {
                if (_currentPlayerHPTotal != BattleEngine.PlayerCurrent.RemainingHP)
                {
                    _currentPlayerHPTotal = BattleEngine.PlayerCurrent.TotalHP;
                    OnPropertyChanged();
                }
            }
        }

        private int _currentPlayerHPCurrent;
        public int currentPlayerHPCurrent
        {
            get
            {
                return _currentPlayerHPCurrent;
            }
            set
            {
                if (_currentPlayerHPCurrent != BattleEngine.PlayerCurrent.RemainingHP)
                {
                    _currentPlayerHPCurrent = BattleEngine.PlayerCurrent.RemainingHP;
                    OnPropertyChanged();
                }
            }
        }

        private int _currentPlayerAttack;
        public int currentPlayerAttack
        {
            get
            {
                return _currentPlayerAttack;
            }
            set
            {
                if (_currentPlayerAttack != BattleEngine.PlayerCurrent.Attack)
                {
                    _currentPlayerAttack = BattleEngine.PlayerCurrent.Attack;
                    OnPropertyChanged();
                }
            }

        }

        private int _currentPlayerSpeed;
        public int currentPlayerSpeed
        {
            get
            {
                return _currentPlayerSpeed;
            }
            set
            {
                if (_currentPlayerSpeed != BattleEngine.PlayerCurrent.Speed)
                {
                    _currentPlayerSpeed = BattleEngine.PlayerCurrent.Speed;
                    OnPropertyChanged();
                }
            }

        }

        private int _currentPlayerDefense;
        public int currentPlayerDefense
        {
            get
            {
                return _currentPlayerDefense;
            }
            set
            {
                if (_currentPlayerDefense != BattleEngine.PlayerCurrent.Defense)
                {
                    _currentPlayerDefense = BattleEngine.PlayerCurrent.Defense;
                    OnPropertyChanged();
                }
            }

        }

        // Load the Data command
        public Command LoadDataCommand { get; set; }

        // Flag to check if the data needs refreshing
        private bool _needsRefresh;

        public BattleViewModel()
        {
            Title = "Battle";

            SelectedCharacters = new ObservableCollection<Character>();
            AvailableCharacters = new ObservableCollection<Character>();
            FightingMonsters = new ObservableCollection<Monster>();
          

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            BattleEngine = new BattleEngine();
            _currentPlayerName = "Current Character";
            _currentPlayerURI = "";
            _currentPlayerHPTotal = 0;
            _currentPlayerHPCurrent = 0;
            _currentPlayerAttack = 0;



            // Load Data
            ExecuteLoadDataCommand().GetAwaiter().GetResult();

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
                currentPlayerName = BattleEngine.PlayerCurrent.Name;
                OnPropertyChanged();
                currentPlayerURI = BattleEngine.PlayerCurrent.ImageURI;
                OnPropertyChanged();
                currentPlayerHPCurrent = BattleEngine.PlayerCurrent.RemainingHP;
                OnPropertyChanged();
                currentPlayerHPTotal = BattleEngine.PlayerCurrent.TotalHP;
                OnPropertyChanged();
                currentPlayerAttack = BattleEngine.PlayerCurrent.Attack;
                OnPropertyChanged();
                currentPlayerDefense = BattleEngine.PlayerCurrent.Defense;
                OnPropertyChanged();
                currentPlayerSpeed = BattleEngine.PlayerCurrent.Speed;
                OnPropertyChanged();
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
        public void StartBattle()
        {
            BattleEngine.StartBattle(false);
        }

        /// <summary>
        /// Call to the Engine to End the Battle
        /// </summary>
        public void EndBattle()
        {
           BattleEngine.EndBattle();
        }

        public void SetPlayerCurrent()
        {
           BattleEngine.SetPlayerCurrent();
        }

        /// <summary>
        /// Call to the Engine to Start the First Round
        /// </summary>
        public void StartRound()
        {
            BattleEngine.StartRound();
            FightingMonsters.Clear();
            foreach (var data in BattleEngine.MonsterList)
            {
                FightingMonsters.Add(new Monster(data));
            }
        }

        /// <summary>
        /// Load the Characters from the Selected List into the Battle Engine
        /// Making a copy of the character.
        /// </summary>
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

        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }
    }
}