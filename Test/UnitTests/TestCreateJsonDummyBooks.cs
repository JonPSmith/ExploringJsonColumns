// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookEfCore;
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
        var options = this.CreateUniqueClassOptions<BookSqlContext>();
        using var context = new BookSqlContext(options);
        context.Database.EnsureClean();

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
}