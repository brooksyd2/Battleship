
using Battleship.Models.Interfaces;

namespace Battleship.Models
{
    public class GameBoard
    {
        private readonly char[,] _gameBoard;
        private IConstants _constants;

        public GameBoard(IConstants constants)
        {
            _constants = constants;
            _gameBoard = new char[_constants.BoardRows, _constants.BoardColumns];
        }
    }
}
