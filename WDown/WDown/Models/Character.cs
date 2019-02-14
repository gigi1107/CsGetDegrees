using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDown.Controllers;
using Xamarin.Forms;
using SQLite;

namespace WDown.Models
{
    public class Character : BaseCharacter
    {
        public AttributeBase CharacterAttribute { get; set; }

        public Character()
        {
            CreateDefaultCharacter();
        }
        private void CreateDefaultCharacter()
        {
            CharacterAttribute = new AttributeBase();

            Alive = true;
            Name = "Unknown";
            Description = "Unknown";
            ImageURI = CharacterController.DefaultImageURI;

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

