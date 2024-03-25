using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Repositories;
using XPChallenge.Services;

namespace XPChallenge.Controllers {
    [ApiController]
    [Route("api/trade")]
    public class TradeController : ControllerBase {
        private readonly ILogger<FinancialProductController> _logger;
        private readonly IBaseRepository<Trade> _repository;
        private readonly TradeService _service;

        public TradeController(
            ILogger<FinancialProductController> logger,
            IBaseRepository<Trade> repository,
            TradeService service
        ) {
            _logger = logger;
            _repository = repository;
            _service = service;
        }

        [HttpGet("{customerId}/all-transactions")]
        public async Task<IActionResult> AllTransactions(string customerId) {
            var trades = await _service.GetAllTransactions(customerId);
            if (trades != null) {
                return new JsonResult(trades);
            } else {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { Status = 404, Message = "NotFound" });
            }
        }

        [HttpGet("{customerId}/{productId}/history")]
        public async Task<IActionResult> ProductHistory(string customerId, string productId) {
            var trade = await _service.GetProductHistory(productId, customerId);
            if (trade != null) {
                return new JsonResult(new { trade, trade.History });
            } else {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { Status = 404, Message = "NotFound" });
            }
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] Trade body) {
            var trade = await _service.Buy(body.ProductId, body.CustomerId, body.Quantity);
            if (trade != null) {
                Response.StatusCode = StatusCodes.Status201Created;
                return new JsonResult(new { Status = 201, Message = "Created" });
            } else {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(new { Status = 201, Message = "BadRequest", Description = "Invalid quantity" });
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] Trade body) {
            var trade = await _service.Sell(body.ProductId, body.CustomerId, body.Quantity);
            if (trade != null) {
                Response.StatusCode = StatusCodes.Status201Created;
                return new JsonResult(new { Status = 201, Message = "Created" });
            } else {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(new { Status = 400, Message = "BadRequest", Description = "Not found trade/Insufficient funds" });
            }
        }
    }
}
