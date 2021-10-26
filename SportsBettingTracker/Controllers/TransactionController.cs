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
        public IHttpActionResult GetAllTransactions()
        {
            TransactionService transactionService = CreateTransactionService();
            var transactions = transactionService.GetAllTransactionsByUserId();
            return Ok(transactions);
        }

        public IHttpActionResult PostNewTransaction(TransactionModelCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.CreateNewTransaction(transaction))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult GetTransactionByTransactionId(int id)
        {
            TransactionService transactionService = CreateTransactionService();
            var transaction = transactionService.GetOneTransactionByTransactionId(id);
            return Ok();
        }

        public IHttpActionResult UpdateTransaction(TransactionEdit transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.UpdateTransactionByTransactionId(transaction))
                return InternalServerError();

            return Ok();

        }

        public IHttpActionResult DeleteTransaction(int id)
        {
            var service = CreateTransactionService();

            if (!service.DeleteTransactionByTransactionId(id))
                return InternalServerError();

            return Ok();
        }

        private TransactionService CreateTransactionService()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var userService = new TransactionService(userId);
            return userService;
        }
    }
}
