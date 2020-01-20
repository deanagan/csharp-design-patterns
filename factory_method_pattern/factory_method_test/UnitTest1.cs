using NUnit.Framework;

namespace FrequentFlyers.Test
{
    public class Tests
    {
        private EarningBonusCalculator _calculator;
        
        [SetUp]
        public void Setup()
        {
            _calculator = new EarningBonusCalculator();
        }

        [Test]
        public void AddingMilesAtBasicPerks_AddsBasicMilesEarned()
        {
            Assert.That(_calculator.UpdatedMiles(8000, 2000) == 10000, Is.True);
        }

        [Test]
        public void AddingMilesAtGoldPerks_AddsGoldPerkMilesEarned()
        {
            
            Assert.That(_calculator.UpdatedMiles(20000, 2000) == 24000, Is.True);
        }

        [Test]
        public void AddingMilesAtSilverPerks_AddsSilverPerkMilesEarned()
        {
            System.Console.WriteLine(_calculator.UpdatedMiles(10000, 2000));
            Assert.That(_calculator.UpdatedMiles(10000, 2000) == 13000, Is.True);
        }
    }
}