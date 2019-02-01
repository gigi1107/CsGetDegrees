using System;

namespace week2.Models
{
    public class Item : Entity<Item>
    {
        public int Range { get; set; }
        public int Damage { get; set; }

        public Item()
        {
            CreateDefaultItem();
        }

        public Item(Item data)
        {
            Update(data);
        }

        public Item(string name, string description)
        {
            // Create default, and then override...
            CreateDefaultItem();

            Name = name;
            Description = description;
            //ImageURI = imageuri;

            //Range = range;
            //Damage = damage;

        
        }


        private void CreateDefaultItem()
        {
            Name = "Unknown";
            Description = "Unknown";
            ImageURI = "star.png";
            Range = 0;
            Damage = 0;

        }

        private string FormatOutput()
        {
            var myReturn = Name + " , " +
                           Description + "  " +
                           "Damage : " + Damage + " , " +
                           "Range : " + Range;

            return myReturn.Trim();
        }
        public void Update(Item newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            Range = newData.Range;
            Damage = newData.Damage;
        }




    }
}