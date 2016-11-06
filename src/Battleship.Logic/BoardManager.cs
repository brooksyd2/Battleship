using Battleship.Logic.Interfaces;
using Battleship.Models;

namespace Battleship.Logic
{
    public class BoardManager : IBoardManager
    {
        private readonly char[,] _gameBoard = new char[Constants.BoardRows, Constants.BoardColumns];

        public BoardManager()
        {

        }

        public bool Shoot(BoardCoordinates torpedo)
        {

            return false;
        }



    }
}
