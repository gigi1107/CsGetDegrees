using System;

using Xamarin.Forms;

namespace week2.Models
{
    public class AttributeBase
    {

        //the speed of the character
        public int Speed { get; set; }

        public int Defense { get; set; }

        public int Attack { get; set; }

        public int CurrentHealth { get; set; }

        public int MaxHealth { get; set; }

        public string FormatOutput()
        {
            var myReturn = "Implement";
            return myReturn.Trim();
        }

        public AttributeBase()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Speed = 1;
            Defense = 1;
            Attack = 1;
            CurrentHealth = 1;
            MaxHealth = 1;
        }

        public AttributeBase(int speed, int defense, int attack,
        int currentHealth, int maxHealth)
        {
            Speed = speed;
            Defense = defense;
            Attack = attack;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int getSpeed() { return Speed; }
        public int getDefense() { return Defense; }
        public int getAttack() { return Attack; }
        public int getCurrentHealth() { return CurrentHealth; }
        public int getMaxHealth() { return MaxHealth; }
    }
}

