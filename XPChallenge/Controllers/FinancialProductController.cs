using Microsoft.AspNetCore.Mvc;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Repositories;

namespace XPChallenge.Controllers {
    [ApiController]
    [Route("api/financial-product")]
    public class FinancialProductController : ControllerBase {
        private readonly ILogger<FinancialProductController> _logger;
        private readonly IBaseRepository<FinancialProduct> _repository;

        public FinancialProductController(
            ILogger<FinancialProductController> logger,
            IBaseRepository<FinancialProduct> repository
        ) {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var financialProducts = await _repository.GetAllAsync();
            return new JsonResult(financialProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var financialProduct = await _repository.GetByIdAsync(id);
            if (financialProduct != null) {
                return new JsonResult(financialProduct);
            } else {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { Status = 404, Message = "NotFound" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FinancialProduct body) {
            var financialProduct = await _repository.CreateAsync(body);
            return new JsonResult(financialProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] FinancialProduct body) {
            var financialProduct = await _repository.UpdateAsync(id, body);
            return new JsonResult(financialProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            var deletedId = await _repository.DeleteAsync(id);
            if (deletedId != null) {
                return new JsonResult(new { Id = deletedId });
            } else {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { Status = 404, Message = "NotFound" });
            }
        }
    }
}
