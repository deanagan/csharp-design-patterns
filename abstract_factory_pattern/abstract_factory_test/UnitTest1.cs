using NUnit.Framework;
using Laptop;
namespace Laptop.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UseLenovoPartsFactory_ExpectProcessorMatch()
        {
            var lenovo = new LenovoPartsFactory();
            var processor = lenovo.CreateProcessor();
            
            Assert.That(processor.BrandName(), Is.EqualTo("AMD"));
            Assert.That(processor.SpeedInGigaHertz(), Is.EqualTo(2.1));
        }

        
        [Test]
        public void UseDellPartsFactory_ExpectProcessorMatch()
        {
            var dell = new DellPartsFactory();
            var processor = dell.CreateProcessor();
            
            Assert.That(processor.BrandName(), Is.EqualTo("Intel"));
            Assert.That(processor.SpeedInGigaHertz(), Is.EqualTo(1.8));
        }
    }
}