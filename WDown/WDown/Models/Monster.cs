using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDown.ViewModels;
using Xamarin.Forms;
using SQLite;
namespace WDown.Models
{
    // The Monster is the higher level concept.  This is the Character with all attirbutes defined.
    public class Monster : BaseMonster
    {
        // Remaining Experience Points to give
        public int ExperienceGiven { get; set; }
        

        // Add in the actual attribute class
        public AttributeBase MonsterAttribute { get; set; }

        // Make sure Attribute is instantiated in the constructor
        public Monster()
        {
            Name = "Monster";
            MonsterAttribute = new AttributeBase();

            Alive = true;
            Level = 1;

            // Scale up to the level
            // // Implement ScaleLevel(Level);
        }

        // Passed in from creating via the Database, so use the guid passed in...
        public Monster(BaseMonster newData)
        {
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;

        }

        // For making a new one for lists etc..
        public Monster(Monster newData)
        {
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;

        }

        // Upgrades a monster to a set level
        public void ScaleLevel(int level)
        {
            // Implement
        }

        // Update the values passed in
        public new void Update(Monster newData)
        {
            // Implement

            return;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var UniqueOutput = "Implement";

            var myReturn = "Implement";

            // Implement

            myReturn += " , Unique Item : " + UniqueOutput;

            return myReturn;
        }

        // Calculate How much experience to return
        // Formula is the % of Damage done up to 100%  times the current experience
        // Needs to be called before applying damage
        public int CalculateExperienceEarned(int damage)
        {
            // Implement
            return 0;

        }

        #region GetAttributes
        // Get Attributes

        // Get Attack
        public int GetAttack()
        {
            // Base Attack
            var myReturn = MonsterAttribute.Attack;

            return myReturn;
        }

        // Get Speed
        public int GetSpeed()
        {
            // Base value
            var myReturn = MonsterAttribute.Speed;

            return myReturn;
        }

        // Get Defense
        public int GetDefense()
        {
            // Base value
            var myReturn = MonsterAttribute.Defense;

            return myReturn;
        }

        // Get Max Health
        public int GetHealthMax()
        {
            // Base value
            var myReturn = MonsterAttribute.MaxHealth;

            return myReturn;
        }

        // Get Current Health
        public int GetHealthCurrent()
        {
            // Base value
            var myReturn = MonsterAttribute.CurrentHealth;

            return myReturn;
        }

        #endregion GetMonsterAttributes

        #region Items
        // Gets the unique item (if any) from this monster when it dies...
        public Item GetUniqueItem()
        {
            var myReturn = ItemsViewModel.Instance.GetItem(UniqueItem);

            return myReturn;
        }


        #endregion Items

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public void TakeDamage(int damage)
        {
            // Implement
            return;

            // Implement   CauseDeath();
        }
    }
}