// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookEfCore;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.PerformanceTests;

public class PerfBookSqlContext
{
    private readonly ITestOutputHelper _output;

    public PerfBookSqlContext(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestSqlBookContext_AddBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateSqlBookData.CreateDummyBooks(numBooks);

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
    public void TestSqlBookContext_ReadBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateSqlBookData.CreateDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();

        //ATTEMPT
        BookListDto[] books;
        using (new TimeThings(_output, $"Read {numBooks} Sql Books."))
        {
            books = context.Books.MapBookToDto().ToArray();
        }

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(numBooks);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public void TestSqlBookContext_OrderByStars(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateSqlBookData.CreateDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        BookListDto[] books;
        using (new TimeThings(_output, $"OrderByStars {numBooks} Sql Books."))
        {
            books = context.Books.MapBookToDto().OrderBy(x => x.ReviewsAverageVotes).ToArray();
        }

        //VERIFY
        double lastStar = 0;
        foreach (var book in books)
        {
            ((book.ReviewsAverageVotes ?? 0) >= lastStar).ShouldBeTrue($"{book.Title}");
            lastStar = book.ReviewsAverageVotes ?? 0;
        }
    }
}