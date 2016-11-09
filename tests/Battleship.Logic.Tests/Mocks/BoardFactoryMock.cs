using Battleship.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Interfaces;

namespace Battleship.Logic.Tests.Mocks
{
    public class BoardFactoryMock : IBoardFactory
    {
        private IConstants _constants;

        public BoardFactoryMock(IConstants constants)
        {
            _constants = constants;
        }

        public Board InitialiseBoard(Dictionary<ShipType, int> shipTypes)
        {
            return new Board
            {
                GameBoard = new BoardSquareStatus[_constants.BoardRows, _constants.BoardColumns],
                DisplayBoard = new string[_constants.BoardRows, _constants.BoardColumns],
                Ships = new List<IShip>()
            };
        }
    }
}
