﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace WorkspaceServer.Tests
{
    public static class StringAssertionExtensions
    {
        public static void ShouldMatch(this IReadOnlyCollection<string> actual, params string[] expected)
        {
            for (var i = 0; i < expected.Length; i++)
            {
                var item = actual.ElementAtOrDefault(i);

                item.Should().NotBeNull(because: $"expected to match a collection of {expected.Length} lines but only found {i}");
                
                item.Should().Match(expected[i]);
            }
        }

        public static void ShouldBeEquivalentTo(this IReadOnlyCollection<string> actual, params string[] expected)
            => ShouldMatch(actual.Select(x => x.Trim()).ToList(), expected.Select(x => x.Trim()).ToArray());

        public static void ShouldBeEquivalentTo(this string actual, string expected)
            => ShouldBeEquivalentTo(actual.EnforceLF().Split("\n"), expected.EnforceLF().Split("\n"));
    }
}