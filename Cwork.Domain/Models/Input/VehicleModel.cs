using System.ComponentModel.DataAnnotations;

namespace Cwork.Domain.Models.Input
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]

        public string Year { get; set; }
        [Required]

        public decimal Weight { get; set; }
        [Required]

        public int ManufacturingId { get; set; }
        [Required]

        public int CategoryId { get; set; }

        public ManufacturerModel Manufacturer { get; set; }
        public CategoryModel Category { get; set; }
    }
}
