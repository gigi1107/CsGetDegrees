﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    public class BasePlayer<T> : BasePlayerItemSlots<T>
    {
        // Where is the Item Slots, it is in the baseclass...


        // Level of the character, or difficulty level of the monster
        public int Level { get; set; }


        // Current experience gained, or to give
        public int ExperienceTotal { get; set; }

        // Whether the player is alive or dead
        public bool Alive { get; set; }

        // The AttributeString will be unpacked and stored in the top level of Character as actual attributes, 
        // but it needs to go here as a string so it can be saved to the database.
        public string AttributeString { get; set; }

        // The Dice to use when leveling up, defualt is d10
        public int HealthDice { get; set; } = 10;

        // Death
        // Alive turns to False
        public void CauseDeath()
        {
            Alive = false;
        }

        // Get Level based Damage
        // 1/4 of the Level of the Player is the base damage they do.
        public int GetLevelBasedDamage()
        {
            return (int)Math.Ceiling(Level * .25);
        }

    }
}