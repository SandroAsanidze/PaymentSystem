﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Domain.Interface;

namespace PaymentSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly IMerchant _merchant;
        private readonly INotificationService _notificationService;

        public AdminController(ITransaction transaction, IMerchant merchant, INotificationService notificationService)
        {
            _transaction = transaction;
            _merchant = merchant;
            _notificationService = notificationService;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Index()
        {
            var pendingPayments = await _transaction.GetPendingTransactions();

            return View(pendingPayments);
        }


        [HttpPost("adminResponse")]
        public async Task<IActionResult> AdminResponse(int transactionId, string action)
        {
            var transaction = await _transaction.GetTransactionById(transactionId);
            if (transaction == null)
                return NotFound("Transaction Not Found");

            if (action == "confirm")
            {
                await _merchant.UpdatePaymentStatus(transactionId, 2);

                var merchant = await _merchant.GetMerchant(transaction.MerchantId);
                var merchantUrl = merchant.URL;

                var notificationData = new
                {
                    TransactionID = transactionId,
                    Amount = transaction.Amount,
                    Status = "Confirmed"
                };

                var success = await _notificationService.SendNotificationAsync(merchantUrl, notificationData);

                if (success)
                {
                    return Ok(new { Message = "Notification sent successfully", Data = notificationData });
                }
                else
                {
                    return StatusCode(500, new { Message = "Failed to send notification", Data = notificationData });
                }

            }
            else
            {
                await _merchant.UpdatePaymentStatus(transactionId, 3);


                var merchant = await _merchant.GetMerchant(transaction.MerchantId);
                var merchantUrl = merchant.URL;

                var notificationData = new
                {
                    TransactionId = transactionId,
                    Amount = transaction.Amount,
                    Status = "Rejected"
                };

                var success = await _notificationService.SendNotificationAsync(merchantUrl, notificationData);

                if (success)
                {
                    return Ok(new { Message = "Notification sent successfully", Data = notificationData });
                }
                else
                {
                    return StatusCode(500, new { Message = "Failed to send notification", Data = notificationData });
                }
            }
        }
    }
}
