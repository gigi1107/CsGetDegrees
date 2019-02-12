using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WDown.Models
{
    public class BaseCharacter : BasePlayer<BaseCharacter>
    {
        public BaseCharacter()
        {

        }

        // Makes BaseCharacter using character for constructor
        public BaseCharacter(Character newData)
        {
            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;


            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Populate the Attributes
            AttributeString = newData.AttributeString;

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklace = newData.Necklace;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        // Update character from the passed in data
        public void Update(BaseCharacter newData)
        {
            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;


            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Populate the Attributes
            AttributeString = newData.AttributeString;

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklace = newData.Necklace;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;

        }

    }
}