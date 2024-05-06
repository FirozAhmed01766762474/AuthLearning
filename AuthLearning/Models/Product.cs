using System.ComponentModel.DataAnnotations;

namespace AuthLearning.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required, StringLength(15)]
        public string Unit { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
