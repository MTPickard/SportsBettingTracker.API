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
    [Authorize]
    public class ResultController : ApiController
    {
        // C POST PostResult
        public IHttpActionResult PostResult(ResultCreate result)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateResultService();

            if (!service.CreateResult(result))
                return InternalServerError();

            return Ok();
        }

        // R GET GetAllResultsByUserId [!]
        public IHttpActionResult GetResultsByUserId()
        {
            ResultService resultService = CreateResultService();
            var results = resultService.ViewResultsByUserId();
            return Ok(results);
        }

        // R GET GetOneResultByResultId
        public IHttpActionResult GetResultByResultId(int resultId)
        {
            ResultService resultService = CreateResultService();
            var result = resultService.ViewResultByResultId(resultId);
            return Ok(result);
        }

        // U PUT PutResultByResultId
        public IHttpActionResult PutResultByResultId(ResultEdit result)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateResultService();

            if (!service.UpdateResultByResultId(result))
                return InternalServerError();

            return Ok();
        }

        // D DELETE DeleteResultByResultId
        public IHttpActionResult DeleteResultByResultId(int resultId)
        {
            var service = CreateResultService();

            if (!service.RemoveResultByResultId(resultId))
                return InternalServerError();

            return Ok();
        }

        // "Helper Method"
        private ResultService CreateResultService()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var userService = new ResultService(userId);
            return userService;
        }
    }
}
