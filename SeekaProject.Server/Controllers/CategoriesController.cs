using Microsoft.AspNetCore.Mvc;
using SeekaProject.Server.Data;
using SeekaProject.Server.Models;


namespace SeekaProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all the categories
        // GET: api/categories
        [HttpGet]
        public IActionResult Get()
        {
            var query = from c in _context.Category select new Category() { Id = c.Id, Name = c.Name};

            if (!query.Any())
            {
                return NoContent();
            }

            return Ok(query);
        }
    }
}
