// Copyright (c) 2023 Lykke Corp.
// See the LICENSE file in the project root for more information.

using System;
using Lykke.Common.EnumerableExtensions;
using Lykke.Common.ListExtensions;
using Xunit;

namespace Common.Tests;

public class EnumerableSingleTests
{
    [Fact]
    public void Single_WhenSequenceContainsNoMatchingElement_ThrowsInvalidOperationException()
    {
        var sequence = new[] { 1, 2, 3 };
        var predicate = new DescribedPredicate<int>(x => x == 4, "x => x == 4");

        var ex = Assert.Throws<InvalidOperationException>(() => sequence.Single(predicate));
        
        Assert.NotNull(ex);
        Assert.True(ex.Message.Contains("no matching element"));
        Assert.True(ex.Message.Contains("x => x == 4"));
    }
    
    [Fact]
    public void Single_WhenSequenceContainsMoreThanOneMatchingElement_ThrowsInvalidOperationException()
    {
        var sequence = new[] { 1, 2, 3, 3 };
        var predicate = new DescribedPredicate<int>(x => x == 3, "x => x == 3");

        var ex = Assert.Throws<InvalidOperationException>(() => sequence.Single(predicate));
        
        Assert.NotNull(ex);
        Assert.True(ex.Message.Contains("more than one matching element"));
        Assert.True(ex.Message.Contains("x => x == 3"));
    }
    
    [Fact]
    public void Single_WhenSequenceContainsOneMatchingElement_ReturnsElement()
    {
        var sequence = new[] { 1, 2, 3 };
        var predicate = new DescribedPredicate<int>(x => x == 3, "x => x == 3");

        var result = sequence.Single(predicate);
        
        Assert.Equal(3, result);
    }
    
    [Fact]
    public void Single_WhenSequenceContainsNoElements_ThrowsInvalidOperationException()
    {
        var sequence = Array.Empty<int>();
        var description = "description";

        var ex = Assert.Throws<InvalidOperationException>(() => sequence.Single(description));
        
        Assert.NotNull(ex);
        Assert.True(ex.Message.Contains("no matching element"));
        Assert.True(ex.Message.Contains("description"));
    }
    
    [Fact]
    public void Single_WhenSequenceContainsMoreThanOneElement_ThrowsInvalidOperationException()
    {
        var sequence = new[] { 1, 2, 3 };
        var description = "description";

        var ex = Assert.Throws<InvalidOperationException>(() => sequence.Single(description));
        
        Assert.NotNull(ex);
        Assert.True(ex.Message.Contains("more than one matching element"));
        Assert.True(ex.Message.Contains("description"));
    }
}
