
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Iterator.Test
{

    public class Matrix2
{
    private double[,] storage = new double[3, 3];

    public double this[int row, int column]
    {
        // The embedded array will throw out of range exceptions as appropriate.
        get { return storage[row, column]; }
        set { storage[row, column] = value; }
    }
}

    public class IteratorShould
    {
        const int ROWS = 2;
        const int COLUMNS = 3;

        private struct MatrixValues
        {
            public int value;
            public int row;
            public int col;
        }

        public IteratorShould()
        {

        }

        private Matrix SetupIterable(Matrix.Direction direction)
        {
            var iterableObject = new Matrix(direction, ROWS, COLUMNS);
            foreach(var element in GetMatrixValues(ROWS, COLUMNS))
            {
                iterableObject[element.row, element.col] = element.value;
            }

            return iterableObject;
        }

        private IEnumerable<MatrixValues> GetMatrixValues(int rows, int columns)
        {
            return Enumerable.Range(1, rows*columns).Select((v, i) => new MatrixValues{ value = v, row = i / columns, col = i % columns});
        }

        [Fact]
        public void ReturnCorrectNumbers_WhenInvokedByRow()
        {
            var iterableObject = SetupIterable(Matrix.Direction.ByRow);
            var expected = 1;
            foreach(var num in iterableObject)
            {
                num.Should().Be(expected++);
            }

            expected = 1;
            foreach(var num in iterableObject)
            {
                num.Should().Be(expected++);
            }
        }

        [Fact]
        public void ReturnCorrectNumbers_WhenInvokedByColumn()
        {
            var iterableObject = SetupIterable(Matrix.Direction.ByColumn);
            var expected = new int[] { 1,4,2,5,3,6 };
            var index = 0;
            foreach(var num in iterableObject)
            {
                num.Should().Be(expected[index++]);
            }

            index = 0;
            foreach(var num in iterableObject)
            {
                num.Should().Be(expected[index++]);
            }
        }


    }
}
