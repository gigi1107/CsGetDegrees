using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using WDown.Models;
using WDown.ViewModels;
using System.Linq;

namespace WDown.GameEngine
{ /// * 
    // * Need to decide who takes the next turn
    // * Target to Attack
    // * Should Move, or Stay put (can hit with weapon range?)
    // * Death
    // * Manage Round...
    // * /

    public class TurnEngine
    {
        #region Properties
        // Holds the official score
        public Score BattleScore = new Score();

        public BattleMessages BattleMessages = new BattleMessages();

        public List<Item> ItemPool = new List<Item>();

        //public List<Item> ItemList = new List<Item>();

        public List<Monster> MonsterList = new List<Monster>();
        public List<Character> CharacterList = new List<Character>();

        public Round.PlayerInfo CurrentAttacker;
        public Round.PlayerInfo CurrentDefender;

        public Monster Target;
        //this gets chaged in front end based on button press
        public Round.MoveEnum turnType;

        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Properties

        // Character Attacks...
        public bool TakeTurn(Character Attacker)
        {
            // Choose Move or Attack TODO MAKE THIS SO THAT THE CHARACTER ACTUALLY SELECTS MONSTER TP ATTACK

            // For Attack, Choose Who
            //If it is an autobattle, proceed noramlly
            if(BattleScore.AutoBattle)
            {
                var Target = AttackChoice(Attacker);


                if (Target == null)
                {
                    return false;
                }
                // Do Attack
                var AttackScore = Attacker.Level + Attacker.GetAttack();
                var DefenseScore = Target.GetDefense() + Target.Level;
                TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);


            }

            else if(turnType == Round.MoveEnum.Attack)
            {
                if (Target == null)
                {
                    return false;
                }
                // Do Attack
                var AttackScore = Attacker.Level + Attacker.GetAttack();
                var DefenseScore = Target.GetDefense() + Target.Level;
                TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);

            }
            CurrentAttacker = new Round.PlayerInfo(Attacker);
            CurrentDefender = new Round.PlayerInfo(Target);




            return true;
        }

        // Monster Attacks...
        public bool TakeTurn(Monster Attacker)
        {
            // Choose Attack, Rest, or Use Item
            //here we want to use an ENUM-- Move Enum
     
                // For Attack, Choose Who
                var Target = AttackChoice(Attacker);

                if (Target == null)
                {
                    return false;
                }

                // Do Attack
                var AttackScore = Attacker.Level + Attacker.GetAttack();
                var DefenseScore = Target.GetDefense() + Target.Level;
                TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);
           
            
            CurrentAttacker = new Round.PlayerInfo(Attacker);
            CurrentDefender = new Round.PlayerInfo(Target);

            return true;
        }

        // Monster Attacks Character
        public bool TurnAsAttack(Monster Attacker, int AttackScore, Character Target, int DefenseScore)
        {
            BattleMessages.TurnMessage = string.Empty;
            BattleMessages.TurnMessageSpecial = string.Empty;
            BattleMessages.AttackStatus = string.Empty;

            BattleMessages.PlayerType = Round.PlayerTypeEnum.Monster;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            // Choose who to attack

            BattleMessages.TargetName = Target.Name;
            BattleMessages.AttackerName = Attacker.Name;

            var HitSuccess = RollToHitTarget(AttackScore, DefenseScore);

            Debug.WriteLine(BattleMessages.GetTurnMessage());

            if (BattleMessages.HitStatus == HitStatusEnum.Miss)
            {
                return true;
            }

            if (BattleMessages.HitStatus == HitStatusEnum.CriticalMiss)
            {
                return true;
            }

            // It's a Hit or a Critical Hit
            if (BattleMessages.HitStatus == HitStatusEnum.Hit || BattleMessages.HitStatus == HitStatusEnum.CriticalHit)
            {
                //Calculate Damage
                BattleMessages.DamageAmount = Attacker.GetDamageRollValue();

                BattleMessages.DamageAmount += GameGlobals.ForceCharacterDamangeBonusValue;   // Add the Forced Damage Bonus (used for testing...)

                if (GameGlobals.EnableCriticalHitDamage)
                {
                    if (BattleMessages.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        //2x damage
                        BattleMessages.DamageAmount += BattleMessages.DamageAmount;
                    }
                }

                Target.TakeDamage(BattleMessages.DamageAmount);
            }

            BattleMessages.CurrentHealth = Target.CharacterAttribute.CurrentHealth;
            BattleMessages.TurnMessageSpecial = BattleMessages.GetCurrentHealthMessage();

            // Check for alive
            if (Target.Alive == false)
            {
                // Remover target from list...
                CharacterList.Remove(Target);

                // Mark Status in output
                BattleMessages.TurnMessageSpecial = " and causes death";

                // Add the monster to the killed list
                BattleScore.CharacterAtDeathList.Add(Target);

                // Drop Items to item Pool
                var myItemList = Target.DropAllItems();

                // Add to Score
                foreach (var item in myItemList)
                {
                    BattleScore.ItemsDroppedList.Add(item);
                    BattleMessages.TurnMessageSpecial += " Item " + item.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            BattleMessages.TurnMessage = Attacker.Name + BattleMessages.AttackStatus + Target.Name + BattleMessages.TurnMessageSpecial;
            Debug.WriteLine(BattleMessages.TurnMessage);

            return true;
        }

        // Character attacks Monster
        //here we wanto to set monster target as something the user selects
        public bool TurnAsAttack(Character Attacker, int AttackScore, Monster Target, int DefenseScore)
        {
            BattleMessages.TurnMessage = string.Empty;
            BattleMessages.TurnMessageSpecial = string.Empty;
            BattleMessages.AttackStatus = string.Empty;
            BattleMessages.LevelUpMessage = string.Empty;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            // Choose who to attack

            BattleMessages.TargetName = Target.Name;
            BattleMessages.AttackerName = Attacker.Name;

            var HitSuccess = RollToHitTarget(AttackScore, DefenseScore);

            Debug.WriteLine(BattleMessages.GetTurnMessage());

            if (BattleMessages.HitStatus == HitStatusEnum.Miss)
            {
                return true;
            }

            if (BattleMessages.HitStatus == HitStatusEnum.CriticalMiss)
            {

                if (GameGlobals.EnableCriticalMissProblems)
                {
                    BattleMessages.TurnMessage += DetermineCriticalMissProblem(Attacker);
                }
                return true;
            }

            // It's a Hit or a Critical Hit
            if (BattleMessages.HitStatus == HitStatusEnum.Hit || BattleMessages.HitStatus == HitStatusEnum.CriticalHit)
            {
                //Calculate Damage
                BattleMessages.DamageAmount = Attacker.GetDamageRollValue();

                BattleMessages.DamageAmount += GameGlobals.ForceCharacterDamangeBonusValue;   // Add the Forced Damage Bonus (used for testing...)

                if (GameGlobals.EnableCriticalHitDamage)
                {
                    if (BattleMessages.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        //2x damage
                        BattleMessages.DamageAmount += BattleMessages.DamageAmount;
                    }
                }

                Target.TakeDamage(BattleMessages.DamageAmount);

                var experienceEarned = Target.CalculateExperienceEarned(BattleMessages.DamageAmount);

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    BattleMessages.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetHealthMax();
                    Debug.WriteLine(BattleMessages.LevelUpMessage);
                }

                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            BattleMessages.TurnMessageSpecial = " remaining health is " + Target.MonsterAttribute.CurrentHealth;

            // Check for alive
            if (Target.Alive == false)
            {
                // Remove target from list...
                MonsterList.Remove(Target);

                // Mark Status in output
                BattleMessages.TurnMessageSpecial = " and causes death";

                // Add one to the monsters killd count...
                BattleScore.MonsterSlainNumber++;

                // Add the monster to the killed list
                BattleScore.MonstersKilledList.Add(Target);

                // Drop Items to item Pool
                var myItemList = Target.DropAllItems();

                // If Random drops are enabled, then add some....
                myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

                // Add to Score
                foreach (var item in myItemList)
                {
                    BattleScore.ItemsDroppedList.Add(item);
                    BattleMessages.TurnMessageSpecial += " Item " + item.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            BattleMessages.TurnMessage = Attacker.Name + BattleMessages.AttackStatus + Target.Name + BattleMessages.TurnMessageSpecial;
            Debug.WriteLine(BattleMessages.TurnMessage);

            return true;
        }

        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {

            var d20 = HelperEngine.RollDice(1, 20);

            // Turn On UnitTestingSetRoll
            if (GameGlobals.ForceRollsToNotRandom)
            {
                // Don't let it be 0, if it was not initialized...
                if (GameGlobals.ForceToHitValue < 1)
                {
                    GameGlobals.ForceToHitValue = 1;
                }

                d20 = GameGlobals.ForceToHitValue;
            }

            if (d20 == 1)
            {
                // Force Miss
                BattleMessages.HitStatus = HitStatusEnum.CriticalMiss;
                return BattleMessages.HitStatus;
            }

            if (d20 == 20)
            {
                // Force Hit
                BattleMessages.HitStatus = HitStatusEnum.CriticalHit;
                return BattleMessages.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessages.AttackStatus = " misses ";
                // Miss
                BattleMessages.HitStatus = HitStatusEnum.Miss;
                BattleMessages.DamageAmount = 0;
            }
            else
            {
                // Hit
                BattleMessages.HitStatus = HitStatusEnum.Hit;
            }

            return BattleMessages.HitStatus;
        }

        // Decide which to attack
        public Monster AttackChoice(Character data)
            //ACTUALLY HERE IS THE PART WE WANT TO MAKE MANUAL SELECTION
            //how do we get front end selection to work with backend?
            //implement button function to return the attack choice
            //TODO

        {
            //want to run automatically if in Autobattle
            if(BattleScore.AutoBattle)
            {
                if (MonsterList == null)
                {
                    return null;
                }

                if (MonsterList.Count < 1)
                {
                    return null;
                }

                // Select first one to hit in the list for now...
                // Attack the WEAKEST (lowest HP) Monster first 
                var DefenderWeakest = MonsterList.OrderBy(m => m.MonsterAttribute.CurrentHealth).FirstOrDefault();
                if (DefenderWeakest.Alive)
                {
                    return DefenderWeakest;
                }

               
            }

            if(!BattleScore.AutoBattle && turnType == Round.MoveEnum.Attack)
            {
                if (MonsterList == null)
                {
                    return null;
                }

                if (MonsterList.Count < 1)
                {
                    return null;
                }
                return Target;
                
            }
            return null;

        }

        // Decide which to attack
        public Character AttackChoice(Monster data)
        {
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // For now, just use a simple selection of the first in the list.
            // Later consider, strongest, closest, with most Health etc...

            foreach (var Defender in CharacterList)
            {
                if (Defender.Alive)
                {
                    // Select first one to hit in the list for now...
                    return Defender;
                }
            }
            return null;
        }

        // Will drop between 1 and 4 items from the item set...
        public List<Item> GetRandomMonsterItemDrops(int round)
        {
            var myList = new List<Item>();

            if (!GameGlobals.AllowMonsterDropItems)
            {
                return myList;
            }

            var myItemsViewModel = ItemsViewModel.Instance;

            if (myItemsViewModel.Dataset.Count > 0)
            {
                // Random is enabled so build up a list of items dropped...
                var ItemCount = HelperEngine.RollDice(1, 4);
                for (var i = 0; i < ItemCount; i++)
                {
                    var rnd = HelperEngine.RollDice(1, myItemsViewModel.Dataset.Count);
                    var itemBase = myItemsViewModel.Dataset[rnd - 1];
                    var item = new Item(itemBase);
                    item.ScaleLevel(round);

                    // Make sure the item is added to the global list...
                    var myItem = ItemsViewModel.Instance.CheckIfItemExists(item);
                    if (myItem == null)
                    {
                        // Item does not exist, so add it to the datstore
                        ItemsViewModel.Instance.AddAsync(item);
                    }
                    else
                    {
                        // Swap them becaues it already exists, no need to create a new one...
                        item = myItem;
                    }

                    // Add the item to the local list...
                    myList.Add(item);
                }
            }

            return myList;
        }

        public string DetermineCriticalMissProblem(Character attacker)
        {
            if (attacker == null)
            {
                return " Invalid Character ";
            }

            var myReturn = " Nothing Bad Happened ";
            Item droppedItem;

            // It may be a critical miss, roll again and find out...
            var rnd = HelperEngine.RollDice(1, 10);
            /*
                1. Primary Hand Item breaks, and is lost forever
                2-4, Character Drops the Primary Hand Item back into the item pool
                5-6, Character drops a random equipped item back into the item pool
                7-10, Nothing bad happens, luck was with the attacker
             */

            switch (rnd)
            {
                case 1:
                    myReturn = " Luckly, nothing to drop from " + ItemLocationEnum.PrimaryHand;
                    var myItem = ItemsViewModel.Instance.GetItem(attacker.PrimaryHand);
                    if (myItem != null)
                    {
                        myReturn = " Item " + myItem.Name + " from " + ItemLocationEnum.PrimaryHand + " Broke, and lost forever";
                    }

                    attacker.PrimaryHand = null;
                    break;

                case 2:
                case 3:
                case 4:
                    // Put on the new item, which drops the one back to the pool
                    myReturn = " Luckly, nothing to drop from " + ItemLocationEnum.PrimaryHand;
                    droppedItem = attacker.AddItem(ItemLocationEnum.PrimaryHand, null);
                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                        myReturn = " Dropped " + droppedItem.Name + " from " + ItemLocationEnum.PrimaryHand;
                    }
                    break;

                case 5:
                case 6:
                    var LocationRnd = HelperEngine.RollDice(1, ItemLocationList.GetListCharacter.Count);
                    var myLocationEnum = ItemLocationList.GetLocationByPosition(LocationRnd);
                    myReturn = " Luckly, nothing to drop from " + myLocationEnum;

                    // Put on the new item, which drops the one back to the pool
                    droppedItem = attacker.AddItem(myLocationEnum, null);
                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                        myReturn = " Dropped " + droppedItem.Name + " from " + myLocationEnum;
                    }
                    break;
            }

            return myReturn;
        }
    }
}
