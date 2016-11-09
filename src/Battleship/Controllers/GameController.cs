using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Battleship.Models;
using System.Net;
using System.Net.Http;
using Battleship.Logic.Interfaces;
using System.Web.Http;
using Battleship.Models.Function;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    public class GameController : ApiController
    {
        private IBoardManager _boardManager;

        public GameController(IBoardManager boardManager)
        {
            _boardManager = boardManager;
        }


        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            var board = _boardManager.GetBoard();        

            return Request.CreateResponse(HttpStatusCode.OK, board);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Torpedo torpedoPosition)
        {
            ShotWrapper shotResult;
            try
            {
                shotResult = _boardManager.Shoot(torpedoPosition);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid board coordinates" };
            }

            return Request.CreateResponse(HttpStatusCode.OK, shotResult);
        }



    }
}
