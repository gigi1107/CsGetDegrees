using System;
using System.Collections.Generic;
using System.Text;

namespace WDown.Models
{

    // The information about a Player, this allows players and monsters to be together in a single list.
    public class PlayerInfo
    {
        // TurnOrder
        public int Order;

        // guid of the original data it links back to
        public string Guid;

        // alive status, !alive will be removed from the list
        public bool Alive;

        // Sorting Order is :  Speed, Level, ExperiencePoints, PlayerType, Name, ListOrder

        // Total speed, including level and items
        public int Speed;

        // Level of character or monster
        public int Level;

        // The experience points the player has used in sorting ties...
        public int ExperiencePoints;

        // The type of player, character comes before monster
        public PlayerTypeEnum PlayerType;

        // Sorting on the alpha name if needed
        public string Name;

        // Finally if all of the above are the same, sort based on who was loaded first into the list...
        public int ListOrder;

        // The Image to the Character or Monster
        public string ImageURI;


        public int RemainingHP;

        public int TotalHP;

        public int Attack;

        public int Defense;
        public string Head;
        public string Necklass;
        public string PrimaryHand;
        public string OffHand;
        public string RightFinger;
        public string LeftFinger;
        public string Feet;

        // Need because of the instantiation below
        public PlayerInfo()
        {

        }

        // Take a character and add it to the Player
        public PlayerInfo(Character data)
        {
            PlayerType = PlayerTypeEnum.Character;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            RemainingHP = data.GetHealthCurrent();
            TotalHP = data.GetHealthMax();
            Attack = data.GetAttack();
            Defense = data.GetDefense();
            ImageURI = data.GetImageURI();
            Head = data.ItemSlotsFormatOutput();

        }

        // Take a monster and add it to the player
        public PlayerInfo(Monster data)
        {
            PlayerType = PlayerTypeEnum.Monster;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            RemainingHP = data.GetHealthCurrent();
            TotalHP = data.GetHealthMax();
            Attack = data.GetAttack();
            Defense = data.GetDefense();
            ImageURI = data.GetImageURI();
        }

        public string GetGuid()
        {
            return Guid;
        }
    }
}