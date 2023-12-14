namespace LinkedList.Tests
{
    public class Tests
    {
        [Fact]
        public void PushBegin_FirstElement()
        {
            // Arrange
            var list = new LinkedList<int>();
            int value = 0;

            // Act
            list.PushBegin(value);

            // Assert
            Assert.Equal(value, list.Begin);
            Assert.Equal(value, list.End);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void PushEnd_FirstElement()
        {
            // Arrange
            var list = new LinkedList<int>();
            int value = 0;

            // Act
            list.PushEnd(value);

            // Assert
            Assert.Equal(value, list.Begin);
            Assert.Equal(value, list.End);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void PushBegin_SeveralElements()
        {
            // Arrange
            var collection = new int[] { 1, 2, 3, 4, 5 };
            var list = new LinkedList<int>();

            // Act
            foreach (var value in collection)    // list: 5 <-> 4 <-> 3 <-> 2 <-> 1
                list.PushBegin(value);

            // Assert
            Assert.All(collection, value => list.Contains(value));
            Assert.Equal(collection.Length, list.Count);
            Assert.Equal(collection.First(), list.End);
            Assert.Equal(collection.Last(), list.Begin);
        }

        [Fact]
        public void PushEnd_SeveralElements()
        {
            // Arrange
            var collection = new int[] { 1, 2, 3, 4, 5 };
            var list = new LinkedList<int>();

            // Act
            foreach (var value in collection)    // list: 1 <-> 2 <-> 3 <-> 4 <-> 5
                list.PushEnd(value);

            // Assert
            Assert.All(collection, value => list.Contains(value));
            Assert.Equal(collection.Length, list.Count);
            Assert.Equal(collection.First(), list.Begin);
            Assert.Equal(collection.Last(), list.End);
        }

        [Fact]
        public void Pop_FromEmptyList()
        {
            // Arrange
            var list = new LinkedList<int>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => list.PopEnd());
            Assert.Throws<InvalidOperationException>(() => list.PopBegin());
        }

        [Fact]
        public void Contains_InEmptyList()
        {

            // Arrange
            var list = new LinkedList<int>();

            // Assert
            Assert.False(list.Contains(0));
        }
    }
}
