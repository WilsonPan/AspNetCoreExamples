
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<Data.EFCoreDbContext>())
            {
                // context.Database.EnsureDeleted();
                // context.Database.EnsureCreated();

                if (context.School.Any())
                {
                    return;
                }

                context.School.Add(new Models.School
                {
                    Name = "中山大学",
                    Address = "广东省广州市海珠区新港西路135号",
                    Students = new System.Collections.Generic.List<Models.Student>()
                    {
                        new Models.Student
                        {
                            Name= "张山",
                            Age = 18,
                        },
                        new Models.Student
                        {
                            Name= "李四",
                            Age = 20,
                        },
                        new Models.Student
                        {
                            Name= "王建",
                            Age = 21,
                        },
                    },
                });

                context.School.Add(new Models.School
                {
                    Name = "济南大学",
                    Address = "山东省济南市南辛庄西路336号",
                    Students = new System.Collections.Generic.List<Models.Student>()
                    {
                        new Models.Student
                        {
                            Name= "李梅",
                            Age = 18,
                        },
                        new Models.Student
                        {
                            Name= "韩磊磊",
                            Age = 19,
                        },
                        new Models.Student
                        {
                            Name= "李晓军",
                            Age = 22,
                        },
                    },
                });

                context.SaveChanges();
            }
        }
    }
}