using System;
using WDown.Models;
using System.Collections.Generic;

namespace WDown.GameEngine
{
    public static class HelperEngine
    {
        /// <summary>
        /// Random should only be instantiated once
        /// Because each call to new Random will reset the seed value, and thus the numbers generated
        /// You can control the seed value for Random by passing a value to the constructor
        /// Do that if you want to be able able get the same sequence of Random over and over
        /// </summary>
        private static Random rnd = new Random();

        //keeps a table of levels to attributes
        //key: level
        //value: (speed, defense, attack, wisdom-opt, experienceTotl)
        public static Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>()
        {
            { 1, new int[]{1, 1, 1, 3, 0 } },
            { 2, new int[] {1, 2, 1, 4, 300 } },
            { 3, new int[] {1, 3, 2, 5, 900 } },
            { 4, new int[] {1, 3, 2, 5, 2700 } },
            { 5, new int[] {2, 4, 2, 5, 6500} },
            { 6, new int[] {2, 4, 3, 6, 14000 } },
            { 7, new int[] {2, 5, 3, 6, 23000} },
            { 8, new int[] {2, 5, 3, 7, 34000} },
            { 9, new int[] {2, 5, 3, 7, 48000} },
            { 10, new int[] {3, 6, 4, 8, 64000} },
            { 11, new int[] {3, 6, 4, 8, 85000 } },
            { 12, new int[] {3, 6, 4, 9, 100000} },
            { 13, new int[] {3, 7, 4, 9, 120000} },
            { 14, new int[] {3, 7, 5, 10, 140000 } },
            { 15, new int[] {4, 7, 5, 10, 165000 } },
            { 16, new int[] {4, 8, 5, 11, 195000 } },
            { 17, new int[] {4, 8, 5, 11, 225000 } },
            { 18, new int[] {4, 8, 6, 12, 265000} },
            { 19, new int[] {4, 9, 7, 14, 305000 } },
            { 20, new int[] {5, 10, 8, 15, 355000 } },

        };





        //level: (speed, defense, attack)

        /// <summary>
        /// Method to Roll A Random Dice, a Set number of times
        /// </summary>
        /// <param name="rolls">The number of Rolls to Make</param>
        /// <param name="dice">The Dice to Roll</param>
        /// <returns></returns>
        public static int RollDice (int rolls, int dice)
        {

            if (rolls < 1)
            {
                return 0;
            }

            if (dice < 1)
            {
                return 0;
            }

            if (Models.GameGlobals.ForceRollsToNotRandom)
            { 
                return rolls * Models.GameGlobals.ForcedRandomValue;
            }

            var myReturn = 0;

            for (var i = 0; i < rolls; i++)
            {
                // Add one to the dice, because random is between.  So 1-10 is rnd.Next(1,11)
                myReturn += rnd.Next(1, dice + 1);
            }

            return myReturn;
        }
    }
}
