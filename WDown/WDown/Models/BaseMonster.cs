using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WDown.Models
{
    public class BaseMonster : BasePlayer<BaseMonster>
    {
        // Unique Item dropped by Monster upon getting killed
        public string UniqueItem { get; set; }

        // Unique Item Drop Rate
        public double UniqueDropRate { get; set; }



        public BaseMonster()
        {

        }

        // Creaste a base from a monster, this reuses the guid and id
        public BaseMonster(Monster newData)
        {
            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;

            // Populate the Attributes
            AttributeString = newData.AttributeString;


            // Calculate Experience Remaining based on Lookup...
            //ExperienceTotal = LevelTable.Instance.LevelDetailsList[Level].Experience;

        }

        // So when working with the database, pass Character
        public void Update(Monster newData)
        {
            return;
        }
    }
}
