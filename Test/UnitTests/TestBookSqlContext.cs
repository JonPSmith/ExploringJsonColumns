// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using DataLayer.BookClasses;
using DataLayer.SqlBookEfCore.EfCode;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests;

public class TestBookSqlContext
{
    private readonly ITestOutputHelper _output;

    public TestBookSqlContext(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestExample()
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
}