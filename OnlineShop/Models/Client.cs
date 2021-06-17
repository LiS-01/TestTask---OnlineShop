using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public enum Gender
    {
        Male, Female
    }

    public class Client
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayFormat(DataFormatString = "{0:name@gmail.com}", ApplyFormatInEditMode = true)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Column("Order Count")]
        public int OrderCount { get;set; }

        [Column("Average Order Sum")]
        public int AverageOrderSum {
            get;set;
        }

        public ICollection<Order> Orders { get; set; }
    }
}