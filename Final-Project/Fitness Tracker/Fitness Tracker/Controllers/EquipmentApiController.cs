using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class EquipmentApiController : ControllerBase
    {
        private readonly IDatabaseAgent _agent;

        public EquipmentApiController(IDatabaseAgent agent)
        {
            _agent = agent;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _agent.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _agent.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // GET: api/products/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            var count = await _agent.GetProductCountAsync();
            return Ok(count);
        }
    }
}
