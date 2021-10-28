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
    public class TransactionController : ApiController
    {
        // C POST PostNewTransaction
        public IHttpActionResult GetAllTransactionsByUserID()
        {
            TransactionService transactionService = CreateTransactionService();
            var transactions = transactionService.GetAllTransactionsByUserId();
            return Ok(transactions);
        }

        // R GET GetTransactionsByUserId
        public IHttpActionResult PostNewTransaction(TransactionCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.CreateNewTransaction(transaction))
                return InternalServerError();

            return Ok();
        }

        // R GET GetTransactionByTransactionId
        public IHttpActionResult GetTransactionByTransactionId(int id)
        {
            TransactionService transactionService = CreateTransactionService();
            var transaction = transactionService.GetOneTransactionByTransactionId(id);
            return Ok();
        }

        // U PUT PutTransactionByTransactionId
        public IHttpActionResult PutTransactionByTransactionID(TransactionEdit transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.UpdateTransactionByTransactionId(transaction))
                return InternalServerError();

            return Ok();

        }

        // D DELETE DeleteTransactionByTransactionId
        public IHttpActionResult DeleteTransactionByTransactionID(int id)
        {
            var service = CreateTransactionService();

            if (!service.RemoveTransactionByTransactionId(id))
                return InternalServerError();

            return Ok();
        }

        // Helper Method
        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var userService = new TransactionService(userId);
            return userService;
        }
    }
}
