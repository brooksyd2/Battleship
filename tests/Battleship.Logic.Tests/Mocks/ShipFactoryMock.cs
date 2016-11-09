using Battleship.Logic.Interfaces;
using System;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using Battleship.Models;

namespace Battleship.Logic.Tests.Mocks
{
    public class ShipFactoryMock : IShipFactory
    {
        public IShip CreateShip<T>(BoardSquareStatus[,] gameBoard) where T : IShip
        {
            ShipDirection direction = ShipDirection.Horizontal;
            BoardCoordinates initialPosition = new BoardCoordinates { Row = 1, Column = 1 };
            var ship = (T)Activator.CreateInstance(typeof(T), direction, initialPosition);

            return ship;
        }
    }
}
