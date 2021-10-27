using _02_SportsBetting.Models;
using _03_SportsBetting.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportsBettingTracker.Controllers
{
    public class BetController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostBet(BetCreate bet)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBetService();

            if(!service.CreateBet(bet))
            {
                return InternalServerError();
            }
            return Ok("Bet has been added.");
        }

        [HttpGet]
        public IHttpActionResult GetBetsByUserId()
        {
            BetService betService = CreateBetService();
            var bets = betService.ViewBetsByUserId();
            return Ok(bets);
        }

        [HttpGet]
        public IHttpActionResult GetBetByBetId(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.ViewBetByBetId(id);
            return Ok(bet);
        }

        [HttpPut]
        public IHttpActionResult PutBetByBetId(BetEdit bet)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBetService();

            if(!service.UpdateBetByBetId(bet))
            {
                return InternalServerError();
            }
            return Ok("Bet updated.");
        }

        [HttpDelete]
        public IHttpActionResult DeleteBetByBetId(int id)
        {
            var service = CreateBetService();

            if(!service.RemoveBetByBetId(id))
            {
                return InternalServerError();
            }
            return Ok("Bet deleted.");
        }

        //HelperMethod
        private BetService CreateBetService()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var betService = new BetService(userId);
            return betService;
        }
    }
}
