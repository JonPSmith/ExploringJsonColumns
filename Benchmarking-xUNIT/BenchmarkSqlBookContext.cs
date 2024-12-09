// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using DataLayer.SqlBookEfCore;
using BenchmarkDotNet.Attributes;
using Test.TestHelpers;
using TestSupport.EfHelpers;

namespace Benchmarking_xUNIT;

[Config(typeof(FastAndDirtyConfig))]
public class BenchmarkSqlBookContext
{
    private DbContextOptions<SqlBookContext> _options;

    public BenchmarkSqlBookContext()
    {
        _options = this.CreateUniqueClassOptions<SqlBookContext>();
    }

    [Fact]
    public void AddReviewToBook()
    {
        var summary = BenchmarkRunner.Run<BenchmarkSqlBookContext>();
    }

    [Params(100)]
    public int NumBooks;

    [Benchmark]
    public void AddSqlBooks()
    {
        using var context = new SqlBookContext(_options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Books.AddRange(CreateSqlBookData.CreateDummyBooks(NumBooks));
        context.SaveChanges();
    }

    [Benchmark]
    public void ReadSqlBooks()
    {
        using var context = new SqlBookContext(_options);

        var books = context.Books.MapBookToDto().ToArray();
        if (books.Length != NumBooks)
        {
            throw new Exception($"should be {NumBooks}, but was books.Length");
        }

    }
}