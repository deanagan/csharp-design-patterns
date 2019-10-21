using Xunit;
using Moq;
using System;


namespace visitor_pattern
{
    public class TestElementVisitor
    {

        Sprite mario = new Sprite("Mario");
        Sprite luigi = new Sprite("Luigi");
        Instruction instruction = new Instruction();
        
        [Fact]
        public void AddMarioAndLuigiAndInstruction_AllElementNamesInitialStateCorrect()
        { 
            Assert.Equal("Mario", mario.Name);
            Assert.Equal("Luigi", luigi.Name);

            Assert.False(mario.Active);
            Assert.False(luigi.Active);
            Assert.False(instruction.Active);
        }

        [Fact]
        public void EnableMario_AllElementStateCorrect()
        {
            var revealMario = new Revealer();

            mario.Accept(revealMario);    

            Assert.True(mario.Active);
            Assert.False(luigi.Active);
            Assert.False(instruction.Active);
        }

        [Fact]
        public void EnableInstructionOnly_AllElementStateCorrect()
        {
            var revealInstruction = new Revealer();

            instruction.Accept(revealInstruction);    

            Assert.False(mario.Active);
            Assert.False(luigi.Active);
            Assert.True(instruction.Active);
        }

        [Fact]
        public void MockVisitorObjectGetsVisited_MockObjectGetsAccepted()
        {
            var mockVisitor = new Mock<IVisitor>();
            mario.Accept(mockVisitor.Object);
            mockVisitor.Verify(dummyVisitor => dummyVisitor.Visit(mario), Times.Once());            
        }
    }
}
