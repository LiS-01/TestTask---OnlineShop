using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public int Code { get; set; }

        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}