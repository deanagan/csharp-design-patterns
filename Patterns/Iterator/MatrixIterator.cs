
 namespace Iterator
 {
    public class MatrixIterator<T> : GenericIterator
    {
        private IMatrix<T> _matrix;

        private int _rowNumber;
        private int _colNumber;
        private readonly int _totalRows;
        private readonly int _totalColumns;
        private readonly MatrixDirection _matrixDirection;


        public MatrixIterator(IMatrix<T> matrix, MatrixDirection matrixDirection)
        {
            _matrix = matrix;
            _totalColumns = _matrix.TotalColumns();
            _totalRows = _matrix.TotalRows();
            _matrixDirection = matrixDirection;
            Reset();
        }

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
            return _colNumber < _matrix.TotalColumns() - 1 || _rowNumber < _matrix.TotalRows() - 1;
        }

        public override void Reset()
        {
            _rowNumber = _matrixDirection == MatrixDirection.ByColumn ? -1 : 0;
            _colNumber = _matrixDirection == MatrixDirection.ByRow ? -1 : 0;
        }

        public override bool MoveNext()
        {
            if (_matrixDirection == MatrixDirection.ByColumn)
            {
                Increment(ref _rowNumber, ref _colNumber, _totalRows, _totalColumns);
            }
            else
            {
                Increment(ref _colNumber, ref _rowNumber, _totalColumns, _totalRows);
            }

            return HasNext();
        }

        public override object Current()
        {
            if (_rowNumber == -1 || _colNumber == -1)
            {
                return null;
            }
            return _matrix[_rowNumber, _colNumber];
        }

    }
 }