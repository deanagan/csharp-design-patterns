using FluentAssertions;
using Xunit;

namespace Interpreter.Test
{
    public class InterpreterShould
    {
        [Theory]
        [InlineData(new object[] {"IV", 4})]
        [InlineData(new object[] {"V", 5})]
        [InlineData(new object[] {"VI", 6})]
        [InlineData(new object[] {"LXXVI", 76})]
        [InlineData(new object[] {"CDXCIX", 499})]
        [InlineData(new object[] {"MMMDCCCLXXXVIII", 3888})]
        [InlineData(new object[] {"MCMXCVI", 1996})]
        [InlineData(new object[] {"MMMCMXCIX", 3999})]
        [InlineData(new object[] {"DCCCLXXXVIII", 888})]
        [InlineData(new object[] {"MDCLXVI", 1666})]
        [InlineData(new object[] {"MMMM", 0})] // Roman Numerals are in range 0 < number < 4000
        [InlineData(new object[] {"CXCX", 0})] // Invalid
        [InlineData(new object[] {"IVV", 0})] // Invalid
        [InlineData(new object[] {"IIII", 0})] // 4 consecutive numbers disallowed
        [InlineData(new object[] {"IIIIV", 0})] // 4 consecutive numbers disallowed
        [InlineData(new object[] {"XIIII", 0})] // 4 consecutive numbers disallowed
        [InlineData(new object[] {"XIVIIIVV", 0})] // Invalid
        [InlineData(new object[] {"VVXVVVVIII", 0})] // Invalid
        public void ReturnNumericValue_WhenGivenRomanNumerals(string romanNumerals, int expectedValue)
        {
            // Arrange
            var interpreter = new RomanNumeralInterpreter();
            // Act
            var numericValue = interpreter.Interpret(romanNumerals);
            // Assert
            numericValue.Should().Be(expectedValue);
        }
    }
}
