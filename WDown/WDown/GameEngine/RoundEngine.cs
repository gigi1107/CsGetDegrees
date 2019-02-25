using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using WDown.Models;
using WDown.ViewModels;
using System.Diagnostics;

namespace WDown.GameEngine
{
    class RoundEngine : TurnEngine
    {
        // Hold the list of players (monster, and character by guid), and order by speed
        public List<Round.PlayerInfo> PlayerList;

        // Player currently engaged
        public Round.PlayerInfo PlayerCurrent;

        public RoundEngine()
        {
            ClearLists();
        }

        private void ClearLists()
        {
            ItemPool = new List<Item>();
            MonsterList = new List<Monster>();
        }

        // Start the round, need to get the ItemPool, and Characters
        public void StartRound()
        {
            // Start on 0, the turns will increment...
            BattleScore.RoundCount = 0;

            // Start the first round...
            NewRound();

            Debug.WriteLine("Start Round :" + BattleScore.RoundCount);
        }

        // Call to make a new set of monsters...
        public void NewRound()
        {
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;
        }

        // Add Monsters
        // Scale them to meet Character Strength...
        public void AddMonstersToRound()
        {

            // Check to see if the monster list is full, if so, no need to add more...
            if (MonsterList.Count() >= 6)
            {
                return;
            }

            // TODO, determine the character strength
            // add monsters up to that strength...
            var ScaleLevelMax = 2;
            var ScaleLevelMin = 1;

            // Make Sure Monster List exists and is loaded...
            var myMonsterViewModel = MonstersViewModel.Instance;
            myMonsterViewModel.ForceDataRefresh();

            if (myMonsterViewModel.Dataset.Count() > 0)
            {
                // Get 6 monsters
                do
                {
                    var rnd = HelperEngine.RollDice(1, myMonsterViewModel.Dataset.Count);
                    {
                        var item = new Monster(myMonsterViewModel.Dataset[rnd - 1]);

                        // Help identify which monster it is...
                        item.Name += " " + (1 + MonsterList.Count()).ToString();

                        var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
                        item.ScaleLevel(rndScale);
                        MonsterList.Add(item);
                    }

                } while (MonsterList.Count() < 6);
            }
            else
            {
                // No monsters in DB, so add 6 new ones...
                for (var i = 0; i < 6; i++)
                {
                    var item = new Monster();
                    // Help identify which monster it is...
                    item.Name += " " + MonsterList.Count() + 1;
                    MonsterList.Add(item);
                }
            }
        }

        // At the end of the round
        // Clear the Item List
        // Clear the Monster List
        public void EndRound()
        {
            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }

            ClearLists();
        }

        // Get Round Turn Order

        // Rember Who's Turn

        // RoundNextTurn
        public Round.RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                return Round.RoundEnum.GameOver;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                return Round.RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            PlayerCurrent = GetNextPlayerTurn();

            // Decide Who to Attack
            // Do the Turn         
            if (PlayerCurrent.PlayerType == Round.PlayerTypeEnum.Character)
            {
                // Get the player
                var myPlayer = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn....
                TakeTurn(myPlayer);
            }
            // Add Monster turn here...
            else if (PlayerCurrent.PlayerType == Round.PlayerTypeEnum.Monster)
            {
                // Get the player
                var myPlayer = MonsterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn....
                TakeTurn(myPlayer);
            }

            return Round.RoundEnum.NextTurn;
        }

        public Round.PlayerInfo GetNextPlayerTurn()
        {
            // Recalculate Order
            OrderPlayerListByTurnOrder();

            var PlayerCurrent = GetNextPlayerInList();
            // Lookup CurrentPlayer in the list
            // Find the player next to Current Player in order

            return PlayerCurrent;
        }

        public void OrderPlayerListByTurnOrder()
        {
            var myReturn = new List<Round.PlayerInfo>();

            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before Monster (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            MakePlayerList();

            PlayerList = PlayerList.OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }

        private void MakePlayerList()
        {
            PlayerList = new List<Round.PlayerInfo>();
            Round.PlayerInfo tempPlayer;

            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    tempPlayer = new Round.PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {

                    tempPlayer = new Round.PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }
        }

        public Round.PlayerInfo GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // No current player, so set the last one, so it rolls over to the first...
            if (PlayerCurrent == null)
            {
                PlayerCurrent = PlayerList.LastOrDefault();
            }

            // Else go and pick the next player in the list...
            for (var i = 0; i < PlayerList.Count(); i++)
            {
                if (PlayerList[i].Guid == PlayerCurrent.Guid)
                {
                    if (i < PlayerList.Count() - 1) // 0 based...
                    {
                        return PlayerList[i + 1];
                    }
                    else
                    {
                        // Return the first in the list...
                        return PlayerList.FirstOrDefault();
                    }
                }
            }

            return null;
        }

        public void PickupItemsFromPool(Character character)
        {
            // Have the character, walk the items in the pool, and decide if any are better than current one.

            // No items in the pool...
            if (ItemPool.Count < 1)
            {
                return;
            }

            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
        }

        public void GetItemFromPoolIfBetter(Character character, ItemLocationEnum setLocation)
        {
            var myList = ItemPool.Where(a => a.Location == setLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return;
            }

            var currentItem = character.GetItemByLocation(setLocation);
            if (currentItem == null)
            {
                // If no item in the slot then put on the first in the list
                character.AddItem(setLocation, myList.FirstOrDefault().Id);
                return;
            }

            foreach (var item in myList)
            {
                if (item.Value > currentItem.Value)
                {
                    // Put on the new item, which drops the one back to the pool
                    var droppedItem = character.AddItem(setLocation, item.Id);

                    // Remove the item just put on from the pool
                    ItemPool.Remove(item);

                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                    }
                }
            }
        }
    }
}
