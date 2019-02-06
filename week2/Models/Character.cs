using System;

using Xamarin.Forms;

namespace week2.Models
{
    public class Character : BaseCharacter
    {
        public AttributeBase CharacterAttribute { get; set;  }


        public Character()
        {
            CharacterAttribute = new AttributeBase();
            Alive = true;
         }

        public Character(BaseCharacter newData)
        {

            Name = newData.Name;
            Description = newData.Description;
            Wisdom = newData.Wisdom;
            ImageURI = newData.ImageURI;
            
        }
    }
}

