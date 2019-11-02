using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Razor.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<RazorDbContext>>();
            using (var context = new RazorDbContext(options))
            {
                if (context.Book.Any())
                {
                    return;
                }

                context.Book.AddRange(
                    new Models.Book
                    {
                        Name = "算法之美",
                        UnitPrice = 26.99m,
                        PublicationDate = DateTime.Parse("2016-08-20"),
                        CreateTime = DateTime.Now
                    },
                    new Models.Book
                    {
                        Name = "大话设计模式",
                        UnitPrice = 28.03m,
                        PublicationDate = DateTime.Parse("2015-06-12"),
                        CreateTime = DateTime.Now
                    },
                    new Models.Book
                    {
                        Name = "幸福的陷阱",
                        UnitPrice = 30.59m,
                        PublicationDate = DateTime.Parse("2018-09-13"),
                        CreateTime = DateTime.Now
                    },
                    new Models.Book
                    {
                        Name = "墨菲定律",
                        UnitPrice = 32.12m,
                        PublicationDate = DateTime.Parse("2018-09-13"),
                        CreateTime = DateTime.Now
                    }
                );

                context.SaveChanges();
            }
        }
    }
}