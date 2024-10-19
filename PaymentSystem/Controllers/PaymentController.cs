using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Interface;
using PaymentSystem.Models.Deposit;
using PaymentSystem.Models;
using PaymentSystem.Models.Withdraw;
using PaymentSystem.Models.Response;
using PaymentSystem.Models.Merchant;
using PaymentSystem.Models.Transaction;

namespace PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ITransaction _transaction;
        private readonly IMerchant _merchant;
        private readonly IConfiguration _configuration;
        public PaymentController(ITransaction transaction, IConfiguration configuration, IMerchant merchant)
        {
            _transaction = transaction;
            _configuration = configuration;
            _merchant = merchant;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> ProcessDeposit([FromBody] TransactionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registered = await _transaction.AddTransaction(request,"Deposit");
            if (!registered)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Register",
                    PaymentUrl = null!
                });
            }

            string indexRoute = Url.Action("Index", "Merchant", new { transactionId = request.TransactionId }, Request.Scheme)!;


            return Ok(new ApiResponse
            {
                Status = "SUCCESS",
                PaymentUrl = indexRoute
            });
        }


        [HttpPost("withdraw")]
        public async Task<ActionResult<ApiResponse>> ProcessWithdraw([FromBody] TransactionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registered = await _transaction.AddTransaction(request,"Withdraw");
            if (!registered)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Register",
                    PaymentUrl = null!
                });
            }

            string indexRoute = Url.Action("Index", "Merchant", new { transactionId = request.TransactionId }, Request.Scheme)!;


            return Ok(new ApiResponse
            {
                Status = "SUCCESS",
                PaymentUrl = indexRoute
            });
        }
    }
}
