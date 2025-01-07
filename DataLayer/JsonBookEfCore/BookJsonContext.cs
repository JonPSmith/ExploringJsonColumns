// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.JsonBookEfCore;

public class BookJsonContext(DbContextOptions<BookJsonContext> options)
    : DbContext(options)
{
    public DbSet<BookTop> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookTop>().OwnsOne(
            headEntry => headEntry.BookData, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsOne(x => x.Promotion);
                ownedNavigationBuilder.OwnsMany(x => x.Authors);
                ownedNavigationBuilder.OwnsMany(x => x.Reviews);
            });
    }
}