using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProductApp.Models;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        List<Product> _products;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
            _products = new List<Product>()
            {
                new Product { Id = 1,Name = "Computer"},
                new Product { Id = 2,Name = "Keyboard"},
                new Product { Id = 3,Name = "Mouse"},
            };
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            _logger.Log(LogLevel.Information,"GetProducts action has been called");

            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute]int id)
        {
            var foundProduct = _products.Find(x => x.Id == id);
            if (foundProduct is null) 
            {
                _logger.Log(LogLevel.Warning, "Product with {id} ID not found");
                return NotFound($"Product with {id} ID not found");
            }

            return Ok(foundProduct);
        }
    }
}
