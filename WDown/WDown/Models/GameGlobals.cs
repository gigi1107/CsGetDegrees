using SQLite;
// Global switches for the overall game to use...

namespace WDown.Models
{
    public static class GameGlobals
    {
        // Max number of Players in a Party
        public static int MaxNumberPartyPlayers = 6;

        // Turn on to force Rolls to be non random
        public static bool ForceRollsToNotRandom = false;

        //var to hold slow is the new fast
        public static bool SlowIsTheNewFast = false;

        //var to hold Boss battles enables
        public static bool BossBattles = false;

        //var percent chance Boss battle occurs
        public static double BossBattleChance = 0.0;

        //corresponds to Slow is the new fast
        public static double PercentChanceValue = 0.0;

        // Holds the random value for the sytem
        private static int _ForcedRandomValue = 1;

        // What number should return for random numbers (1 is good choice...)
        public static int ForcedRandomValue
        {
            get => _ForcedRandomValue;
        }

        // What number to use for ToHit values (1,2, 19, 20)
        public static int ForceToHitValue = 20;

        // Forces Monsters to hit with a set value
        // Zero, because don't want to add it in unless it is used...
        public static int ForceMonsterDamangeBonusValue = 0;

        // Forces Characters to hit with a set value
        // Zero, because don't want to add it in unless it is used...
        public static int ForceCharacterDamangeBonusValue = 0;

        // Allow Random Items when monsters die...
        public static bool AllowMonsterDropItems = true;

        // Turn Off Random Number Generation, and use the passed in values.
        public static void SetForcedRandomNumbersValueAndToHit(int value, int hit)
        {
            SetForcedRandomNumbersValue(value);
            ForceToHitValue = hit;
        }

        // Turn Off Random Number Generation, and use the passed in values.
        public static void SetForcedRandomNumbersValue(int value)
        {
            EnableRandomValues();
            _ForcedRandomValue = value;
        }

        // Flip the Random State (false to true etc...)
        // Call this after setting, to restore...
        public static void ToggleRandomState()
        {
            ForceRollsToNotRandom = !ForceRollsToNotRandom;
        }

        // Turn Random State Off
        public static void DisableRandomValues()
        {
            ForceRollsToNotRandom = false;
        }

        // Turn Random State On
        public static void EnableRandomValues()
        {
            ForceRollsToNotRandom = true;
        }


       

        //sets values for slow is the new fast chance
        public static void SetRandomValueForSlowIsTheNewFast(double value)
        {
            PercentChanceValue = value;
        }

       //sets values for BossBattle 
       public static void SetPercentChanceOccurringBossBattle(double value)
        {
            BossBattleChance = value;
        }


        // Debug Settings
        public static bool EnableCriticalMissProblems = true;
        public static bool EnableCriticalHitDamage = true;


    }
}
