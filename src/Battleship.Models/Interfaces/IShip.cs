using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Interfaces
{
    public interface IShip
    {
        BoardCoordinates Position { get; set; }
        ShipDirection Direction { get; set; }
        int Size { get; set; }
        int HitCount { get; set; }
        bool Sunk { get; set; }
    }
}
