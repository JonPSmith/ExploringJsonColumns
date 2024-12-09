// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarking_xUNIT;

public class FastAndDirtyConfig : ManualConfig
{
    public FastAndDirtyConfig()
    {
        Add(DefaultConfig.Instance); // *** add default loggers, reporters etc? ***

        Add(Job.Default
                .WithLaunchCount(1)     // benchmark process will be launched only once
                //.WithIterationTime(100) // 100ms per iteration
                .WithWarmupCount(3)     // 3 warmup iteration
                //.WithTargetCount(3)     // 3 target iteration
        );
    }
}