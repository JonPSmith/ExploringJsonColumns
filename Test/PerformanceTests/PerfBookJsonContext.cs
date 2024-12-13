// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

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
        List<BookListDto> books;
        using (new TimeThings(_output, $"Read {numBooks} Sql Books."))
        {
            books = context.Books.MapBookTopToDto().ToList();
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
        List<BookListDto> bookDtos;
        using (new TimeThings(_output, $"OrderByStars {numBooks} Sql Books."))
        {
            bookDtos = context.Books.MapBookTopToDto().ToList().OrderBy(x => x.ReviewsAverageVotes).ToList();
        }

        //VERIFY
        double? lastStar = null;
        foreach (var bookDto in bookDtos)
        {
            (bookDto.ReviewsAverageVotes == null 
                ? lastStar == null 
                : bookDto.ReviewsAverageVotes >= (lastStar ?? 0)).ShouldBeTrue("{bookDto.Title}");
            lastStar = bookDto.ReviewsAverageVotes;
        }
        context.Books.Count().ShouldEqual(numBooks);
    }
}