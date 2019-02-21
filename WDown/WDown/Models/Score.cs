using System;

namespace WDown.Models
{
    public class Score : Entity<Score>
    {
        // This battle number, incremental int from the last int in the database
        public int BattleNumber { get; set; }

        // Total Score
        public int ScoreTotal { get; set; }

        // The Date the game played, and when the score was saved
        public DateTime GameDate { get; set; }

        // Tracks if auto battle is true, or if user battle = false
        public bool AutoBattle { get; set; }

        // The number of turns the battle took to finish
        public int TurnCount { get; set; }

        // The number of rounds the battle took to finish
        public int RoundCount { get; set; }

        // The count of monsters slain during battle
        public int MonsterSlainNumber { get; set; }

        // The total experience points all the characters received during the battle
        public int ExperienceGainedTotal { get; set; }

        // A list of all the characters at the time of death and their stats.  
        // Only use Get only, set will be done by the Add feature.
        public string CharacterAtDeathList { get; set; }

        // All of the monsters killed and their stats. 
        // Only use Get only, set will be done by the Add feature.
        public string MonstersKilledList { get; set; }

        // All of the items dropped and their stats. 
        // Only use Get only, set will be done by the Add feature.
        public string ItemsDroppedList { get; set; }

        // Instantiate new Score
        public Score()
        {
            CreateDefaultScore();

        }

        private void CreateDefaultScore()
        {
            Name = "Score";

            Description = "This is a Score.";

            ImageURI = "https://i.imgur.com/DeFwZPA.png";

            BattleNumber = 0;
            ScoreTotal = 0;
            GameDate = DateTime.Today;
            AutoBattle = false;
            TurnCount = 0;
            RoundCount = 0;
            MonsterSlainNumber = 0;
            ExperienceGainedTotal = 0;
            CharacterAtDeathList = "";
            MonstersKilledList = "";
            ItemsDroppedList = "";
        }

        public Score(Score newData)
        {
            CreateDefaultScore();

            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;

            BattleNumber = newData.BattleNumber;
            ScoreTotal = newData.ScoreTotal;
            GameDate = newData.GameDate;
            AutoBattle = newData.AutoBattle;
            TurnCount = newData.TurnCount;
            RoundCount = newData.RoundCount;
            MonsterSlainNumber = newData.MonsterSlainNumber;
            ExperienceGainedTotal = newData.ExperienceGainedTotal;
            CharacterAtDeathList = newData.CharacterAtDeathList;
            MonstersKilledList = newData.MonstersKilledList;
            ItemsDroppedList = newData.ItemsDroppedList;
        }

        // Update the score based on the passed in values.
        public void Update(Score newData)
        {
            if (newData == null)
            {
                return;
            }
            BattleNumber = newData.BattleNumber;
            ScoreTotal = newData.ScoreTotal;
            GameDate = newData.GameDate;
            AutoBattle = newData.AutoBattle;
            TurnCount = newData.TurnCount;
            RoundCount = newData.RoundCount;
            MonsterSlainNumber = newData.MonsterSlainNumber;
            ExperienceGainedTotal = newData.ExperienceGainedTotal;
            CharacterAtDeathList = newData.CharacterAtDeathList;
            MonstersKilledList = newData.MonstersKilledList;
            ItemsDroppedList = newData.ItemsDroppedList;

            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;

            return;
            
        }

        #region ScoreItems

        // Adding a character to the score output as a text string
        public bool AddCharacterToList( Character data)
        {
            CharacterAtDeathList += data.ToString();
            return true;
        }

        // All a monster to the list of monsters and their stats
        public bool AddMonsterToList( Monster data)
        {
            MonstersKilledList += data.ToString();
            return true;
           
        }

        // Add an item to the list of items for score and their stats
        public bool AddItemToList( Item data)
        {
            ItemsDroppedList += data.ToString();
            return false;

        }
        #endregion ScoreItems
    }
}