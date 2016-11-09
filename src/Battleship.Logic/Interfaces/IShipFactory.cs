using Battleship.Models.Enums;
using Battleship.Models.Interfaces;

namespace Battleship.Logic.Interfaces
{
    public interface IShipFactory
    {
        IShip CreateShip<T>(BoardSquareStatus[,] gameBoard) where T : IShip;
    }
}
