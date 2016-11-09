using Battleship.Models;
using Battleship.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Logic.Interfaces
{
    public interface IBoardFactory
    {
        Board InitialiseBoard(Dictionary<ShipType, int> shipTypes);
    }
}
