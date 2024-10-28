using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Interface;
using PaymentSystem.Models;
using PaymentSystem.Models.Response;

namespace PaymentSystem.Controllers
{
    public class MerchantController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly IMerchant _merchant;

        public MerchantController(ITransaction transaction, IMerchant merchant)
        {
            _transaction = transaction;
            _merchant = merchant;
        }

        [HttpGet("approval")]
        public async Task<IActionResult> Index(int transactionId)
        {
            var transaction = await _transaction.GetTransactionById(transactionId);
            var amount = transaction.Amount;

            ViewBag.Amount = amount;
            ViewBag.TransactionId = transactionId;
            return View();
        }


        [HttpPost("processPayment")]
        public async Task<IActionResult> ProcessPayment(int transactionId, string action)
        {
            var transaction = await _transaction.GetTransactionById(transactionId);
            if (transaction == null)
                return NotFound("Transaction Not Found");

            if (action == "confirm")
            {
                await _merchant.UpdatePaymentStatus(transactionId, 1);
                return Ok(new MerchantNotification
                {
                    TransactionId = transactionId,
                    Status = "Confirmed"
                });
            }
            else
            {
                await _merchant.UpdatePaymentStatus(transactionId, 5);
                return Ok(new MerchantNotification
                {
                    TransactionId = transactionId,
                    Status = "Rejected"
                });
            }
        }
    }
}
