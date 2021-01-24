using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace Iterator.Test
{
    public class IteratorShould
    {
        private AggregateObject _iterableObject;

        public IteratorShould()
        {
        }

        [Fact]
        public void ReturnCorrectNumbers_WhenInvokedByRow()
        {
            _iterableObject = new Matrix(Matrix.Direction.ByRow, 3, 4);
            _iterableObject[0,0] = 20;
            _iterableObject[0,0] = 20;
            _iterableObject[0,0] = 20;
            _iterableObject[0,0] = 20;
        }


    }
}
