
namespace Battleship.Models
{
    public class GameBoard
    {
        private readonly char[,] _gameBoard = new char[Constants.BoardRows, Constants.BoardColumns];

        public GameBoard()
        {
        }
    }
}
