﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookEfCore;
using Test.MappingCode;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests;

public class TestCreateJsonDummyBooks
{
    private readonly ITestOutputHelper _output;

    public TestCreateJsonDummyBooks(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestCreateJsonDummyBooks_CheckBooks()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookSqlContext>();
        using var context = new BookSqlContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(12);

        //VERIFY
        books.Count.ShouldEqual(12);
    }

    [Fact]
    public void TestCreateJsonDummyBooks_CheckReviews()
    {
        //SETUP

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(12, 4);

        //VERIFY
        books.Count.ShouldEqual(12);
        books.ForEach(x => (x.Reviews?.Count ?? 0).ShouldBeInRange(0, 4));
        for (int j = 0; j < 12; j++)
        {
            _output.WriteLine($"{j:D}, Num reviews = {books[j].Reviews?.Count ?? 0}");
        }
    }

    [Fact]
    public void TestCreateJsonDummyBooks_CheckCommonAuthor()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookSqlContext>();
        using var context = new BookSqlContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(12, 4, 6);

        //VERIFY
        books.Count.ShouldEqual(12);
        books.ForEach(x => (x.AuthorsLink.Last().Author.Name).ShouldBeInRange("CommonAuthor0000", "CommonAuthor0001"));
        for (int j = 0; j < 12; j++)
        {
            _output.WriteLine($"{j:D}, CommonAuthorName = {books[j].AuthorsLink.Last().Author.Name}");
        }
    }

    [Fact]
    public void TestCreateJsonDummyBooks_CheckPromotions()
    {
        //SETUP

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(12, 4, 6, 3);

        //VERIFY
        books.Count.ShouldEqual(12);
        books.Count(x => x.Promotion != null).ShouldEqual(4);
        foreach (var book in books)
        {
            if (book.Promotion != null)
                _output.WriteLine($"{book.Title}: Price was {book.Price}, but is now {book.Promotion.NewPrice}");
        }
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestCreateJsonDummyBooks_CountBooksParts(int numBooks)
    {
        //SETUP

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(numBooks);

        //VERIFY
        _output.WriteLine($"{numBooks} books");
        var commonAuthors = 0;
        var reviewsCount = 0;
        foreach (var book in books)
        {
            if (book.AuthorsLink.Any(x => x.Author.Name == "CommonAuthor0000"))
            {
                commonAuthors++;
            }

            reviewsCount += (book.Reviews?.Count ?? 0);
        }

        _output.WriteLine($"{commonAuthors} Authors books");
        _output.WriteLine($"{reviewsCount} Reviews");
        _output.WriteLine($"{books.Count(x => x.Promotion != null)} Promotions");
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestCreateJsonDummyBooks_CountSimpleBooksParts(int numBooks)
    {
        //SETUP

        //ATTEMPT
        var books = CreateSqlBookData.CreateSqlDummyBooks(numBooks, 0, 0, 0);

        //VERIFY
        _output.WriteLine($"{numBooks} books");
        var commonAuthors = 0;
        var reviewsCount = 0;
        foreach (var book in books)
        {
            if (book.AuthorsLink.Any(x => x.Author.Name == "CommonAuthor0000"))
            {
                commonAuthors++;
            }

            reviewsCount += (book.Reviews?.Count ?? 0);
        }

        _output.WriteLine($"{commonAuthors} Authors books");
        _output.WriteLine($"{reviewsCount} Reviews");
        _output.WriteLine($"{books.Count(x => x.Promotion != null)} Promotions");
    }
}