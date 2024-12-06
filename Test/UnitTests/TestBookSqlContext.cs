// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using DataLayer.SqlBookEfCore;
using Microsoft.EntityFrameworkCore;
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
    public void TestSqlBookContext_Many()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<SqlBookContext>();
        using var context = new SqlBookContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        context.Books.AddRange(CreateSqlBookData.CreateDummyBooks());
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(10);
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
        foreach (var book in context.Books.Include(x => x.Authors))
        {
            _output.WriteLine($"{book.Title}, Price {book.Price}, Author: {book.Authors.First().Name}");
        }
    }
}