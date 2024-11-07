using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Domain.Interface;
using PaymentSystem.Domain.Models.Response;

namespace PaymentSystem.Web.Controllers
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

            var statusName = await _transaction.GetStatusName(transactionId);

            ViewBag.Amount = amount;
            ViewBag.TransactionId = transactionId;
            ViewBag.StatusName = statusName;
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

                return RedirectToAction("Index", new MerchantNotification
                {
                    TransactionId = transactionId,
                    Status = "Confirmed"
                });
            }
            else
            {
                await _merchant.UpdatePaymentStatus(transactionId, 5);
                return RedirectToAction("Index", new MerchantNotification
                {
                    TransactionId = transactionId,
                    Status = "Rejected"
                });
            }
        }
    }
}
