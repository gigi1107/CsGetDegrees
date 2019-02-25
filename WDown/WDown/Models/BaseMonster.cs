using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    public class BaseMonster : BasePlayer<BaseMonster>
    {
        // Unique Item dropped by Monster upon getting killed
        public string UniqueItem { get; set; }

        // Unique Item Drop Rate
        public double UniqueDropRate { get; set; }

 // Damage the Monster can do.
        public int Damage { get; set; }

        public BaseMonster()
        {

        }

        // Creaste a base from a monster, this reuses the guid and id
        public BaseMonster(Monster newData)
        {
            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            Update(newData);

        }

        // So when working with the database, pass Character
        public void Update(Monster newData)
        {
            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Calculate Experience Remaining based on Lookup...
            ExperienceTotal = LevelTable.Instance.LevelDetailsList[Level].Experience;

            Damage = newData.Damage;
            return;
        }
    }
}