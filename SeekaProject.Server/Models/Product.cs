using System.ComponentModel.DataAnnotations;

namespace SeekaProject.Server.Models
{
    public class Product
    {
        public Product()
        {

        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign key
        public int CategoryId { get; set; }
        // Naviagation property
        public Category Category { get; set; } = null!;
    }
}
