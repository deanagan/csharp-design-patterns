using FluentAssertions;
using Xunit;

namespace Interpreter.Test
{
    public class InterpreterShould
    {
        [Theory]
        [InlineData(new object[] {"VI", 6})]
        // [InlineData(new object[] {"LXXVI", 76})]
        // [InlineData(new object[] {"CDXCIX", 499})]
        // [InlineData(new object[] {"MMMDCCCLXXXVIII", 3888})]
        public void ReturnNumericValue_WhenGivenRomanNumerals(string romanNumerals, int expectedValue)
        {
            // Arrange
            var interpreter = new Interpreter();
            // Act
            var numericValue = interpreter.Interpret(romanNumerals);
            // Assert
            numericValue.Should().Be(numericValue);
        }
    }
}
