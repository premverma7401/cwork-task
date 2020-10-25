using System.ComponentModel.DataAnnotations;

namespace Cwork.Domain.Models.Input
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public decimal MinWeight { get; set; }
        [Required]
        public decimal MaxWeight { get; set; }
        public string Icon { get; set; }
    }
}
