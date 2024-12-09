// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Runtime.InteropServices;
using DataLayer.JsonBookClasses;
using DataLayer.JsonBookEfCore;
using DataLayer.SqlBookEfCore;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit;
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
        context.Add(new BookTop() { BookData = new JsonBookData{ Title = "New Book" } });
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(1);
    }

    [Theory]
    [InlineData(100)]
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

    [Fact]
    public void TestBookJsonContext_FourBooksAllData()
    {
        //SETUP
        var options = this.CreateUniqueClassOptions<BookJsonContext>();
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();

        var slqBooks = CreateSqlBookData.CreateFourBooks();

        //ATTEMPT
        context.Books.AddRange(CreateJsonBookData.ConvertSqlBookToJsonBook(slqBooks));
        context.SaveChanges();

        //VERIFY
        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(4);
        foreach (var bookTop in context.Books)
        {
            _output.WriteLine(bookTop.ToString());
        }
    }
}