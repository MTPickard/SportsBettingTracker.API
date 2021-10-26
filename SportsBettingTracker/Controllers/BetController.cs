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
        public IHttpActionResult GetAllBets()
        {
            BetService betService = CreateBetService();
            var bets = betService.GetBets();
            return Ok(bets);
        }

        [HttpGet]
        public IHttpActionResult GetBetById(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.GetBetById(id);
            return Ok(bet);
        }

        [HttpPut]
        public IHttpActionResult Put(BetEdit bet)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBetService();

            if(!service.UpdateBet(bet))
            {
                return InternalServerError();
            }
            return Ok("Bet updated.");
        }

        [HttpDelete]
        public IHttpActionResult DeleteBet(int id)
        {
            var service = CreateBetService();

            if(!service.DeleteBet(id))
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
