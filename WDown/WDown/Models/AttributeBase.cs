﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Collections;

namespace WDown.Models
{
    public class AttributeBase
    {
       

        // Speed of the character
        public int Speed { get; set; }

        // Defense, or attack dodge ability 
        public int Defense { get; set; }

        // Attack value to determine attack strength during battle
        public int Attack { get; set; }

        // Current HP
        public int CurrentHealth { get; set; }


        // Maximum HP
        public int MaxHealth { get; set; }

        



        public string FormatOutput()
        {
            var myReturn = "";
            return myReturn.Trim();
        }

        // Default constructor
        public AttributeBase()
        {
            SetDefaultValues();

        }

      // Default values
        private void SetDefaultValues()
        {
            Speed = 1;
            Defense = 1;
            Attack = 1;
            CurrentHealth = 5;
            MaxHealth = 5;

        }

        // Constructor based on user given inputs
        public AttributeBase(int speed, int defense, int attack,
        int currentHealth, int maxHealth)
        {
            Speed = speed;
            Defense = defense;
            Attack = attack;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;

        }

        // Getters
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