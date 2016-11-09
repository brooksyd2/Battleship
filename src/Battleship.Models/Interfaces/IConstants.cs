namespace Battleship.Models.Interfaces
{
    public interface IConstants
    {
        int BoardRows { get; }
        int BoardColumns { get; }
        int BattleshipLength { get; }
        int DestroyerLength { get; }
        int NumberOfDestroyers { get; }
        int NumberOfBattleships { get; }
        string SinkMessage { get; }
        string EndMessage { get;  }
        string HitMessage { get; }
        string MissMessage { get; }
    }
}
