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

    [Params(100)]
    public int NumBooks;


    [GlobalSetup]
    public void Setup()
    {
        _readSqlContext.Database.EnsureDeleted();
        _readSqlContext.Database.EnsureCreated();
        _readSqlContext.Books.AddRange(CreateSqlBookData.CreateSqlDummyBooks(NumBooks));

        _readJsonContext.Database.EnsureDeleted();
        _readJsonContext.Database.EnsureCreated();
        _readJsonContext.Books.AddRange(CreateJsonBookData.CreateJsonDummyBooks(NumBooks));
    }

    [Benchmark]
    public void ReadSql()
    {
        var books = _readSqlContext.Books.MapBookToDto().ToArray();

    }

    [Benchmark]
    public void ReadJson()
    {

        var books = _readJsonContext.Books.MapBookTopToDto().ToList();
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