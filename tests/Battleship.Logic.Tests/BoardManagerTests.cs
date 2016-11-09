using Battleship.Logic.Interfaces;
using Battleship.Logic.Tests.Mocks;
using Battleship.Logic.Tests.TestObjects;
using Battleship.Models;
using Battleship.Models.Function;
using Battleship.Models.Interfaces;
using System;
using Xunit;

namespace Battleship.Logic.Tests
{
    public class BoardManagerTests : IDisposable
    {
        private IBoardManager _boardManager;
        private IBoardFactory _mockBoardFactory;
        private IConstants _constants = new TestConstants();

        public BoardManagerTests()
        {
            _mockBoardFactory = new BoardFactoryMock(_constants);
            _boardManager = new BoardManager(_mockBoardFactory, _constants);
            
        }

        public void Dispose()
        {
            _boardManager = null;
            _mockBoardFactory = null;
            _constants = null;
        }

        [Theory]
        [InlineData("A3")]
        [InlineData("B5")]
        [InlineData("E1")]
        public void Should_Return_Valid_Shot_Result_If_Shot_Is_In_Bounds(string value)
        {
            var torpedo = new Torpedo
            {
                TorpedoPosition = value
            };

            var results = _boardManager.Shoot(torpedo);

            Assert.IsType<ShotWrapper>(results);
        }

        [Theory]
        [InlineData("A13")]
        [InlineData("Z5")]
        [InlineData("C12")]
        public void Should_Return_Exception_If_Shot_Is_Out_Of_Bounds(string value)
        {
            var torpedo = new Torpedo
            {
                TorpedoPosition = value
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => _boardManager.Shoot(torpedo));
        }
    }
}
