﻿using System;
using Xunit.Abstractions;

namespace EntityFrameworkCore.CommandQuery.Tests
{
    public abstract class UnitTestBase
    {
        protected UnitTestBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;

        }

        public ITestOutputHelper OutputHelper { get; }
    }
}