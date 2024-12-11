﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.JsonBookEfCore;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.PerformanceTests;

public class PerfBookJsonContext
{
    private readonly ITestOutputHelper _output;

    public PerfBookJsonContext(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestBookJsonContext_AddMany(int numBooks)
    {
        //SETUP
        var logs = new List<string>();
        var options = this.CreateUniqueClassOptionsWithLogTo<BookJsonContext>(logs.Add);
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonBookData.CreateDummyBooks(numBooks);

        //ATTEMPT
        using (new TimeThings(_output, $"Add {numBooks} Sql Books."))
        {
            context.Books.AddRange(dummyBooks);
            context.SaveChanges();
        }

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(numBooks);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestBookJsonContext_ReadBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonBookData.CreateDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();

        //ATTEMPT
        string[] books;
        using (new TimeThings(_output, $"Read {numBooks} Sql Books."))
        {
            books = context.Books.Select(x => x.ToString()).ToArray();
        }

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(numBooks);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestBookJsonContext_OrderByStars(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonBookData.CreateDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        BookTop[] books;
        using (new TimeThings(_output, $"OrderByStars {numBooks} Sql Books."))
        {
            books = context.Books.ToList().OrderBy(x => x.BookData.StarsValue()).ToArray();
        }

        //VERIFY
        double lastStar = 0.0;
        foreach (var book in books)
        {
            (book.BookData.StarsValue() >= lastStar).ShouldBeTrue($"{book.BookData.Title}");
            lastStar = book.BookData.StarsValue();
        }
        context.Books.Count().ShouldEqual(numBooks);
    }
}