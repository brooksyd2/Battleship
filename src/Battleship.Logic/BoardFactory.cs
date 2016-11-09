using Battleship.Logic.Interfaces;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Battleship.Logic
{
    public class BoardFactory : IBoardFactory
    {
        private IShipFactory _shipFactory;
        private BoardSquareStatus[,] GameBoard;
        private IList<IShip> Ships;
        private readonly ILogger<BoardFactory> _logger;
        private IConstants _constants;

        public BoardFactory(IShipFactory shipFactory, ILogger<BoardFactory> logger, IConstants constants)
        {
            _shipFactory = shipFactory;
            _constants = constants;
        }

        public Board InitialiseBoard(Dictionary<ShipType, int> shipTypes)
        {

            GameBoard = new BoardSquareStatus[_constants.BoardRows, _constants.BoardColumns];
            var displayBoard = new string[_constants.BoardRows, _constants.BoardColumns];

            for (int row = 0; row < _constants.BoardRows; row++)
            {
                for (int col = 0; col < _constants.BoardColumns; col++)
                {
                    GameBoard[row, col] = BoardSquareStatus.Empty;
                    displayBoard[row, col] = ShotTypes.Empty.ToString();
                }
            }

            var ships = AddShips(shipTypes);

            var board = new Board
            {
                GameBoard = GameBoard,
                DisplayBoard = displayBoard,
                Ships = ships
            };
            return board;
        }

        private IList<IShip> AddShips(Dictionary<ShipType, int> shipTypes)
        {
            var ships = new List<IShip>();

            foreach (var shipType in shipTypes)
            {
                for (int i = 0; i < shipType.Value; i++)
                {
                    try
                    {
                        IShip ship = shipType.Key == ShipType.Battleship ?
                            ship = _shipFactory.CreateShip<Models.Battleship>(GameBoard) :
                            ship = _shipFactory.CreateShip<Models.Destroyer>(GameBoard);

                        ships.Add(ship);
                        AddShipToBoard(ship);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                }
            }

            return ships;
        }

        private void AddShipToBoard(IShip ship)
        {
            int shipRow = ship.Position.Row;
            int shipCol = ship.Position.Column;

            for (int i = 0; i < ship.Size; i++)
            {
                GameBoard[shipRow, shipCol] = BoardSquareStatus.Ship;

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipRow++;
                }
                else
                {
                    shipCol++;
                }
            }
        }
    }
}
