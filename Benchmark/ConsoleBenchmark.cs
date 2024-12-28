// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using DataLayer.JsonBookEfCore;
using DataLayer.SqlBookEfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.MappingCode;
using Test.TestHelpers;
using TestSupport.Helpers;

namespace Benchmark;

[Config(typeof(AntiVirusFriendlyConfig))]
public class ConsoleBenchmark
{
    public readonly BookSqlContext _readSqlContext;
    public readonly BookJsonContext _readJsonContext;

    public ConsoleBenchmark()
    {
        var config = AppSettings.GetConfiguration();

        _readSqlContext = new BookSqlContext(GetContextOptions<BookSqlContext>(nameof(_readSqlContext), config));
        _readJsonContext = new BookJsonContext(GetContextOptions<BookJsonContext>(nameof(_readJsonContext), config));
    }

    private DbContextOptions<TContest> GetContextOptions<TContest>(string contextName, IConfigurationRoot config) 
        where TContest : DbContext
    {
        var connectionString = config.GetConnectionString(contextName);
        var builder = new
            DbContextOptionsBuilder<TContest>();
        builder.UseSqlServer(connectionString);
        return builder.Options;
    }

    private void CreateBooksAndSave(bool jsonApproach, bool simple)
    {
        if (jsonApproach)
        {
            _readJsonContext.ChangeTracker.Clear();

            _readJsonContext.Database.EnsureDeleted();
            _readJsonContext.Database.EnsureCreated();
            
            _readJsonContext.Books.AddRange(
                simple
                    ? CreateJsonBookData.CreateJsonDummyBooks(NumBooks, 0, 0, 0)
                    : CreateJsonBookData.CreateJsonDummyBooks(NumBooks)
                    );
            _readJsonContext.SaveChanges();
        }
        else
        {
            _readSqlContext.ChangeTracker.Clear();

            _readSqlContext.Database.EnsureDeleted();
            _readSqlContext.Database.EnsureCreated();
            _readSqlContext.Books.AddRange(
                simple
                    ? CreateSqlBookData.CreateSqlDummyBooks(NumBooks, 0, 0, 0)
                    : CreateSqlBookData.CreateSqlDummyBooks(NumBooks)
            );
            _readSqlContext.SaveChanges();
        }
    }

    [Params(10, 100)]
    public int NumBooks;

    [Benchmark]
    public void AddSql()
    {
        CreateBooksAndSave(false, true); //Json
    }

    [Benchmark]
    public void AddJson()
    {
        CreateBooksAndSave(true, true); //Sql
    }

    [Benchmark]
    public void ReadSql()
    {
        _readSqlContext.ChangeTracker.Clear();
        var books = _readSqlContext.Books.MapBookToDto().ToArray();
    }

    [Benchmark]
    public void ReadJson()
    {
        _readJsonContext.ChangeTracker.Clear();
        var books = _readJsonContext.Books.MapBookTopToDto().ToList();
    }

    [Benchmark]
    public void OrderSql()
    {
        _readSqlContext.ChangeTracker.Clear();
        var books = _readSqlContext.Books.MapBookToDto().OrderBy(x => x.ReviewsAverageVotes).ToArray();
    }

    [Benchmark]
    public void OrderJson()
    {
        _readJsonContext.ChangeTracker.Clear();
        var books = _readJsonContext.Books.MapBookTopToDto().OrderBy(x => x.ReviewsAverageVotes).ToArray();
    }

    [Benchmark]
    public void AuthorSql()
    {
        _readSqlContext.ChangeTracker.Clear();
        var books = _readSqlContext.Books.MapBooksByAuthor("CommonAuthor0009").ToArray();
    }

    [Benchmark]
    public void AuthorJson()
    {
        _readJsonContext.ChangeTracker.Clear();
        var books = _readJsonContext.Books.MapBooksByAuthor("CommonAuthor0009").ToList();
    }
}

public class AntiVirusFriendlyConfig : ManualConfig
{
    public AntiVirusFriendlyConfig()
    {
        AddJob(Job.MediumRun
            .WithToolchain(InProcessNoEmitToolchain.Instance));
    }
}

class Program
{
    // public static void Main(string[] args)
    // {
    //     BenchmarkRunner.Run(typeof(Program).Assembly);
    // }

    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ConsoleBenchmark>();
    }
}