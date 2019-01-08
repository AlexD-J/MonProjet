using System;
using System.Collections.Generic;

namespace Library.API.Entities
{
    public static class LibraryContextExtensions
    {
        public static void EnsureSeedDataForContext(this LibraryContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            context.UserAccounts.RemoveRange(context.UserAccounts);
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();

            // init seed data
            var userAccounts = new List<UserAccount>()
            {
                new UserAccount()
                {
                     Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                     FirstName = "Stephen",
                     LastName = "King",
                },
                new UserAccount()
                {
                     Id = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                     FirstName = "George",
                     LastName = "RR Martin",
                },
                new UserAccount()
                {
                     Id = new Guid("412c3012-d891-4f5e-9613-ff7aa63e6bb3"),
                     FirstName = "Neil",
                     LastName = "Gaiman",
                }
            };

            var products = new List<Product>()
            {
                new Product()
                {
                     Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                     Name = "Balvanie",
                     Description = "Description scotch",
                     Articles = new List<Article>()
                     {
                         new Article()
                         {
                             Id = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                             Title = "SAQ"
                         },
                         new Article()
                         {
                             Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                             Title = "QuebecWisky",
                         },
                         new Article()
                         {
                             Id = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                             Title = "Malt"
                         }
                     }
                },
                new Product()
                {
                    Id = new Guid("35320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Dao",
                    Description = "Description Vin",
                    Articles = new List<Article>()
                    {
                        new Article()
                        {
                            Id = new Guid("d7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                            Title = "SAQ"
                        }
                    }
                },
                new Product()
                {
                    Id = new Guid("35320c5e-f78a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Laga",
                    Description = "Description Bìere",
                    Articles = new List<Article>() { }
                }
            };
            context.UserAccounts.AddRange(userAccounts);
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
