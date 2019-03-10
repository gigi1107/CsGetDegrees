using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using WDown.Models;
using WDown.ViewModels;

namespace WDown.GameEngine
{
    // Battle is the top structure

    // A battle has

    public class BattleEngine : RoundEngine
    {
        // Battle is the top structure

        // A battle has

        public class BattleEngine : RoundEngine
        {
            // The status of the actual battle, running or not (over)
            private bool isBattleRunning = false;

            // Constructor calls Init
            public BattleEngine()
            {
                BattleEngineInit();
            }

            // Sets the new state for the variables for Battle
            private void BattleEngineInit()
            {
                CharacterList.Clear();

                // Clear the rest of the data
                BattleEngineClearData();
            }

            // Sets the new state for the variables for Battle
            private void BattleEngineClearData()
            {
                BattleScore = new Score();
                BattleMessages = new BattleMessages();

                ItemPool.Clear();
                MonsterList.Clear();
                CharacterList.Clear();

                // Reset current player
                PlayerCurrent = null;
            }

            // Determine if Auto Battle is On or Off
            public bool GetAutoBattleState()
            {
                return BattleScore.AutoBattle;
            }

            // Return if the Battle is Still running
            public bool BattleRunningState()
            {
                return isBattleRunning;
            }

            // Battle is over
            // Update Battle State, Log Score to Database
            public void EndBattle()
            {
                // Set Score
                BattleScore.ScoreTotal = BattleScore.ExperienceGainedTotal;

                // Set off state
                isBattleRunning = false;

                // Save the Score to the DataStore
                ScoresViewModel.Instance.AddAsync(BattleScore).GetAwaiter().GetResult();
            }

            // Initializes the Battle to begin
            public bool StartBattle(bool isAutoBattle)
            {
                BattleEngineClearData();

                // New Battle
                // Load Characters
                BattleScore.AutoBattle = isAutoBattle;
                isBattleRunning = true;

                // Characters not Initialized, so false start...
                if (CharacterList.Count < 1)
                {
                    return false;
                }

                return true;
            }

            // Add Characters
            // Scale them to meet Character Strength...
            public bool AddCharactersToBattle()
            {
                // Check if the Character list is empty
                if (CharactersViewModel.Instance.Dataset.Count < 1)
                {
                    return false;
                }

                // Check to see if the Character list is full, if so, no need to add more...
                if (CharacterList.Count >= GameGlobals.MaxNumberPartyPlayers)
                {
                    return true;
                }

                // TODO, determine the character strength
                // add Characters up to that strength...
                var ScaleLevelMax = 3;
                var ScaleLevelMin = 1;

                // Get 6 Characters
                do
                {
                    var Data = GetRandomCharacter(ScaleLevelMin, ScaleLevelMax);
                    CharacterList.Add(Data);
                } while (CharacterList.Count < GameGlobals.MaxNumberPartyPlayers);

                return true;
            }

            public Character GetRandomCharacter(int ScaleLevelMin, int ScaleLevelMax)
            {
                var myCharacterViewModel = CharactersViewModel.Instance;

                var rnd = HelperEngine.RollDice(1, myCharacterViewModel.Dataset.Count);

                var myData = new Character(myCharacterViewModel.Dataset[rnd - 1]);

                // Help identify which Character it is...
                myData.Name += " " + (1 + CharacterList.Count).ToString();

                var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
                myData.ScaleLevel(rndScale);

                // Add Items...
                myData.Head = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Head, AttributeEnum.Unknown);
                myData.Necklass = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Necklass, AttributeEnum.Unknown);
                myData.PrimaryHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.PrimaryHand, AttributeEnum.Unknown);
                myData.OffHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.OffHand, AttributeEnum.Unknown);
                myData.RightFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.RightFinger, AttributeEnum.Unknown);
                myData.LeftFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.LeftFinger, AttributeEnum.Unknown);
                myData.Feet = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);

                return myData;
            }


            // Check Character List, if empty battle over
            // Check Monster List, if empty Round Over, then New Round

            // Round Over
            // Clear Monsters
            // Drop Items to Pool
            // Allow Pickup of Items from Pool

            // New Round
            // Item pool is empty
            // Monster List is new
            // Start Round

            // Start Round
            // Choose Attack Order
            // Walk Attack Order
            // Take Turn A attacks B


            /// <summary>
            /// Retruns a formated String of the Results of the Battle
            /// </summary>
            /// <returns></returns>
            public string GetResultsOutput()
            {

                string myResult = "" +
                        " Battle Ended" + BattleScore.ScoreTotal +
                        " Total Score :" + BattleScore.ExperienceGainedTotal +
                        " Total Experience :" + BattleScore.ExperienceGainedTotal +
                        " Rounds :" + BattleScore.RoundCount +
                        " Turns :" + BattleScore.TurnCount +
                        " Monster Kills :" + BattleScore.MonstersKilledList;

                Debug.WriteLine(myResult);

                return myResult;
            }

        }
    }
}