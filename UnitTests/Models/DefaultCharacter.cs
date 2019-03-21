using WDown.Models;
using Newtonsoft.Json.Linq;

namespace UnitTests.Models.Default
{
    public static partial class DefaultModels
    {

        public static Character CharacterDefault()
        {
            var myData = new Character();

            myData.Alive = true;

            // Base information
            myData.Name = "Name";
            myData.Description = "Description";
            myData.Level = 1;
            myData.ExperienceTotal = 0;
            myData.ImageURI = null;

            myData.CharacterAttribute.Speed = 1;
            myData.CharacterAttribute.Defense = 1;
            myData.CharacterAttribute.Attack = 1;
            myData.CharacterAttribute.CurrentHealth = 1;
            myData.CharacterAttribute.MaxHealth = 1;

            // Set the strings for the items
            myData.Head = null;
            myData.Feet = null;
            myData.Necklass = null;
            myData.RightFinger = null;
            myData.LeftFinger = null;
            myData.Feet = null;

            myData.AttributeString = AttributeBase.GetAttributeString(myData.Attribute);

            return myData;
        }

    }
}
