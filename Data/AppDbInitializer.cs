using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading;
using MyBooks.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBooks.Data
{
	public class AppDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				if (!context.Books.Any())
				{
                    context.Books.AddRange(
                        new Book() {
                            Title = "1st Book Title",
                            description = "1st Book Description",
                            IsRead = true,
                            ReadAt = DateTime.Now.AddDays(-10),
                            Rate = 3,
                            Genre = "Komedi",
                            CoverUrl = "https://...",
                            CreatedAt = DateTime.Now.AddDays(-11)
                        },
                        new Book()
                        {
                            Title = "2st Book Title",
                            description = "2st Book Description",
                            IsRead = false,
                            Genre = "Filsafat",
                            CoverUrl = "https://...",
                            CreatedAt = DateTime.Now.AddDays(-11)
                        }
                    );

                    context.SaveChanges();
                }
			}
		}
	}
}

