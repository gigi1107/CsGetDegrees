using NUnit.Framework;
using WDown.GameEngine;
using WDown.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;
using WDown;

namespace UnitTests.GameEngine
{
    [TestFixture]
    public class AutoBattleEngineUnitTests
    {
        [Test]
        // This unit test ensures GetResultOutput returns a string and passes
        public void AutoBattleEngine_GetResultsOutput_Should_Pass()
        {
            MockForms.Init();
            // New AutoBattleEngine object
            AutoBattleEngine test1 = new AutoBattleEngine();

            // Call GetResultsOutput
            var Actual = test1.GetResultsOutput();

            // Assert
            Assert.IsNotNull(Actual, TestContext.CurrentContext.Test.Name);
        }
        [Test]
        // This unit test ensures AutoBattleEngine is instantiated
        public void AutoBattleEngine_Instantiate_Should_Pass()
        {

            // Arrange

            var Actual = new AutoBattleEngine();


            // Assert
            Assert.IsNotNull(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        // This ensures the GetScoreObject doesn't return a null object
        public void AutoBattleEngine_GetScoreObject_Should_Pass()
        {
            MockForms.Init();
            // New AutoBattleEngine object
            AutoBattleEngine test1 = new AutoBattleEngine();

            // Call GetResultsOutput
            var Actual = test1.GetScoreObject();

            // Assert
            Assert.IsNotNull(Actual, TestContext.CurrentContext.Test.Name);

        }
        [Test]
        // This ensures the GetScoreValue returns a valid int
        // Any value equal or larger than 0 is acceptable
        public void AutoBattleEngine_GetScoreValue_100_Should_Pass()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.ScoreTotal = 100;

            var Expected = 100;

            var Actual = test.GetScoreValue();

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        [Test]
        // This ensures the GetScoreValue returns a valid int
        // Any value equal or larger than 0 is acceptable
        public void AutoBattleEngine_GetScoreValue_0_Should_Pass()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.ScoreTotal = 0;

            var Expected = 0;

            var Actual = test.GetScoreValue();

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        // This unit test ensures round value of 1 is valid
        public void AutoBattleEngine_GetRoundsValue_1_Should_Pass()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.RoundCount = 1;

            var Expected = 1;

            var Actual = test.GetRoundsValue();

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        [Test]
        // This unit test ensures round value of 1000 is valid
        public void AutoBattleEngine_GetRoundsValue_1000_Should_Pass()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.RoundCount = 1000;

            var Expected = 1000;

            var Actual = test.GetRoundsValue(); ;

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        // This unit test ensures round value of -1 should return default value
        // Which is 1
        public void AutoBattleEngine_GetRoundsValue_Neg_1_Should_Default()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.RoundCount = -1;
            var Expected = 1; //default value

            var Actual = test.GetRoundsValue();

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        // This unit test ensures round value of -1 should return default value
        // Which is 1
        // All battle has at least 1 round
        public void AutoBattleEngine_GetRoundsValue_0_Should_Default()
        {
            MockForms.Init();
            //arrange
            AutoBattleEngine test = new AutoBattleEngine();
            test.BattleEngine.BattleScore.RoundCount = 0;
            var Expected = 1; //default value

            var Actual = test.GetRoundsValue();

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        
    }
}
