using System;

using Xamarin.Forms;

namespace week2.Models
{
    public class Character : BaseCharacter
    {
        public AttributeBase Attribute { get; set;  }


        public Character()
        {
            Attribute = new AttributeBase();
            Alive = true;
         }

        public Character(BaseCharacter newData)
        {

            Name = newData.Name;
            Description = newData.Description;
        }
    }
}

