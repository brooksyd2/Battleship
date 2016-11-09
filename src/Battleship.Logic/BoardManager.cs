using Battleship.Logic.Interfaces;
using Battleship.Models;
using Battleship.Models.Enums;
using Battleship.Models.Function;
using Battleship.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class BoardManager : IBoardManager
    {
        private Dictionary<ShipType, int> ShipTypes;
        private IBoardFactory _boardFactory;
        private static Board GameBoard;
        private IConstants _constants;

        public BoardManager(IBoardFactory boardFactory, IConstants constants)
        {
            _boardFactory = boardFactory;
            _constants = constants;
            ShipTypes = new Dictionary<ShipType, int>
            {
                { ShipType.Battleship, _constants.NumberOfBattleships },
                { ShipType.Destroyer, _constants.NumberOfDestroyers }
            };
            GameBoard = _boardFactory.InitialiseBoard(ShipTypes);            
        }

        public BoardWrapper GetBoard()
        {
            var board = new BoardWrapper
            {
                DisplayBoard = GameBoard.DisplayBoard
            };

            return board;
        }

        public ShotWrapper Shoot(Torpedo torpedo)
        {

            var torpedoCoordinates = ExtractPosition(torpedo);
            ShotStatus shotStatus;
            string message = string.Empty;

            if (GameBoard.GameBoard[torpedoCoordinates.Row, torpedoCoordinates.Column] == BoardSquareStatus.Empty) {
                shotStatus = ShotStatus.Miss;
                message = _constants.MissMessage;
                GameBoard.DisplayBoard[torpedoCoordinates.Row, torpedoCoordinates.Column] = ShotStatus.Miss.ToString();
            }
            else
            {
                shotStatus = TorpedoHit(torpedoCoordinates);
                if(shotStatus == ShotStatus.Hit)
                {
                    GameBoard.DisplayBoard[torpedoCoordinates.Row, torpedoCoordinates.Column] = ShotTypes.Hit.ToString();
                    message = _constants.HitMessage;
                }
                else if(shotStatus == ShotStatus.Sink)
                {
                    GameBoard.DisplayBoard[torpedoCoordinates.Row, torpedoCoordinates.Column] = ShotTypes.Sink.ToString();
                    message = _constants.SinkMessage;
                }

                if (AllShipsSunk())
                {
                    shotStatus = ShotStatus.End;
                    message = _constants.EndMessage;
                }
            }

            var boardWrapper = new ShotWrapper
            {
                DisplayBoard = GameBoard.DisplayBoard,
                Status = shotStatus.ToString(),
                Message = message
            };

            return boardWrapper;
        }

        private bool AllShipsSunk()
        {
            var sunkShipCount = GameBoard.Ships.Where(x => x.Sunk == true).ToList().Count;
            var shipsSunk = sunkShipCount == GameBoard.Ships.Count ? true : false;
            return shipsSunk;
        }

        private ShotStatus TorpedoHit(BoardCoordinates torpedo)
        {
            ShotStatus shotStatus = ShotStatus.Miss;            
            for (int i = 0; i < GameBoard.Ships.Count; i++)
            {
                var ship = GameBoard.Ships[i];               
                var boardCellStatus = (ShotTypes)Enum.Parse(typeof(ShotTypes), GameBoard.DisplayBoard[torpedo.Row, torpedo.Column]);
                if (CheckShipHit(ship, torpedo) && (boardCellStatus != ShotTypes.Hit && boardCellStatus != ShotTypes.Sink))
                {
                    if(ship.HitCount < ship.Size) ship.HitCount++;
                    if(ship.HitCount == ship.Size)
                    {
                        ship.Sunk = true;
                        shotStatus = ShotStatus.Sink;
                    } else
                    {
                        shotStatus = ShotStatus.Hit;
                    }
                }
            }

            return shotStatus;
        }

        private bool CheckShipHit(IShip ship, BoardCoordinates torpedo)
        {
            var row = ship.Position.Row;
            var col = ship.Position.Column;

            for (int j = 0; j < ship.Size; j++)
            {
                if (torpedo.Row == row && torpedo.Column == col)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Horizontal)
                {
                    col++;
                }
                else
                {
                    row++;
                }
            }

            return false;
        }

        private bool ShotInBounds(BoardCoordinates position)
        {            
            return (position.Column >= 0 && position.Column <= _constants.BoardColumns - 1) && (position.Row >= 0 && position.Row <= _constants.BoardRows - 1);
        }

        private BoardCoordinates ExtractPosition(Torpedo torpedo)
        {
            if(String.IsNullOrEmpty(torpedo.TorpedoPosition)) throw new ArgumentOutOfRangeException("Invalid coordinates");

            var column = torpedo.TorpedoPosition.Substring(0, 1);
            var rowString = torpedo.TorpedoPosition.Substring(1, torpedo.TorpedoPosition.Length - 1);
            int row;
            if (!int.TryParse(rowString, out row)) throw new ArgumentOutOfRangeException("Invalid coordinates");
            if (String.IsNullOrEmpty(column) || !Char.IsLetter(column.ToCharArray()[0])) throw new ArgumentOutOfRangeException("Invalid coordinates");
            int columnInt = ((int)char.ToUpper(column.ToCharArray()[0]) - 64);

            var boardCoordinates = new BoardCoordinates
            {
                Row = row - 1,
                Column = columnInt - 1
            };

            if (!ShotInBounds(boardCoordinates)) throw new ArgumentOutOfRangeException("Invalid coordinates"); ;

            return boardCoordinates;
        }

    }
}
