using NUnit.Framework;

using WDown.GameEngine;
using WDown.Models;
using WDown.ViewModels;
using WDown.Services;

using Xamarin.Forms.Mocks;

using UnitTests.Models.Default;

namespace UnitTests.GameEngine
{
    [TestFixture]
    public class BattleEngineTests
    {
        #region BattleEngineTDD

        [Test]
        public void BattleEngine_Instantiate_Should_Pass()
        {
            // Can create a new battle engine...

            // Arrange

            // Act
            var Actual = new BattleEngine();

            // Assert
            Assert.AreNotEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_AddCharacter_Should_Create_6_Characters()
        {
            // Set a new Battle Engine
            // Create new Characters, 6 is the default

            // Arrange
            var myEngine = new BattleEngine();
            var Expect = GameGlobals.MaxNumberPartyPlayers;

            // Act
            myEngine.AddCharactersToBattle();
            var Actual = myEngine.CharacterList.Count;

            // Assert
            Assert.AreEqual(Expect, Actual, TestContext.CurrentContext.Test.Name);
        }



        [Test]
        public void BattleEngine_AddCharacter_No_Characters_Should_Fail()
        {
            // Set a new Battle Engine
            // Create new Characters, 6 is the default

            // Arrange
            var myEngine = new BattleEngine();
            for (var i = 0; i < GameGlobals.MaxNumberPartyPlayers; i++)
            {
                myEngine.CharacterList.Add(new Character());
            }

            bool Expect = true; // Engine is ready to go...

            // Act
            var Actual = myEngine.AddCharactersToBattle();

            // Assert
            Assert.AreEqual(Expect, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion BattleEngineTDD

        #region BattleBasics
        [Test]
        public void BattleEngine_StartBattle_Flag_Should_Pass()
        {
            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();
            myBattleEngine.StartBattle(true);

            var Actual = myBattleEngine.GetAutoBattleState();
            var Expected = true;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_With_One_Character_Should_Pass()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            var myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myBattleEngine.StartBattle(true);

            var Actual = myBattleEngine.GetAutoBattleState();
            var Expected = true;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_With_No_Characters_Should_Skip()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            var Actual = myBattleEngine.StartBattle(false);
            var Expected = false;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_With_Six_Characters_Should_Pass()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            var myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myCharacter = new Character(DefaultModels.CharacterDefault());
            myBattleEngine.CharacterList.Add(myCharacter);

            myBattleEngine.StartBattle(true);

            var Actual = myBattleEngine.GetAutoBattleState();
            var Expected = true;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_EndBattle_Should_Pass()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();
            myBattleEngine.StartBattle(true);
            myBattleEngine.EndBattle();

            var Actual = myBattleEngine.BattleRunningState();
            var Expected = false;

            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion BattleBasics

      
        #region AddCharactersToBattle
        [Test]
        public void BattleEngine_AddCharactersToBattle_With_6_Characters_Should_Skip()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());
            myBattleEngine.CharacterList.Add(new Character());

            var Return = myBattleEngine.AddCharactersToBattle();

            var Actual = myBattleEngine.CharacterList.Count;
            var Expected = GameGlobals.MaxNumberPartyPlayers;

            Assert.AreEqual(true, Return, " Pass Fail " + TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_AddCharactersToBattle_With_Empty_CharacterListView_Should_Fail()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            // Clear the dataset...
            CharactersViewModel.Instance.Dataset.Clear();

            var Return = myBattleEngine.AddCharactersToBattle();

            // Reset
            MasterDataStore.ForceDataRestoreAll();

            Assert.AreEqual(false, Return, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_AddCharactersToBattle_With_Empty_CharacterList_Should_Create_Characters()
        {
            MockForms.Init();

            // Can create a new battle engine...
            var myBattleEngine = new BattleEngine();

            // Force data for Characters and Items to be loaded and ready...
            MasterDataStore.ForceDataRestoreAll();

            var myItems = ItemsViewModel.Instance;

            var Return = myBattleEngine.AddCharactersToBattle();

            var Actual = myBattleEngine.CharacterList.Count;
            var Expected = GameGlobals.MaxNumberPartyPlayers;

            Assert.AreEqual(true, Return, " Pass Fail " + TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        #endregion AddCharactersToBattle
    }
};
