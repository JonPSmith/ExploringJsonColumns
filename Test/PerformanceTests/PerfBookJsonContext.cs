// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.JsonBookEfCore;
using Test.Dtos;
using Test.MappingCode;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;
using static Test.TestHelpers.CreateJsonBookData;

namespace Test.PerformanceTests;

public class PerfBookJsonContext
{
    private readonly ITestOutputHelper _output;

    public PerfBookJsonContext(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestBookJsonContext_AddBooks(int numBooks)
    {
        //SETUP
        var logs = new List<string>();
        var options = this.CreateUniqueClassOptionsWithLogTo<BookJsonContext>(logs.Add);
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonDummyBooks(numBooks);

        //ATTEMPT
        using (new TimeThings(_output, $"Add Json {numBooks} books"))
        {
            context.Books.AddRange(dummyBooks);
            context.SaveChanges();
        }

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(numBooks);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestBookJsonContext_ReadBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();

        //ATTEMPT
        List<BookListDto> books;
        using (new TimeThings(_output, $"Read Json {numBooks} books"))
        {
            books = context.Books.MapBookTopToDto().ToList();
        }

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(numBooks);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestBookJsonContext_OrderByStars(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        List<BookListDto> bookDtos;
        using (new TimeThings(_output, $"OrderByStars Json {numBooks} books"))
        {
            bookDtos = context.Books.MapBookTopToDto().OrderBy(x => x.ReviewsAverageVotes).ToList();
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

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestBookJsonContext_FindAuthorsBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        List<string> bookTitles;
        using (new TimeThings(_output, $"FindAuthorsBooks Json {numBooks} books"))
        {
            bookTitles = context.Books.MapBooksByAuthor("CommonAuthor0000").ToList();
        }

        //VERIFY
        bookTitles.Count().ShouldEqual(10);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    public void TestBookJsonContext_JsonBookDataFilterBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var dummyBooks = CreateJsonDummyBooks(numBooks);
        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        List<BookTop> bookTitles;
        using (new TimeThings(_output, $"FindAuthorsBooks Json {numBooks} books"))
        {
            bookTitles = context.Books.SelectBooksByPublishedOn().ToList();
        }

        //VERIFY
        bookTitles.Count.ShouldEqual(3);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(110)]
    public void TestBookJsonDatesContext_BooksTopFilterBooks(int numBooks)
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonDatesContext>();
        using var context = new BookJsonDatesContext(options);
        context.Database.EnsureClean();
        BookTopWithDate[] dummyBooks = CreateJsonDummyBooks(numBooks)
            .Select(x => new BookTopWithDate{ BookData = x.BookData, PublishedOn = x.BookData.PublishedOn }).ToArray();

        context.Books.AddRange(dummyBooks);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        //ATTEMPT
        List<BookTopWithDate> bookTitles;
        using (new TimeThings(_output, $"FindAuthorsBooks Json {numBooks} books"))
        {
            bookTitles = context.Books.Where(x =>
                x.PublishedOn >= new DateOnly(2024, 12, 4) &&
                x.PublishedOn <= new DateOnly(2024, 12, 6)).ToList();
        }

        //VERIFY
        bookTitles.Count().ShouldEqual(3);
    }
}