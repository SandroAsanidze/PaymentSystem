using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Domain.Interface;
using PaymentSystem.Domain.Models.Response;
using PaymentSystem.Domain.Models.Transaction;

namespace PaymentSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ITransaction _transaction;
        private readonly IHashValidation _hash;
        private readonly IMerchant _merchant;
        public PaymentController(ITransaction transaction, IHashValidation hash, IMerchant merchant)
        {
            _transaction = transaction;
            _hash = hash;
            _merchant = merchant;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> ProcessDeposit([FromBody] TransactionDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var merchant = await _merchant.GetMerchant(request.MerchantId);
            var secretKey = merchant.SecretKey;

            var info = $"{request.Amount}+{request.MerchantId}+{request.TransactionId}+{secretKey}";
            var infoHash = _hash.Hash(info);

            var hashResult = _hash.Verify(infoHash, info);

            if (!hashResult)
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

            string indexRoute = Url.Action("Index", "Merchant", new { transactionId = transactionID }, Request.Scheme)!;

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

            var merchant = await _merchant.GetMerchant(request.MerchantId);
            var secretKey = merchant.SecretKey;

            var info = $"{request.Amount}+{request.MerchantId}+{request.TransactionId}+{secretKey}";
            var infoHash = _hash.Hash(info);

            var hashResult = _hash.Verify(infoHash, info);

            if (!hashResult)
            {
                return StatusCode(500, new ApiResponse
                {
                    Status = "ERROR Hash",
                    PaymentUrl = null!
                });
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
