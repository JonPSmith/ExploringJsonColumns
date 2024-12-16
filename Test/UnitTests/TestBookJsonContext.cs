// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.JsonBookEfCore;
using Test.MappingCode;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests;

public class TestBookJsonContext
{
    private readonly ITestOutputHelper _output;

    public TestBookJsonContext(ITestOutputHelper output) => _output = output;

    [Fact]
    public void BasicTestJsonBookContext()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        context.Add(new BookTop() { BookData = new JsonBookData { Title = "New Book" } });
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(1);
    }

    [Fact]
    public void TestJsonBookContext_FourBooksAllData()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();
        var fourBooks = CreateJsonBookData.ConvertSqlBookToJsonBook(CreateSqlBookData.CreateFourBooks());

        //ATTEMPT
        context.Books.AddRange(fourBooks);
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(4);

        foreach (var dto in context.Books.ToList().MapBookTopToDto())
        {
            string stars = dto.ReviewsCount == 0
                ? "No reviews"
                : $"NumReviews: {dto.ReviewsCount}, Stars: {((double)dto.ReviewsAverageVotes):0.00} ";
            _output.WriteLine($"{dto.Title}, Price {dto.ActualPrice}, Authors: {dto.AuthorsOrdered}, " +
                              $"Reviews: {stars}");
        }
    }

}