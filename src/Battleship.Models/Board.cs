using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using System.Collections.Generic;

namespace Battleship.Models
{
    public class Board
    {
        public BoardSquareStatus[,] GameBoard { get; set; }
        public string[,] DisplayBoard { get; set; }
        public IList<IShip> Ships { get; set; }
    }
}
