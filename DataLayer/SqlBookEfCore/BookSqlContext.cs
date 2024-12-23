// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.SqlBookEfCore;

public class BookSqlContext : DbContext
{
    public BookSqlContext(DbContextOptions<BookSqlContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<PriceOffer> PriceOffers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) //#E
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(x => new { x.BookId, x.AuthorId });
    }
}