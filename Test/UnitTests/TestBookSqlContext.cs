// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using DataLayer.SqlBookEfCore;
using Microsoft.EntityFrameworkCore;
using Test.MappingCode;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests;

public class TestBookSqlContext
{
    private readonly ITestOutputHelper _output;

    public TestBookSqlContext(ITestOutputHelper output) => _output = output;

    [Fact]
    public void BasicTestSqlBookContext()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        context.Add(new Book { Title = "New Book" });
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(1);
    }

    [Fact]
    public void TestSqlBookContext_FourBooksAllData()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        context.Books.AddRange(CreateSqlBookData.CreateFourBooks());
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(4);

        foreach (var dto in context.Books.MapBookToDto())
        {
            string stars = dto.ReviewsCount == 0
                    ? "No reviews"
                    : $"NumReviews: {dto.ReviewsCount}, Stars: { ((double) dto.ReviewsAverageVotes):0.00} ";
                _output.WriteLine($"{dto.Title}, Price {dto.ActualPrice}, Authors: {dto.AuthorsOrdered}, " +
                                  $"Reviews: {stars}");
        }
    }
}