using System.Collections;

namespace Iterator
{
    public class NumberMatrix : AggregateObject, IMatrix<int>
    {
        private int[,] _data;
        public MatrixDirection IteratorDirection { get; set; }

        public int TotalRows() => _data.GetLength(0);
        public int TotalColumns() => _data.GetLength(1);

        public NumberMatrix(MatrixDirection iteratorDirection, int width, int height)
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
           return new MatrixIterator<int>(this, IteratorDirection);
        }
    }
}
