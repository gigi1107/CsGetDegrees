﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDown.Models;
using WDown.ViewModels;

namespace WDown.Services
{
    //makes and stores mock data not stored in a real database
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        private List<Monster> _monsterDataset = new List<Monster>();
        private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitilizeSeedData();
        }

        private void InitilizeSeedData()
        {


           
            // Load Items.
            _itemDataset.Add(new Item
            {
                Name = "Fresh Carrot",
                Location = ItemLocationEnum.Unknown,
                Description = "Regenerates health to full when consumed. ",
                Value = 200,
                Range = 0,
                Damage = 0,
                Wearable = false,
                Attribute = AttributeEnum.CurrentHealth,
                ImageURI = "https://i.imgur.com/DeFwZPA.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Wet Grass",
                Location = ItemLocationEnum.Unknown,
                Description = "Regenerates some health when consumed. ",
                Value = 10,
                Range = 0,
                Damage = 0,
                Wearable = false,
                Attribute = AttributeEnum.CurrentHealth,
                ImageURI = "https://i.imgur.com/6jxvNb7.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Magical Dew",
                Location = ItemLocationEnum.Unknown,
                Description = "Regenerates Wisdom points. ",
                Value = 1,
                Range = 0,
                Damage = 0,
                Wearable = false,
                Attribute = AttributeEnum.Unknown,
                ImageURI = "https://i.imgur.com/YTAxFyn.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Tree Bark of Agility",
                Location = ItemLocationEnum.PrimaryHand,
                Description = "Increases defense. ",
                Value = 1,
                Range = 0,
                Damage = 0,
                Wearable = true,
                Attribute = AttributeEnum.Defense,
                ImageURI = "https://i.imgur.com/YRhd2kP.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Rope of Vengeance",
                Location = ItemLocationEnum.Head,
                Description = "Increases Attack ",
                Value = 1,
                Range = 0,
                Damage = 0,
                Wearable = true,
                Attribute = AttributeEnum.Attack,
                ImageURI = "https://i.imgur.com/ZpEuNvv.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Collar of Transcendence",
                Location = ItemLocationEnum.Necklass,
                Description = "Increases speed. ",
                Value = 1,
                Range = 0,
                Damage = 0,
                Wearable = true,
                Attribute = AttributeEnum.Speed,
                ImageURI = "https://i.imgur.com/JPbyaEF.png"
            });

            _itemDataset.Add(new Item
            {
                Name = "Jewel of Gibberish",
                Location = ItemLocationEnum.LeftFinger,
                Description = "Decreases Attack. ",
                Value = -1,
                Range = 0,
                Damage = 0,
                Wearable = true,
                Attribute = AttributeEnum.Attack,
                ImageURI = "https://i.imgur.com/ikREFHq.png"
            });


            // Implement Characters

            _characterDataset.Add(new Character
            {
                Name = "Hazel",
                Description = "Hazel leads the group of rabbits" +
                "from Sandleford and becomes Chief Rabbit. Though not particularly large or" +
                "powerful, he is loyal, brave, and a quick thinker.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "https://i.imgur.com/pNTXqMH.png",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 2, 2, 7, 7),
                LoveInterest = "Clover"
            });

            _characterDataset.Add(new Character
            {
                Name = "Fiver",
                Description = "A small rabbit, the runt of the litter. He" +
                "has the ability to foresee the future in the form of vivid yet confusing visions." +
                "Fiver is extremely wise and intelligent, but quiet. ",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "https://i.imgur.com/lcTjFJ1.png",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 1, 2, 6, 6),
                LoveInterest = "Vilthuril"
            });

            _characterDataset.Add(new Character
            {
                Name = "Clover",
                Description = "Clover used to live on a farm in a hutch, until " +
                    "she was rescued by Hazel, BigWig, and Fiver. She is naive " +
                    "but well- intentioned.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "https://i.imgur.com/rB0yLge.png",
                Wisdom = 1,
                CharacterAttribute = new AttributeBase(2, 3, 2, 7, 7),
                LoveInterest = "Hazel"

            });

            _characterDataset.Add(new Character
            {
                Name = "Hyzenthlay",
                Description = "Hyzenthlay is one of the does from Efrafra, the evil rabbit" +
                    "warren. She is strong of spirit and will, and the other does in the warren " +
                    "look up to her. ",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "https://i.imgur.com/kIH4UtV.jpg",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 1, 2, 7, 7),
                LoveInterest = "None"
            });

            _characterDataset.Add(new Character
            {
                Name = "BigWig",
                Description = "Used to be an Owsla officer. BigWig is the largest and toughest rabbit of the group." +
                    "He has a large tuft of hair on top of his head.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "https://i.imgur.com/0uaVKml.jpg",
                Wisdom = 1,
                CharacterAttribute = new AttributeBase(3, 1, 4, 8, 8),
                LoveInterest = "Strawberry"


            });



            // Implement Monsters
            _monsterDataset.Add(new Monster
            {
                Name = "Cat",
                Description = "The house cat is one of the most ferocious hunters of rabbits. Its deadly" +
                "claws and extreme agility makes it second most dangerous enemy, after the house dog.",
                Level = 1,
                MonsterAttribute = new AttributeBase(3,1,2,5,5),
                ImageURI = "https://i.imgur.com/EamWXas.png",
                UniqueItem = "Tree Bark",
                UniqueDropRate = 0.5

            });

            _monsterDataset.Add(new Monster
            {
                Name = "Dog",
                Description = "Trained to hunt, the Rottweiler is an extremely hostile enemy of the rabbits.",
                Level = 2,
                MonsterAttribute = new AttributeBase(4, 2, 2, 6, 6),
                ImageURI = "https://i.imgur.com/OnGOYw9.png",
                UniqueItem = "Collar of Transcendence",
                UniqueDropRate = 0.3

            });

            _monsterDataset.Add(new Monster
            {
                Name = "Bird",
                Description = "Fast and unpredictable creatures with wings that pose great harm to the warren.",
                Level = 3,
                MonsterAttribute = new AttributeBase(3, 1, 2, 10, 10),
                ImageURI = "https://i.imgur.com/PirEPrm.png",
                UniqueItem = "Rope",
                UniqueDropRate = 0.4

            });

            _monsterDataset.Add(new Monster
            {
                Name = "Fox",
                Description = "Also known as Elil by the rabbits. A fox is notable for killing" +
                " Mallow after Bigwig leads the carnivore away from Hazel and his group," +
                " unintentionally leading it to an Efrafa Wide Patrol. ",
                Level = 2,
                MonsterAttribute = new AttributeBase(3,4,3,8,8),
                ImageURI = "https://i.imgur.com/K8M7L7E.png",
                UniqueItem = "Collar of Transcendence",
                UniqueDropRate = 0.3
                
            });

            _monsterDataset.Add(new Monster
            {
                Name = "General Woundwort",
                Description = "General Woundwort is depicted as an exceptionally large rabbit with ragged, " +
                "dark grey fur, torn ears and numerous scars, most notably one over his blind left eye. " +
                "He is an arrogant, cruel, bloodthirsty and viciously ruthless Chief rabbit, and will " + 
                "execute anyone who gets in his way, or disobey him at Efrafa.",
                Level = 20,
                MonsterAttribute = new AttributeBase(3, 4, 3, 8, 8),
                ImageURI = "https://i.imgur.com/QTJzhFI.png",
                UniqueItem = "Champion Cup",
                UniqueDropRate = 1.0

            });

            // Implement Scores
            _scoreDataset.Add(new Score
            {
                Name = "Score 1",
                BattleNumber = 1,
                ScoreTotal = 15,
                GameDate = DateTime.Now,
                AutoBattle = false,
                TurnCount = 20,
                RoundCount = 3,
                MonsterSlainNumber = 18,
                ExperienceGainedTotal = 10000,
                CharacterAtDeathList = "Hazel, BigWig",
                MonstersKilledList = "Dog, Cat, Fox, Bird, Cult Rabbit, Dog",
                ItemsDroppedList = "Tree Bark, Collar of Transcendence, Magical Dew",
                Description = "First score"

            });

            _scoreDataset.Add(new Score
            {
                Name = "Score 2",
                BattleNumber = 2,
                ScoreTotal = 200,
                GameDate = DateTime.Now,
                AutoBattle = false,
                TurnCount = 30,
                RoundCount = 4,
                MonsterSlainNumber = 24,
                ExperienceGainedTotal = 20000,
                CharacterAtDeathList = "Hyzenthlay, Clover",
                MonstersKilledList = "Cat, Cat, Dog, Bird, Cult Rabbit, Dog",
                ItemsDroppedList = "Grass, Collar of Transcendence, Magical Dew",
                Description = "Second Score"

            });

        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            _characterDataset.Clear();
            _itemDataset.Clear();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            // Implement Monsters

            // Implement Characters 
            CharactersViewModel.Instance.SetNeedsRefresh(true);

            // Implement Scores
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        public List<Item> GetItemList()
        {
            return _itemDataset;
        }

        #endregion Item


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }
        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }
        //#endregion Character


        //Monster
        #region Monster


        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Monster(data.Id);
            if (oldData == null)
            {
                _monsterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Monster(data);
            if (UpdateResult)
            {
                await AddAsync_Monster(data);
                return true;
            }

            return false;
        }
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(data);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(_monsterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterDataset);
        }

        #endregion Monster

        #region Score
        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));

        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }

        public async Task<bool> InsertUpdateAsync_Score(Score data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Score(data.Id);
            if (oldData == null)
            {
                _scoreDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Score(data);
            if (UpdateResult)
            {
                await AddAsync_Score(data);
                return true;
            }

            return false;
        }

        #endregion Score



    }
}


