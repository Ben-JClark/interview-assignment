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
        public IActionResult Get()
        {
            var query = from p in _context.Product
                        join c in _context.Category on p.CategoryId equals c.Id
                        select new {
                            p.Id,
                            p.Name,
                            p.Description,
                            p.Price,
                            p.CategoryId,
                            CategoryName = p.Category.Name
                        };

            return Ok(query);
        }

        // Get a single product by ID
        // GET api/products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Product Id. Id must be greater than 0");
            }

            var query = from p in _context.Product
                        join c in _context.Category on p.CategoryId equals c.Id
                        where p.Id == id
                        select new
                        {
                            p.Id,
                            p.Name,
                            p.Description,
                            p.Price,
                            p.CategoryId,
                            CategoryName = p.Category.Name
                        };

            if (!query.Any())
            {
                return NotFound("We couldn't find a product with that id");
            }

            return Ok(query);
        }

        // Add a new product
        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Post(string Name, string Description, decimal Price, int CategoryId)
        {
            if (Price < 0)
            {
                return BadRequest("Invalid Product Price. Price must be a positive number");
            } 
            if (CategoryId <= 0)
            {
                return BadRequest("Invalid Category Id. Id must be greater than 0");
            }
            if (!CategoryExists(CategoryId))
            {
                return BadRequest("We couldn't find a category with that id");
            }

            Product product = new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                CategoryId = CategoryId
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
            return Created();
        }

        // Update an existsing product
        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string Name, string Description, decimal Price, int CategoryId)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Product Id. Id must be greater than 0 number");
            }
            if (CategoryId <= 0)
            {
                return BadRequest("Invalid Category Id. Id must be greater than 0");
            }
            if (Price < 0)
            {
                return BadRequest("Invalid Product Price. Price must be a positive number");
            }
            if (!ProductExists(id))
            {
                return NotFound("We couldn't find a product with that id");
            }
            if (!CategoryExists(CategoryId))
            {
                return NotFound("We couldn't find a category with that id");
            }

            Product product = new Product
            {
                Id = id,
                Name = Name,
                Description = Description,
                Price = Price,
                CategoryId = CategoryId
            };
            _context.Update(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    

        // Delete a product
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Product Id. Id must be a positive number");
            }
            
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound("We couldn't find a product with that id");
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(Product => Product.Id == id);
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(Category => Category.Id == id);
        }
    }
}
