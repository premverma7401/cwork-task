using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cwork.Domain.Models.Input
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        //public string Icon { get; set; }
    }
}
