using Battleship.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Battleship : Ship, IShip
    {
        private IConstants _constants;

        public Battleship(ShipDirection direction, BoardCoordinates position, IConstants constants) : base(direction, 1, position)
        {
            _constants = constants;
            this.Size = _constants.BattleshipLength;
        }
    }
}
