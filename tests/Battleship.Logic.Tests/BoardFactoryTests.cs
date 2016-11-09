using Battleship.Logic.Interfaces;
using Battleship.Logic.Tests.Mocks;
using Battleship.Logic.Tests.TestObjects;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;

namespace Battleship.Logic.Tests
{
    public class BoardFactoryTests : IDisposable
    {
        private IShipFactory _shipFactory;
        private IBoardFactory _boardFactory;
        private ILogger<BoardFactory> _logger;
        private IConstants _constants = new TestConstants();

        public BoardFactoryTests() {
            _shipFactory = new ShipFactory(_constants);
            _boardFactory = new BoardFactory(_shipFactory, _logger, _constants);
        }

        public void Dispose()
        {
            _shipFactory = null;
            _boardFactory = null;
            _logger = null;
            _constants = null;
        }

        [Fact]
        public void Should_Create_Given_Number_Of_Ships()
        {
            var numberOfBattleShips = 2;
            var numberOfDestroyers = 2;
            var totalShips = numberOfBattleShips + numberOfDestroyers;

            var ships = new Dictionary<ShipType, int>
            {
                { ShipType.Battleship, numberOfBattleShips },
                { ShipType.Destroyer, numberOfDestroyers }
            };

            var gameBoard = _boardFactory.InitialiseBoard(ships);

            Assert.True(gameBoard.Ships.Count == totalShips);
        }

        [Fact]
        public void Should_Create_Valid_Gameboard()
        {
            var numberOfBattleShips = 2;
            var numberOfDestroyers = 2;
            var totalShips = numberOfBattleShips + numberOfDestroyers;

            var ships = new Dictionary<ShipType, int>
            {
                { ShipType.Battleship, numberOfBattleShips },
                { ShipType.Destroyer, numberOfDestroyers }
            };

            var gameBoard = _boardFactory.InitialiseBoard(ships);

            Assert.True(gameBoard.GameBoard.GetLength(0) == _constants.BoardRows);
            Assert.True(gameBoard.GameBoard.GetLength(1) == _constants.BoardColumns);
            Assert.True(gameBoard.DisplayBoard.GetLength(0) == _constants.BoardRows);
            Assert.True(gameBoard.DisplayBoard.GetLength(1) == _constants.BoardColumns);
        }

        [Fact]
        public void Should_Place_Correct_Number_Of_Ships_On_Gameboard()
        {
            var numberOfBattleShips = 2;
            var numberOfDestroyers = 2;
            var totalShips = numberOfBattleShips + numberOfDestroyers;

            var ships = new Dictionary<ShipType, int>
            {
                { ShipType.Battleship, numberOfBattleShips },
                { ShipType.Destroyer, numberOfDestroyers }
            };

            var gameBoard = _boardFactory.InitialiseBoard(ships);

            var totalExpectedSquares = (numberOfBattleShips * _constants.BattleshipLength) + (numberOfDestroyers * _constants.DestroyerLength);
            var totalShipSquaresOnBoard = 0;

            int rowBounds = gameBoard.GameBoard.GetUpperBound(0);
            int columnBounds = gameBoard.GameBoard.GetUpperBound(1);

            for (int i = 0; i <= rowBounds; i++)
            {
                for (int x = 0; x <= columnBounds; x++)
                {
                    if (gameBoard.GameBoard[i, x] == BoardSquareStatus.Ship) totalShipSquaresOnBoard++;
                }
            }

            Assert.True(totalExpectedSquares == totalShipSquaresOnBoard);
        }

        private IEnumerable<T> Flatten<T>(T[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    yield return map[row, col];
                }
            }
        }

    }
}
