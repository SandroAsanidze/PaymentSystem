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
        private readonly IHashValidation _hash;
        public PaymentController(ITransaction transaction, IHashValidation hash)
        {
            _transaction = transaction;
            _hash = hash;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> ProcessDeposit([FromBody] TransactionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = $"{request.Amount}+{request.MerchantId}+{request.TransactionId}+SecretKey";
            var infoHash = _hash.Hash(info);

            var hashResult = _hash.Verify(infoHash, info);

            if(!hashResult)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Hash",
                    PaymentUrl = null!
                });
            }

            var transactionID = await _transaction.AddTransaction(request, "Deposit");

            if (transactionID == 0)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Register",
                    PaymentUrl = null!
                });
            }

            string indexRoute = Url.Action("Index", "Merchant",new { transactionId = transactionID },Request.Scheme)!;

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

            var transactionID = await _transaction.AddTransaction(request, "Withdraw");

            if (transactionID == 0)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Register",
                    PaymentUrl = null!
                });
            }

            string indexRoute = Url.Action("Index", "Merchant", new { transactionId = transactionID }, Request.Scheme)!;


            return Ok(new ApiResponse
            {
                Status = "SUCCESS",
                PaymentUrl = indexRoute
            });
        }
    }
}
