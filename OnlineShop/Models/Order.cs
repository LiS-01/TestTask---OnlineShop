using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public enum Status
    {
        Created, Paid, Delivered
    }

    public class Order
    {
        public int ID { get; set; }

        public int ClientID { get; set; }

        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Status Status { get; set; }

        [Column("Order Summa")]
        public decimal OrderSum { get; set; }

        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}

