namespace UnionFind.Tests
{
    public class Tests
    {
        [Fact]
        public void BothOperations()
        {
            var cnm = new UnionFind(7);

            Assert.Equal(1, cnm.Find(1));
            Assert.Equal(5, cnm.Find(5));

            cnm.Union(0, 1);
            Assert.Equal(0, cnm.Find(1));

            cnm.Union(0, 2);
            Assert.Equal(0, cnm.Find(2));

            cnm.Union(4, 5);
            cnm.Union(5, 6);
            cnm.Union(5, 0);

            Assert.Equal(4, cnm.Find(0));
        }
    }
}
