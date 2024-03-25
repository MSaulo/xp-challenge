using Microsoft.AspNetCore.Mvc;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Repositories;

namespace XPChallenge.Controllers {
    [ApiController]
    [Route("api/customer")]
    public class CostumerController : ControllerBase {
        private readonly ILogger<CostumerController> _logger;
        private readonly IBaseRepository<Customer> _repository;

        public CostumerController(
            ILogger<CostumerController> logger,
            IBaseRepository<Customer> repository
        ) {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var customers = await _repository.GetAllAsync();
            return new JsonResult(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var customer = await _repository.GetByIdAsync(id);
            if (customer != null) {
                return new JsonResult(customer);
            } else {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { Status = 404, Message = "NotFound" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer body) {
            var customer = await _repository.CreateAsync(body);
            return new JsonResult(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Customer body) {
            var customer = await _repository.UpdateAsync(id, body);
            return new JsonResult(customer);
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
