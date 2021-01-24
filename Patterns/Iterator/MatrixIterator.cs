
 namespace Iterator
 {
    public class MatrixIterator : GenericIterator
    {
        private Matrix _matrix;

        // Stores the current traversal position. An iterator may have a lot of
        // other fields for storing iteration state, especially when it is
        // supposed to work with a particular kind of collection.
        private int _rowNumber = 0;
        private int _colNumber = 0;


        public MatrixIterator(Matrix matrix) => _matrix = matrix;

        private void Increment(ref int axis1, ref int axis2, int axis1Length, int axis2Length)
        {
            if (axis1 < axis1Length - 1)
            {
                axis1++;
            }
            else
            {
                axis1 = 0;
                if (axis2 < axis2Length - 1)
                {
                    axis2++;
                }
            }
        }

        public override bool HasNext()
        {
            return _colNumber + 1 == _matrix.TotalColumns() && _rowNumber + 1 == _matrix.TotalRows();
        }

        public override void Reset()
        {
            _colNumber = 0;
            _rowNumber = 0;
        }

        public override bool MoveNext()
        {
            var totalColumns = _matrix.TotalColumns();
            var totalRows = _matrix.TotalRows();

            if (_matrix.IteratorDirection == Matrix.Direction.ByRow)
            {
                Increment(ref _rowNumber, ref _colNumber, _matrix.TotalRows(), _matrix.TotalColumns());
            }
            else
            {
                Increment(ref _colNumber, ref _rowNumber, _matrix.TotalColumns(), _matrix.TotalRows());
            }

            return HasNext();
        }
        public override object Current()
        {
            return _matrix[_rowNumber, _colNumber];
        }


    }
 }