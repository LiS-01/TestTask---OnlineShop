using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clients.Any())
            {
                return;
            }

            var clients = new Client[]
            {
                new Client{Name="Alex", Email="alex@gmail.com", Birthdate=DateTime.Parse("2000-10-01"), Gender=Gender.Male},
                new Client{Name="Boris", Email="boris@gmail.com", Birthdate=DateTime.Parse("2000-10-01"), Gender=Gender.Male},
                new Client{Name="Carl", Email="carl@gmail.com", Birthdate=DateTime.Parse("2000-10-01"), Gender=Gender.Male},
                new Client{Name="David", Email="david@gmail.com", Birthdate=DateTime.Parse("2000-10-01"), Gender=Gender.Male},
                new Client{Name="Eric", Email="eric@gmail.com", Birthdate=DateTime.Parse("2000-10-01"), Gender=Gender.Male},
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product{Code=10, Price=12.34M, Title="Product1"},
                new Product{Code=12, Price=10.30M, Title="Product2"},
                new Product{Code=14, Price=2.40M, Title="Product3"},
                new Product{Code=16, Price=3.33M, Title="Product4"},
                new Product{Code=18, Price=1.67M, Title="Product5"},
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{ClientID=1, ProductID=1, Quantity=3, Status=Status.Paid},
                new Order{ClientID=2, ProductID=2, Quantity=3, Status=Status.Paid},
                new Order{ClientID=3, ProductID=3, Quantity=3, Status=Status.Paid},
                new Order{ClientID=4, ProductID=4, Quantity=3, Status=Status.Paid},
                new Order{ClientID=5, ProductID=5, Quantity=3, Status=Status.Paid},
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
        }
    }
}

