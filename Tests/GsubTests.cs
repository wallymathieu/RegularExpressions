using System;
using Xunit;
using WallyMathieu.RegularExpressions;

namespace Tests
{
    public class GsubTests
    {
        [Fact]
        public void test_some_back_reference()
        {
            var expected = "First sentence. Second sentence.\nThird sentence!\nFourth sentence?\nFifth sentence.";
            var input = "First sentence. Second sentence.Third sentence!Fourth sentence?Fifth sentence.";
            Assert.Equal(expected, input.Gsub("/([.!?])([A-Z1-9])/", "\\1\n\\2"));
        }

        [Fact]
        public void test_should_not_interpret_thing_without_slashes_as_regex()
        {
            var input = "First sentence. Second sentence.Third sentence!Fourth sentence?Fifth sentence.";
            Assert.Equal(input, input.Gsub("([.!?])([A-Z1-9])", "\\1\n\\2"));
        }

        [Fact]
        public void test_case()
        {
            var expected = "FX X.";
            var input = "First sentence.";
            Assert.Equal(expected, input.Gsub("/([a-z]+)/", "X"));
        }

        [Fact]
        public void test_ignore_case()
        {
            var expected = "X X.";
            var input = "First sentence.";
            Assert.Equal(expected, input.Gsub("/([a-z]+)/i", "X"));
        }

        [Fact]
        public void test_multiline()
        {
            var expected = @"X";
            var input = string.Join("\n", @"First sentence.".Split(' '));
            Assert.Equal(expected, input.Sub("/(.+)/m", "X"));
        }

        [Fact]
        public void test_not_multiline()
        {
            var expected = string.Join("\n", @"X sentence.".Split(' '));
            var input = string.Join("\n", @"First sentence.".Split(' '));
            Assert.Equal(expected, input.Sub("/(.+)/", "X"));
        }

        [Fact]
        public void test_ignore_pattern_whitespace()
        {
            var expected = @"X sentence.";
            var input = @"First sentence.";
            Assert.Equal(expected, input.Sub("/ First /x", "X"));
        }

        [Fact]
        public void test_back_reference_is_commented_out()
        {
            var expected = "\\1 sentence.";
            var input = "First sentence.";
            Assert.Equal(expected, input.Gsub("/([A-Z][a-z]+)/", @"\\1"));
        }
    }
}
