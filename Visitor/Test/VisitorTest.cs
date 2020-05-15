using Xunit;
using Moq;
using System;
using FluentAssertions;

namespace Visitor
{
    public class VisitorShould
    {
        private GameElement _mario = new Sprite("Mario");
        private GameElement _luigi = new Sprite("Luigi");
        private TextElement _instruction = new Instruction();

        [Fact]
        public void EnableMarioOnly_WhenRevealerAppliedToMario()
        {
            // Arrange
            var revealMario = new Revealer();
            // Act
            _mario.Accept(revealMario);
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("sprite states"))
            {
                _mario.Active.Should().BeTrue();
                _luigi.Active.Should().BeFalse();
                _instruction.Active.Should().BeFalse();
            }
        }

        [Fact]
        public void EnableInstructionOnly_WhenRevealerAppliedToInstruction()
        {
            // Arrange
            var revealInstruction = new Revealer();
            // Act
            _instruction.Accept(revealInstruction);
            using (new FluentAssertions.Execution.AssertionScope("sprite states"))
            {
                _mario.Active.Should().BeFalse();
                _luigi.Active.Should().BeFalse();
                _instruction.Active.Should().BeTrue();
            }
        }

        [Fact]
        public void BeVisitedByMockObjectOnce_WhenAcceptionMockVisitor()
        {
            // Arrange
            var mockVisitor = new Mock<IVisitor>();
            // Act
            _mario.Accept(mockVisitor.Object);
            // Assert
            mockVisitor.Verify(dummyVisitor => dummyVisitor.Visit(_mario), Times.Once);
        }

    }
}
