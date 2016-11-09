using Battleship.Models.Interfaces;

namespace Battleship.Models
{
    public class Destroyer : Ship, IShip
    {
        private IConstants _constants;

        public Destroyer(ShipDirection direction, BoardCoordinates position, IConstants constants): base(direction, 1, position)
        {
            _constants = constants;
            this.Size = _constants.DestroyerLength;
        }
    }
}
