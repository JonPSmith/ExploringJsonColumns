// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.JsonBookEfCore;

public class BookJsonDatesContext : DbContext
{
    public BookJsonDatesContext(
        DbContextOptions<BookJsonDatesContext> options)
        : base(options) { }

    public DbSet<BookTopWithDate> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookTopWithDate>().OwnsOne(
            headEntry => headEntry.BookData, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsOne(x => x.Promotion);
                ownedNavigationBuilder.OwnsMany(x => x.Authors);
                ownedNavigationBuilder.OwnsMany(x => x.Reviews);
            });
    }
}