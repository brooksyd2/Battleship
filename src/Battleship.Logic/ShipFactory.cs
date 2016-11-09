using Battleship.Logic.Interfaces;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using System;

namespace Battleship.Logic
{
    public class ShipFactory : IShipFactory
    {
        private static Random random = new Random();
        private IConstants _constants;

        public ShipFactory(IConstants constants)
        {
            _constants = constants;
        }

        public IShip CreateShip<T>(BoardSquareStatus[,] gameBoard) where T:IShip
        {
            ShipDirection direction = GetShipDirection();
            BoardCoordinates initialPosition = new BoardCoordinates { Row = 1, Column = 1 };
            var ship = (T)Activator.CreateInstance(typeof(T), direction, initialPosition, _constants);
            ship.Position = this.GetShipPosition(ship.Size, direction);

            while (this.ShipsOverlap(ship, gameBoard))
            {
                ShipDirection newDirection = GetShipDirection();
                ship.Direction = newDirection;
                ship.Position = this.GetShipPosition(ship.Size, newDirection);
            }
            return ship;                    
        }

        private ShipDirection GetShipDirection()
        {
            return random.Next(0, 2) == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal;
        }

        private bool ShipsOverlap(IShip ship, BoardSquareStatus[,] gameBoard)
        {
            int shipRow = ship.Position.Row;
            int shipCol = ship.Position.Column;

            for (int i = 0; i < ship.Size; i++)
            {
                if (gameBoard[shipRow, shipCol] == BoardSquareStatus.Ship)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipRow++;
                }
                else
                {
                    shipCol++;
                }
            }

            return false;
        }


        private BoardCoordinates GetShipPosition(int shipSize, ShipDirection direction)
        {
            int row;
            int col;

            if (direction == ShipDirection.Horizontal)
            {
                row = random.Next(0, _constants.BoardColumns);
                col = random.Next(0, _constants.BoardRows - shipSize);
            }
            else
            {
                row = random.Next(0, _constants.BoardColumns - shipSize);
                col = random.Next(0, _constants.BoardRows);
            }

            return new BoardCoordinates {
                Row = row,
                Column = col
            };
        }

    }
}
