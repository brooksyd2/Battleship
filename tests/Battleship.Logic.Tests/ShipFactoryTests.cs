using Battleship.Logic.Interfaces;
using Battleship.Logic.Tests.TestObjects;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using System;
using Xunit;

namespace Battleship.Logic.Tests
{
    public class ShipFactoryTests : IDisposable
    {
        private IShipFactory _shipFactory;
        private IConstants _constants = new TestConstants();

        public ShipFactoryTests()
        {
            _shipFactory = new ShipFactory(_constants);
            _constants = new Constants();
        }

        public void Dispose()
        {
            _shipFactory = null;
            _constants = null;
        }

        [Fact]
        public void Should_Return_Valid_BattleShip_Dimensions()
        {
            var gameBoard = new BoardSquareStatus[_constants.BoardColumns, _constants.BoardRows];
            
            var ship = _shipFactory.CreateShip<Models.Battleship>(gameBoard);

            Assert.True(ship.Size == _constants.BattleshipLength);
        }

        [Fact]
        public void Should_Return_Valid_Destroyer_Dimensions()
        {
            var gameBoard = new BoardSquareStatus[_constants.BoardColumns, _constants.BoardRows];

            var ship = _shipFactory.CreateShip<Destroyer>(gameBoard);

            Assert.True(ship.Size == _constants.DestroyerLength);
        }
    }
}
