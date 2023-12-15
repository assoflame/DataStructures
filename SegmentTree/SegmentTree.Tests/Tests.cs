namespace SegmentTree.Tests
{
    public class Tests
    {
        [Fact]
        public void GetSum()
        {
            var numbers = new List<long> { 1, 9, -3, 4, 5, 6, -5 };
            var tree = new SegmentTree(numbers);

            Assert.Equal(numbers.GetRange(0, 1).Sum(), tree.GetSum(0, 1));
            Assert.Equal(numbers.GetRange(0, 2).Sum(), tree.GetSum(0, 2));
            Assert.Equal(numbers.GetRange(0, 3).Sum(), tree.GetSum(0, 3));
            Assert.Equal(numbers.GetRange(2, 5).Sum(), tree.GetSum(2, 7));
            Assert.Equal(numbers.GetRange(0, numbers.Count).Sum(), tree.GetSum(0, numbers.Count));
            Assert.Equal(numbers.GetRange(3, 2).Sum(), tree.GetSum(3, 5));
        }

        [Fact]
        public void ChangeElems()
        {
            var numbers = new List<long> { 1, 9, -3, 4, 5, 6, -5 };
            var tree = new SegmentTree(numbers);

            tree.Set(0, -5);
            Assert.Equal(-5, tree.GetSum(0, 1));

            tree.Set(numbers.Count / 2, 0);
            Assert.Equal(0, tree.GetSum(numbers.Count / 2, numbers.Count / 2 + 1));

            tree.Set(numbers.Count - 1, 0);
            Assert.Equal(0, tree.GetSum(numbers.Count - 1, numbers.Count));
        }
    }
}
