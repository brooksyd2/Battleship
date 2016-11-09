using Battleship.Models;
using Battleship.Models.Function;

namespace Battleship.Logic.Interfaces
{
    public interface IBoardManager
    {
        BoardWrapper GetBoard();
        ShotWrapper Shoot(Torpedo torpedo);
    }
}