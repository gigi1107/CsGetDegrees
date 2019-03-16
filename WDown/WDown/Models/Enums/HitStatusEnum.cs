namespace WDown.Models
{
    // Types of Hits during a Turn.
    public enum HitStatusEnum
    {
        Unknown = 0,
        Miss = 1,
        CriticalMiss = 10,
        Hit = 5,
        CriticalHit = 15
    }
}