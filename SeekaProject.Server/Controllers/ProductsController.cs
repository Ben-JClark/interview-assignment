using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using SeekaProject.Server.Data;
using SeekaProject.Server.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeekaProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all the products
        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _context.Product.ToListAsync();
            if (products.Count > 0)
            {
                return Ok(products);
            }
            return NoContent();
        }

        // Get a single product by ID
        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id > 0)
            {
                var product = await _context.Product.FindAsync(id);
                if (product != null)
                {
                    return Ok(product);
                }
                return NotFound("We couldn't find a product with that id");
            }
            return BadRequest("Invalid Product Id. Id must be greater than 0");
        }

        // Add a new product
        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Post(string Name, string Description, decimal Price)
        {
            if (Price >= 0)
            {
                if (!string.IsNullOrEmpty(Name)) // Validate the name
                {
                    if (!string.IsNullOrEmpty(Description)) // Validate the description
                    {
                        Product product = new Product { Name = Name, Description = Description, Price = Price };
                        _context.Add(product);
                        await _context.SaveChangesAsync();
                        return NoContent();
                    }
                    return BadRequest("Invalid Product Description. Description must be provided");
                }
                return BadRequest("Invalid Product Name. Name must be provided");
            }
            return BadRequest("Invalid Product Price. Price must be a positive number");
        }


        // Update an existsing product
        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string Name, string Description, decimal Price)
        {
            if(id >= 0)
            {
                if (ProductExists(id))
                {
                    if (Price >= 0)
                    {
                        Product product = new Product { Id = id, Name = Name, Description = Description, Price = Price };
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                        return NoContent();
                    }
                    return BadRequest("Invalid Product Price. Price must be a positive number");
                }
                return NotFound("We couldn't find a product with that id");
            }
            return BadRequest("Invalid Product Id. Id must be a positive number");
        }

        // Delete a product
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id >= 0)
            {
                var product = await _context.Product.FindAsync(id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                return NotFound("We couldn't find a product with that id");
            }
            return BadRequest("Invalid Product Id. Id must be a positive number");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(Product => Product.Id == id);
        }
    }
}
