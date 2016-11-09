using Battleship.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Logic.Tests.TestObjects
{
    public class TestConstants : IConstants
    {
        private const int _boardRows = 5;
        private const int _boardColumns = 5;
        private const int _battleshipLength = 5;
        private const int _destroyerLength = 4;
        private const int _numberOfDestroyers = 2;
        private const int _numberOfBattleships = 1;
        private const string _sinkMessage = "You have sunk a ship";
        private const string _endMessage = "Congratulations you have sunk all the ships";
        private const string _hitMessage = "Congratulations, you hit a ship";
        private const string _missMessage = "You did not hit anything";

        public int BoardRows { get { return _boardRows; } }
        public int BoardColumns { get { return _boardColumns; } }
        public int BattleshipLength { get { return _battleshipLength; } }
        public int DestroyerLength { get { return _destroyerLength; } }
        public int NumberOfDestroyers { get { return _numberOfDestroyers; } }
        public int NumberOfBattleships { get { return _numberOfBattleships; } }
        public string SinkMessage { get { return _sinkMessage; } }
        public string EndMessage { get { return _endMessage; } }
        public string HitMessage { get { return _hitMessage; } }
        public string MissMessage { get { return _missMessage; } }
    }
}
