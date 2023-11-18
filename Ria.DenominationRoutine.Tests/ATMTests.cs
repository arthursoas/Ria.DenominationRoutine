using Shouldly;
using Xunit;

namespace Ria.DenominationRoutine.Tests
{
    public class ATMTests
    {
        [Fact]
        public void CalculatePayoutCombinations_WhenAmountIsUnreacheble_ShouldReturnEmpty()
        {
            // Arrange
            var amount = 150;
            var denominations = new[] { 100 };

            // Act
            var combinations = ATM.CalculatePayoutCombinations(denominations, amount);

            // Assert
            combinations.ShouldBeEmpty();
        }

        [Fact]
        public void CalculatePayoutCombinations_WhenAmountIsLowerThanAnyDenomination_ShouldReturnEmpty()
        {
            // Arrange
            var amount = 3;
            var denominations = new[] { 100, 50, 10, 5 };

            // Act
            var combinations = ATM.CalculatePayoutCombinations(denominations, amount);

            // Assert
            combinations.ShouldBeEmpty();
        }

        [Fact]
        public void CalculatePayoutCombinations_WhenAmountIsPossible_ShouldReturnAllPossibilites()
        {
            // Arrange
            var amount = 100;
            var denominations = new[] { 100, 50, 10 };

            // Act
            var combinations = ATM.CalculatePayoutCombinations(denominations, amount);

            // Assert
            combinations.Count.ShouldBe(4);
            combinations.ShouldContain(p => p[100] == 1);
            combinations.ShouldContain(p => p[50] == 2);
            combinations.ShouldContain(p => p[50] == 1 && p[10] == 5);
            combinations.ShouldContain(p => p[10] == 10);
        }

        [Fact]
        public void CalculatePayoutCombinations_WhenThereAreTooManyPossibilites_ShouldNotThrowMemoryException()
        {
            // Arrange
            var amount = 1000;
            var denominations = new[] { 100, 50, 10, 5, 2 };

            // Act
            var combinations = ATM.CalculatePayoutCombinations(denominations, amount);

            // Assert
            combinations.Count.ShouldBe(116996);
        }

        [Fact]
        public void FormatCombinationsOutput_WhenCombinationsAreEmpty_ShouldReturnEmptyString()
        {
            // Arrange
            var combinations = new List<IDictionary<int, int>>();

            // Act
            var output = ATM.FormatCombinationsOutput(combinations, "USD");

            // Assert
            output.ShouldBeEmpty();
        }

        [Fact]
        public void FormatCombinationsOutput_WhenDenominationHasZeroUsage_ShouldNotOutputZero()
        {
            // Arrange
            var combinations = new List<IDictionary<int, int>>
            {
                new Dictionary<int, int>
                {
                    { 50, 2 },
                    { 10, 0 },
                    { 05, 1 },
                }
            };

            // Act
            var output = ATM.FormatCombinationsOutput(combinations, "USD");

            // Assert
            output.ShouldBe("2 x 50 USD + 1 x 5 USD\n");
        }
    }
}
