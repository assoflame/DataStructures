namespace UnionFind
{
    public class UnionFind
    {
        private int[] ranks;
        private int[] parents;

        public UnionFind(int size)
        {
            ranks = new int[size];
            parents = new int[size];
            for(int i = 0; i < size; ++i)
                parents[i] = i;
        }

        public int Find(int elem)
        {
            return parents[elem] = parents[elem] == elem
                    ? elem
                    : Find(parents[elem]);
        }

        public void Union(int a, int b)
        {
            var aRoot = Find(a);
            var bRoot = Find(b);

            if (ranks[aRoot] == ranks[bRoot])
                ++ranks[aRoot];
            if (ranks[aRoot] > ranks[bRoot])
                parents[bRoot] = parents[aRoot];
            else
                parents[aRoot] = parents[bRoot];
        }
    }
}
