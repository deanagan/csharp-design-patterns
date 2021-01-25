using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iterator
{
    public class Matrix : AggregateObject, IMatrix<int>
    {
        public enum Direction
        {
            ByRow,
            ByColumn
        }

        private int[,] _data;
        public Direction IteratorDirection { get; set; }

        public int TotalRows() => _data.GetLength(0);
        public int TotalColumns() => _data.GetLength(1);

        public Matrix(Direction iteratorDirection, int width, int height)
        {
            _data = new int[width, height];
            IteratorDirection = iteratorDirection;
        }

        public int this[int row, int column]
        {
            get { return _data[row, column]; }
            set { _data[row, column] = value; }
        }

        public override IEnumerator GetEnumerator()
        {
           return new MatrixIterator(this);
        }
    }
}
