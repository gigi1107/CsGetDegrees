﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using WDown.Models;
using WDown.ViewModels;

namespace WDown.GameEngine
{
    // Battle Engine used by Auto Battle and Regular Battle 
    // Child class of Round Engine
    // Grandchild class of Turn Engine
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

           
            HealAllCharactersAndMonsters();

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

        // Function to rebuff the Current Health to Max Health
        // for all characters and monsters 
        public void HealAllCharactersAndMonsters()
        {
            foreach (Character character in CharactersViewModel.Instance.Dataset)
            {
                character.CharacterAttribute.CurrentHealth = character.CharacterAttribute.MaxHealth;

            }

            foreach (Monster monster in MonstersViewModel.Instance.Dataset)
            {
                monster.MonsterAttribute.CurrentHealth = monster.MonsterAttribute.MaxHealth;
            }
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
            //Score needs to be saved outside the battle engine...
            //ScoresViewModel.Instance.AddAsync(BattleScore).GetAwaiter().GetResult();
        }

        // Initializes the Battle to begin
        public bool StartBattle(bool isAutoBattle)
        {
            if(!isAutoBattle)
            {
                BattleEngineClearData();
                BattleScore.AutoBattle = false;


            }
            else
            {
                BattleScore = new Score();
                BattleScore.AutoBattle = true;
                BattleMessages = new BattleMessages();

                ItemPool.Clear();
                MonsterList.Clear();
                //do not clear char list bc this has already been populated
            }

         

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
