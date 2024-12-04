﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.JsonBookEfCore;

public class BookJsonContext : DbContext
{
    public BookJsonContext(
        DbContextOptions<BookJsonContext> options)
        : base(options) { }

    public DbSet<BookTop> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookTop>().OwnsOne(
            headEntry => headEntry.BookData, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsOne(x => x.PriceOffer);
                ownedNavigationBuilder.OwnsMany(x => x.Authors);
                ownedNavigationBuilder.OwnsMany(x => x.Reviews);
            });
    }
}