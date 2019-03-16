using System;

namespace WDown.Models
{
    // Enum for the types of round state.
    public enum RoundEnum
    {
        Unknown = 0,
        NextTurn = 1,
        NewRound = 2,
        GameOver = 3,
    }

    // Types of players in a round
    public enum PlayerTypeEnum
    {
        Unknown = 0,
        Character = 1,
        Monster = 2,
    }

    //types of moves alllowed
    public enum MoveEnum
    {
        Attack = 0,
        Rest = 1,
        UseItem = 2,
    }

}
