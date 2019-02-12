﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDown.Models;
using WDown.ViewModels;

namespace WDown.Services
{
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
        //private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitilizeSeedData();
        }

        private void InitilizeSeedData()
        {


            // Implement

            // Load Items.
            _itemDataset.Add(new Item("Fresh Carrot", "+health"));

            _itemDataset.Add(new Item("Wet Grass", "+ 20% health"));

            _itemDataset.Add(new Item("Magical Dew", "+2 pts wisdom"));

            // Implement Characters

            _characterDataset.Add(new Character
            {
                Name = "Hazel",
                Age = 3,
                Description = "Hazel leads the group of rabbits" +
                "from Sandleford and becomes Chief Rabbit. Though not particularly large or" +
                "powerful, he is loyal, brave, and a quick thinker.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "Hazel.jpg",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 2, 2, 7, 7),
                LoveInterest = "Clover"
            });

            _characterDataset.Add(new Character
            {
                Name = "Fiver",
                Age = 1,
                Description = "A small rabbit, the runt of the litter. He" +
                "has the ability to foresee the future in the form of vivid yet confusing visions." +
                "Fiver is extremely wise and intelligent, but quiet. ",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "fiver_wire.png",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 1, 2, 6, 6),
                LoveInterest = "Vilthuril"
            });

            _characterDataset.Add(new Character
            {
                Name = "Clover",
                Age = 2,
                Description = "Clover used to live on a farm in a hutch, until " +
                    "she was rescued by Hazel, BigWig, and Fiver. She is naive " +
                    "but well- intentioned.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "clover.jpg",
                Wisdom = 1,
                CharacterAttribute = new AttributeBase(2, 3, 2, 7, 7),
                LoveInterest = "Hazel"

            });

            _characterDataset.Add(new Character
            {
                Name = "Hyzenthlay",
                Age = 3,
                Description = "Hyzenthlay is one of the does from Efrafra, the evil rabbit" +
                    "warren. She is strong of spirit and will, and the other does in the warren " +
                    "look up to her. ",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "Hyzenthlay.gif",
                Wisdom = 2,
                CharacterAttribute = new AttributeBase(1, 1, 2, 7, 7),
                LoveInterest = "Holly"
            });

            _characterDataset.Add(new Character
            {
                Name = "BigWig",
                Age = 2,
                Description = "Used to be an Owsla officer. BigWig is the largest and toughest rabbit of the group." +
                    "He has a large tuft of hair on top of his head.",
                Level = 1,
                ExperienceTotal = 0,
                ImageURI = "bigwig.jpg",
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
                ImageURI = "https://i.imgur.com/EamWXas.png"

            });

            // Implement Scores
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



    }
}


