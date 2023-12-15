namespace SegmentTree
{
    public class SegmentTree
    {
        private long[] tree;
        private int size;

        public SegmentTree(List<long> numbers)
        {
            size = 1;
            while (size < numbers.Count)
                size *= 2;
            tree = new long[size * 2 - 1];
            Build(numbers, 0, 0, size);
        }

        private void Build(List<long> numbers, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if (lx < numbers.Count)
                    tree[x] = numbers[lx];
                return;
            }
            int m = (lx + rx) / 2;
            Build(numbers, 2 * x + 1, lx, m);
            Build(numbers, 2 * x + 2, m, rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public void Set(int index, long value)
            => Set(index, value, 0, 0, size);

        private void Set(int index, long value, int x, int lx, int rx)
        {
            if(rx - lx == 1)
            {
                tree[x] = value;
                return;
            }

            int m = (lx + rx) / 2;

            if (index < m)
                Set(index, value, 2 * x + 1, lx, m);
            else
                Set(index, value, 2 * x + 2, m, rx);

            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public long GetSum(int l, int r)
            => GetSum(l, r, 0, 0, size);

        private long GetSum(int l, int r, int x, int lx, int rx)
        {
            if (lx >= r || rx <= l)
                return 0;
            if (l <= lx && rx <= r)
                return tree[x];

            int m = (lx + rx) / 2;
            long leftSum = GetSum(l, r, 2 * x + 1, lx, m);
            long rightSum = GetSum(l, r, 2 * x + 2, m, rx);

            return leftSum + rightSum;
        }
    }
}
