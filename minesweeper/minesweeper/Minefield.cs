namespace minesweeper
{
    public class MineField
    {
        public int[,] internalCamp;

        public int x, y;

        MineField(int x = 1, int y = 1)
        {
            x = x >= 1 ? x : 1;
            y = y >= 1 ? y : 1;
            BuildGrid(0, 0, x, y);
        }

        private void BuildGrid(int x, int y, int width, int height)
        {
            var internalCamp = new int[width, height];
            for (int row = x; row < width; row++)
            {
                for (int column = y; column < height; column++)
                    this.internalCamp[row, column] = 0;
            }
        }

        public void _setX(int width)
        {
            width = width < 0 ?? 1;
            BuildGrid(0, 0, width, this.y);
            var x = width;
        }

        public int GetX()
        {
            return x;

        }

        public void SetY(int height)
        {
            height = height >= 0 ? height : 1;

            BuildGrid(0, 0, x, height);
            y = height;
        }

        public int GetY()
        {
            return y;
        }

        public int[,] GetCamp()
        {
            _solve();
            return this.internalCamp;
        }

        string Display(int x, int y)
        {
            if (internalCamp[x, y] == 0)
                return " ";
            else if (internalCamp[x, y] == -1)
                return "*";

            return internalCamp[x, y].ToString();
        }

        void SetBomb(int x, int y)
        {
            internalCamp[x, y] = -1;
        }

        void ClearGrid(int x, int y)
        {
            internalCamp[x, y] = 0;
        }

        private bool _existsInCamp(int x, int y)
        {
            return internalCamp[x, y] != 0;
        }

        private void _increment(int x, int y)
        {
            if (_existsInCamp(x, y) && internalCamp[x, y] > -1)
                internalCamp[x, y]++;
        }

        private bool _isBomb(int x, int y)
        {
            return internalCamp[x, y] == -1;
        }

        private void _clear()
        {
            for (int r = 0; r < this.x; r++)
            {
                for (int c = 0; c < this.y; c++)
                {
                    if (!_isBomb(r, c))
                        internalCamp[r, c] = 0;
                }
            }
        }

        public void ChangeBomb(int x, int y)
        {
            if (_isBomb(x, y))
                ClearGrid(x, y);
            else
                SetBomb(x, y);
        }

        private void _solve()
        {
            this._clear();
            for (int r = 0; r < this.x; r++)
            {
                for (int c = 0; c < this.y; c++)
                {
                    if (this._isBomb(r, c))
                    {
                        _increment(r - 1, c - 1);
                        _increment(r - 1, c);
                        _increment(r - 1, c + 1);
                        _increment(r, c - 1);
                        _increment(r, c + 1);
                        _increment(r + 1, c - 1);
                        _increment(r + 1, c);
                        _increment(r + 1, c + 1);
                    }
                }
            }
        }
    }
}
