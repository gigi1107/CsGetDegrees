using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace week2.Models
{
    public enum AttributeEnum
    {
        // Not specified
        Unknown = 0,

        Hazel = 1, 
        
        BigWit = 2

        
        Fiver = 3,

        
        Clover = 4,

       

        Hyzenthlay = 5,
       
        CurrentHealth = 19,

        // The highest value health can go
        MaxHealth = 25,
    }

    // Helper functions for the AttribureList
    public static class AttributeList
    {

        // Returns a list of strings of the enum for Attribute
        // Removes the attributes that are not changable by Items such as Unknown, MaxHealth
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
                var myReturn = myList;
                return myReturn;
            }
        }

        // Returns a list of strings of the enum for Attribute
        // Removes the unknown
        public static List<string> GetListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
                var myReturn = myList;
                return myReturn;
            }
        }

        // Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        public static AttributeEnum ConvertStringToEnum(string value)
        {
            return (AttributeEnum)Enum.Parse(typeof(AttributeEnum), value);
        }
    }
}