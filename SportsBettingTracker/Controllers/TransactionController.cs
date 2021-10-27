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
        //Get Transactions by UserID
        public IHttpActionResult GetAllTransactionsByUserID()
        {
            TransactionService transactionService = CreateTransactionService();
            var transactions = transactionService.GetAllTransactionsByUserId();
            return Ok(transactions);
        }

        //Post Post New Transaction
        public IHttpActionResult PostNewTransaction(TransactionModelCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.CreateNewTransaction(transaction))
                return InternalServerError();

            return Ok();
        }

        //Get Transaction By TransactionID
        public IHttpActionResult GetTransactionByTransactionId(int id)
        {
            TransactionService transactionService = CreateTransactionService();
            var transaction = transactionService.GetOneTransactionByTransactionId(id);
            return Ok();
        }

        //Update Put Transaction By TransactionID
        public IHttpActionResult PutTransactionByTransactionID(TransactionEdit transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.UpdateTransactionByTransactionId(transaction))
                return InternalServerError();

            return Ok();

        }

        //Delete Transaction by TransactionID
        public IHttpActionResult DeleteTransactionByTransactionID(int id)
        {
            var service = CreateTransactionService();

            if (!service.RemoveTransactionByTransactionId(id))
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
