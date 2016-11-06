using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Battleship.Models;
using System.Net;
using System.Net.Http;
using Battleship.Logic.Interfaces;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    public class FireController : Controller
    {
        private IBoardManager _boardManager;

        public FireController(IBoardManager boardManager)
        {
            _boardManager = boardManager;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Torpedo torpedoPosition)
        {
            try
            {
                var position = ExtractPosition(torpedoPosition);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid board coordinates" };
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { };
        }

        private BoardCoordinates ExtractPosition(Torpedo torpedo)
        {
            if (String.IsNullOrEmpty(torpedo.TorpedoPosition) || torpedo.TorpedoPosition.Length > 2) throw new Exception("Invalid coordinates");
            
            var column = torpedo.TorpedoPosition.Substring(1);
            var rowString = torpedo.TorpedoPosition.Substring(2, 1);
            int row;
            if (!int.TryParse(rowString, out row)) throw new Exception("Invalid coordinates");
            if (String.IsNullOrEmpty(column) || !Char.IsLetter(column.ToCharArray()[0])) throw new Exception("Invalid coordinates");

            var boardCoordinates = new BoardCoordinates {
                Row = row,
                Column = column
            };
            return boardCoordinates;
        }

    }
}
