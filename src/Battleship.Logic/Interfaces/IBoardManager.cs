using Battleship.Models;

namespace Battleship.Logic.Interfaces
{
    public interface IBoardManager
    {
        bool Shoot(BoardCoordinates torpedo);
    }
}