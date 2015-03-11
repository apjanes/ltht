using FluentAssertions;
using Ltht.TechTest.Extensions;
using NUnit.Framework;

namespace Ltht.TechTest.Tests.Unit.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("FooBar", false)]
        [TestCase("FooooF", true)]
        [TestCase("Foooof", true)]
        [TestCase("Foo ooF", true)]
        public void IsPalindrome_WhenAsSpecified_ReturnsExpected(string sut, bool expected)
        {
            // Act
            var result = sut.IsPalindrome();

            // Assert
            result.Should().Be(expected);
        }
    }
}