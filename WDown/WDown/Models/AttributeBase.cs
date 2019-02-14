using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace WDown.Models
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
        // Return attributebase based on a string as the constructor.
        public AttributeBase(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                SetDefaultValues();
                return;
            }

            var myAttributes = JsonConvert.DeserializeObject<AttributeBase>(data);

            Speed = myAttributes.Speed;
            Defense = myAttributes.Defense;
            Attack = myAttributes.Attack;
            CurrentHealth = myAttributes.CurrentHealth;
            MaxHealth = myAttributes.MaxHealth;
        }

        // Return a formated string of the datastruture
        public static string GetAttributeString(AttributeBase data)
        {
            var myString = (JObject)JToken.FromObject(data);

            return myString.ToString();
        }

        // Given a string of attributes, convert them to actual attributes
        public static AttributeBase GetAttributeFromString(string data)
        {
            AttributeBase myResult;

            // Convert the string to json object
            // convert the json object to the class
            // retun the class

            // make sure the object is properly formated json for the object type
            try
            {
                myResult = JsonConvert.DeserializeObject<AttributeBase>(data);
                return myResult;
            }

            catch (Exception)
            {
                // Failed, so fall through to the return of new.
                return new AttributeBase();
            }
        }
    }
}