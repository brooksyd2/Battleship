using Battleship.Models.Enums;
using Battleship.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public abstract class Ship : IShip
    {
        public Ship() { }

        public Ship(ShipDirection direction, int size, BoardCoordinates position)
        {
            this.Direction = direction;
            this.Size = size;
            this.Position = position;
        }

        public BoardCoordinates Position { get; set; }
        public ShipDirection Direction { get; set; }
        public int Size { get; set; }
        public int HitCount { get; set; }
        public bool Sunk { get; set; }
    }
}
