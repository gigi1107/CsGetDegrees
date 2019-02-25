using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDown.ViewModels;
using Xamarin.Forms;
using SQLite;
using WDown.GameEngine;
namespace WDown.Models
{
    // The Monster is the higher level concept.  This is the Character with all attirbutes defined.
    public class Monster : BaseMonster
    {
        // Remaining Experience Points to give
        public int ExperienceRemaining { get; set; }
        

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
            ScaleLevel(Level);
        }

        // Passed in from creating via the Database, so use the guid passed in...
        public Monster(BaseMonster newData)
        {
            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;

            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;

            Level = newData.Level;

            // Populate the Attributes
            MonsterAttribute = new AttributeBase(newData.AttributeString);

            // Scale up to the level
            ScaleLevel(Level);

        }

        // For making a new one for lists etc..
        public Monster(Monster newData)
        {
            // Set the strings for the items
            Update(newData);

            // Scale up to the level
            ScaleLevel(Level);

        }

        // Upgrades a monster to a set level
        public void ScaleLevel(int level)
        {
            // Calculate Experience Remaining based on Lookup...
            Level = level;

            // Get the number of points at the next level, and set it for Experience Total...
            ExperienceTotal = LevelTable.Instance.LevelDetailsList[Level + 1].Experience;
            ExperienceRemaining = ExperienceTotal;

            Damage = GetLevelBasedDamage() + LevelTable.Instance.LevelDetailsList[Level].Attack;
            MonsterAttribute.Attack = LevelTable.Instance.LevelDetailsList[Level].Attack;
            MonsterAttribute.Defense = LevelTable.Instance.LevelDetailsList[Level].Defense;
            MonsterAttribute.Speed = LevelTable.Instance.LevelDetailsList[Level].Speed;
            MonsterAttribute.MaxHealth = 5 * Level;    // 1/2 of what Characters can get per level.. 
            MonsterAttribute.CurrentHealth = MonsterAttribute.MaxHealth;

            AttributeString = AttributeBase.GetAttributeString(MonsterAttribute);
        }

        // Update the values passed in
        public new void Update(Monster newData)
        {
         

            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
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
            MonsterAttribute = new AttributeBase(newData.AttributeString);

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Calculate Experience Remaining based on Lookup...
            ExperienceTotal = newData.ExperienceTotal;
            ExperienceRemaining = newData.ExperienceRemaining;

            Damage = newData.Damage;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var UniqueOutput = "None";
            var myUnique = ItemsViewModel.Instance.GetItem(UniqueItem);
            if (myUnique != null)
            {
                UniqueOutput = myUnique.FormatOutput();
            }

            var myReturn = Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Total Experience : " + ExperienceTotal;
            myReturn += " , Unique Item : " + UniqueOutput;

            return myReturn;
        }

        // Calculate How much experience to return
        // Formula is the % of Damage done up to 100%  times the current experience
        // Needs to be called before applying damage
        public int CalculateExperienceEarned(int damage)
        {
            if (damage < 1)
            {
                return 0;
            }

            int remainingHealth = Math.Max(MonsterAttribute.CurrentHealth - damage, 0); // Go to 0 is OK...
            double rawPercent = (double)remainingHealth / (double)MonsterAttribute.CurrentHealth;
            double deltaPercent = 1 - rawPercent;
            var pointsAllocate = (int)Math.Floor(ExperienceRemaining * deltaPercent);

            // Catch rounding of low values, and force to 1.
            if (pointsAllocate < 1)
            {
                pointsAllocate = 1;
            }

            // Take away the points from remaining experience
            ExperienceRemaining -= pointsAllocate;
            if (ExperienceRemaining < 0)
            {
                pointsAllocate = 0;
            }

            return pointsAllocate;

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

        // Get the Level based damage
        // Then add in the monster damage
        public int GetDamage()
        {
            var myReturn = 0; // = GetLevelBasedDamage();  BaseDamage Already calculated in
            myReturn += Damage;

            return myReturn;
        }

        // Get the Level based damage
        // Then add the damage for the primary hand item as a Dice Roll
        public int GetDamageRollValue()
        {
            return GetDamage();
        }

        #endregion GetAttributes

        #region Items
        // Gets the unique item (if any) from this monster when it dies...
        public Item GetUniqueItem()
        {
            var myReturn = ItemsViewModel.Instance.GetItem(UniqueItem);

            return myReturn;
        }

        // Drop all the items the monster has
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Drop all Items
            Item myItem;

            myItem = ItemsViewModel.Instance.GetItem(UniqueItem);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }
            return myReturn;
        }

        #endregion Items

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            MonsterAttribute.CurrentHealth = MonsterAttribute.CurrentHealth - damage;
            if (MonsterAttribute.CurrentHealth <= 0)
            {
                MonsterAttribute.CurrentHealth = 0;
                // Death...
                CauseDeath();
            }
        }
    }
}