using NUnit.Framework;
using Moq;
using System;
using visitor_lib;

namespace visitor_test
{
    public class Tests
    {
        private Sprite _mario;
        private Sprite _luigi;
        private Instruction _instruction;

        [SetUp]
        public void SetUp()
        {
            _mario = new Sprite("Mario");
            _luigi = new Sprite("Luigi");
            _instruction = new Instruction();
        }

        [Test]
        public void AddMarioAndLuigiAndInstruction_AllElementNamesInitialStateCorrect()
        {
            Assert.That("Mario", Is.EqualTo(_mario.Name));
            Assert.That("Luigi", Is.EqualTo(_luigi.Name));

            Assert.False(_mario.Active);
            Assert.False(_luigi.Active);
            Assert.False(_instruction.Active);
        }

        [Test]
        public void EnableMario_AllElementStateCorrect()
        {
            var revealMario = new Revealer();

            _mario.Accept(revealMario);

            Assert.True(_mario.Active);
            Assert.False(_luigi.Active);
            Assert.False(_instruction.Active);
        }

        [Test]
        public void EnableInstructionOnly_AllElementStateCorrect()
        {
            var revealInstruction = new Revealer();

            _instruction.Accept(revealInstruction);

            Assert.False(_mario.Active);
            Assert.False(_luigi.Active);
            Assert.True(_instruction.Active);
        }

        [Test]
        public void MockVisitorObjectGetsVisited_MockObjectGetsAccepted()
        {
            var mockVisitor = new Mock<IVisitor>();
            _mario.Accept(mockVisitor.Object);
            mockVisitor.Verify(dummyVisitor => dummyVisitor.Visit(_mario), Times.Once());
        }

    }
}
