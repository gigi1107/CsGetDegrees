using WDown.Models;
using System.Diagnostics;

namespace WDown.GameEngine
{
    // Engine for the Auto Battle mode
    public class AutoBattleEngine
    {
        // Instantiate a new battle engine object
        public BattleEngine BattleEngine = new BattleEngine();

        // Run the battle
        // Returns false if there is an error 
        public bool RunAutoBattle()
        {
            // Auto Battle, does all the steps that a human would do.

            // Picks 6 Characters
            if (BattleEngine.AddCharactersToBattle() == false)
            {
                // Error, so exit...
                return false;
            }
           

            // Start
            BattleEngine.StartBattle(true);
            //initialize character list with random characters

           

            // Initialize the Rounds

            BattleEngine.SetPlayerCurrent();
            BattleEngine.StartRound();

            RoundEnum RoundResult;

            // Fight Loop. Continue until Game is Over...
            do
            {
                // Do the turn...
                RoundResult = BattleEngine.RoundNextTurn();

                // If the round is over start a new one...
                if (RoundResult == RoundEnum.NewRound)
                {
                    BattleEngine.NewRound();
                   
                }

            } while (RoundResult != RoundEnum.GameOver);

            BattleEngine.EndBattle();

            return true;
        }

        /// <summary>
        // Returns the Score from the current Battle Instance
        /// </summary>
        /// <returns>the score value</returns>
        public int GetScoreValue()
        {
            return BattleEngine.BattleScore.ScoreTotal;
        }

        /// <summary>
        /// Returns the current Score Object
        /// </summary>
        /// <returns>Current Score Object</returns>
        public Score GetScoreObject()
        {
            return BattleEngine.BattleScore;
        }

        /// <summary>
        /// Returns the number of Rounds in the battle
        /// </summary>
        /// <returns>the count of rounds</returns>
        public int GetRoundsValue()
        {
            return BattleEngine.BattleScore.RoundCount;
        }

        /// <summary>
        /// Retruns a formated String of the Results of the Battle
        /// </summary>
        /// <returns></returns>
        public string GetResultsOutput()
        {

            string myResult = BattleEngine.GetResultsOutput();
            Debug.WriteLine(myResult);

            return myResult;
        }
    }
}
