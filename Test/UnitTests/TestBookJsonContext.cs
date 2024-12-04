// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.JsonBookEfCore;
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

    [Fact]
    public void TestBookJsonContext_Many()
    {
        //SETUP
        var logs = new List<string>();
        var options = this.CreateUniqueClassOptionsWithLogTo<BookJsonContext>(log => logs.Add(log));
        using var context = new BookJsonContext(options);
        context.Database.EnsureClean();

        //ATTEMPT
        context.Books.AddRange(CreateJsonBookData.CreateDummyBooks());
        context.SaveChanges();



        //VERIFY
        foreach (var log in logs)
        {
            _output.WriteLine(log);
        }

        context.ChangeTracker.Clear();
        context.Books.Count().ShouldEqual(10);
        foreach (var bookTop in context.Books)
        {
          _output.WriteLine(bookTop.ToString());
        }

    }
}