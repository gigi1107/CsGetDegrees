using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using WDown.ViewModels;
using Xamarin.Forms;

namespace WDown.Models
{
    // Folding ItemSolts into the overall class inheritance, to show approach.  
    // C# does not support multiple inheritance
    // Could use simulated by using a pattern of interfaces, but for this, just doing it the simple way...

    public class BasePlayerItemSlots<T> : Entity<T>
    {
        // Item is a string referencing the database table
        public string Head { get; set; }

        // Feet is a string referencing the database table
        public string Feet { get; set; }

        // Necklasss is a string referencing the database table
        public string Necklass { get; set; }

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; }

        // Offhand is a string referencing the database table
        public string OffHand { get; set; }

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; }

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; }

        // This uses relfection, to get the property from a string
        // Then based on the property, it gets the value which will be the string pointing to the item
        // Then it calls to the view model who has the list of items, and asks for it
        // then it returns the formated string for the Item, and Value.
        private string FormatOutputSlot(string slot)
        {
            var myReturn = slot + " : ";

            var myType = this.GetType();
            var myProperty = myType.GetProperty(slot);
            var myPropertyValue = myProperty.GetValue(this, null);

            if (myPropertyValue == null)
            {
                myReturn += "None";
                return myReturn;
            }

            var myValue = myPropertyValue.ToString();
            var myData = ItemsViewModel.Instance.GetItem(myValue);
            if (myData == null)
            {
                myReturn += "None";
            }
            else
            {
                myReturn += myData.Value.ToString();
            }
            return myReturn;
        }

        public string ItemSlotsFormatOutput()
        {
            var myReturn = "";

            // Need to lookup the Items at the locations, and return them.
            myReturn = FormatOutputSlot("Head") + " , ";
            myReturn += FormatOutputSlot("Necklass") + " , ";
            myReturn += FormatOutputSlot("PrimaryHand") + " , ";
            myReturn += FormatOutputSlot("OffHand") + " , ";
            myReturn += FormatOutputSlot("RightFinger") + " , ";
            myReturn += FormatOutputSlot("LeftFinger") + " , ";
            myReturn += FormatOutputSlot("Feet") + " , ";

            return myReturn.Trim();
        }

        public string GetHead()
        {
            var myReturn = FormatOutputSlot("Head");
            return myReturn;
        }

        public string GetNecklass()
        {
            var myReturn = FormatOutputSlot("Necklass");
            return myReturn;
        }

        public string GetPrimaryHand()
        {
            var myReturn = FormatOutputSlot("PrimaryHand");
            return myReturn;
        }

        public string GetOffHand()
        {
            var myReturn = FormatOutputSlot("OffHand");
            return myReturn;
        }


    }
}