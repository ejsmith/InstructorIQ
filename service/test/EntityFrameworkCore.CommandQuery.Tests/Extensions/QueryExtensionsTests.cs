using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using EntityFrameworkCore.CommandQuery.Extensions;
using EntityFrameworkCore.CommandQuery.Queries;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Extensions
{
    public class QueryExtensionsTests
    {
        [Fact]
        public void PageNormal()
        {
            var fruits = DataGenerator.Generator.Default.List<Fruit>(20);
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Page(1, 10)
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(10);
        }

        [Fact]
        public void PageNextPage()
        {
            var fruits = DataGenerator.Generator.Default.List<Fruit>(20);
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Page(2, 10)
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(10);

            list[0].Id.Should().Be(fruits[10].Id);
        }

        [Fact]
        public void PageNextPageOutOfRange()
        {
            var fruits = DataGenerator.Generator.Default.List<Fruit>(20);
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Page(3, 10)
                .ToList();

            list.Should().BeEmpty();
        }

        [Fact]
        public void PageIndexOutOfRange()
        {
            var fruits = DataGenerator.Generator.Default.List<Fruit>(20);
            fruits.Should().NotBeEmpty();

            Action act = () => fruits
                .AsQueryable()
                .Page(0, 0)
                .ToList();

            act.Should().Throw<ArgumentOutOfRangeException>();
        }


        [Fact]
        public void SortNormal()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Sort(new[] { new EntitySort { Name = "Name" } })
                .ToList();

            list.Should().NotBeEmpty();
            list[0].Name.Should().Be("Apple");
            list[9].Name.Should().Be("Strawberry");
        }

        [Fact]
        public void SortMixedCase()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Sort(new[] { new EntitySort { Name = "name" } })
                .ToList();

            list.Should().NotBeEmpty();
            list[0].Name.Should().Be("Apple");
            list[9].Name.Should().Be("Strawberry");
        }

        [Fact]
        public void SortDescending()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Sort(new[] { new EntitySort { Name = "Name", Direction = "Descending" } })
                .ToList();

            list.Should().NotBeEmpty();
            list[0].Name.Should().Be("Strawberry");
            list[9].Name.Should().Be("Apple");
        }

        [Fact]
        public void SortMultiple()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Sort(new[]
                {
                    new EntitySort { Name = "Rank" },
                    new EntitySort { Name = "Name" }
                })
                .ToList();

            list.Should().NotBeEmpty();

            list[0].Name.Should().Be("Pear");

            list[6].Name.Should().Be("Blueberry");
            list[7].Name.Should().Be("Raspberry");
            list[8].Name.Should().Be("Strawberry");

            list[9].Name.Should().Be("Banana");
        }


        [Fact]
        public void FilterNormal()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter { Name = "Rank", Value = 7 })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(3);
        }

        [Fact]
        public void FilterLogicalOr()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter
                {
                    Logic = "or",
                    Filters = new List<EntityFilter>
                    {
                        new EntityFilter{ Name = "Rank", Value = 7 },
                        new EntityFilter{ Name = "Name", Value = "Apple" }
                    }
                })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(4);
        }

        [Fact]
        public void FilterLogicalAnd()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter
                {
                    Filters = new List<EntityFilter>
                    {
                        new EntityFilter{ Name = "Rank", Value = 7 },
                        new EntityFilter{ Name = "Name", Value = "Blueberry" }
                    }

                })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(1);
        }

        [Fact]
        public void FilterComplex()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter
                {
                    Filters = new List<EntityFilter>
                    {
                        new EntityFilter{ Name = "Rank", Operator = ">", Value = 5 },
                        new EntityFilter
                        {
                            Logic = "or",
                            Filters = new List<EntityFilter>
                            {
                                new EntityFilter{ Name = "Name", Value = "Strawberry" },
                                new EntityFilter{ Name = "Name", Value = "Blueberry" }
                            }
                        }
                    }
                })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(2);
        }

        [Fact]
        public void FilterContains()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter { Name = "Name", Operator = "Contains", Value = "berry" })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(3);
        }

        [Fact]
        public void FilterNotContains()
        {
            var fruits = Fruit.Data();
            fruits.Should().NotBeEmpty();

            var list = fruits
                .AsQueryable()
                .Filter(new EntityFilter { Name = "Name", Operator = "!Contains", Value = "berry" })
                .ToList();

            list.Should().NotBeEmpty();
            list.Count.Should().Be(7);
        }
    }
}
