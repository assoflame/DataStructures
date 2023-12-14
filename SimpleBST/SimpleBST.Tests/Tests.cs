using Tree;

namespace BST.Tests
{
    public class BST_Tests
    {
        [Fact]
        public void Add_OneElem_InEmptyTree()
        {
            // Arrange
            var tree = new BST<int>();
            int value = 0;

            // Act
            tree.Add(value);

            // Assert
            Assert.Equal(1, tree.Count);
            Assert.True(tree.Contains(value));
        }

        [Fact]
        public void Add_SeveralElements_InEmptyTree()
        {
            // Arrange
            var tree = new BST<int>();
            var values = new int[] { 1, 2, 3 };

            // Act
            foreach (var value in values)
                tree.Add(value);

            // Arrange
            Assert.Equal(values.Length, tree.Count);
            Assert.True(values.All(value => tree.Contains(value)));
        }

        [Fact]
        public void GetMinAndMax_InEmptyTree_ThrowsInvalidOperationException()
        {
            var tree = new BST<int>();

            Assert.Throws<InvalidOperationException>(() => tree.Min());
            Assert.Throws<InvalidOperationException>(() => tree.Max());
        }

        [Fact]
        public void Remove_SingleElement()
        {
            var tree = new BST<int>();
            var value = 0;

            tree.Add(value);

            tree.Remove(value);

            Assert.True(!tree.Contains(value));
            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void RemoveRoot_RootWithChilds()
        {
            var tree = new BST<int>();
            var root = 0;
            var leftChild = -1;
            var rightChild = 1;

            tree.Add(root);
            tree.Add(leftChild);
            tree.Add(rightChild);

            tree.Remove(root);

            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void RemoveRoot_RootWithOnlyLeftChild()
        {
            var tree = new BST<int>();
            var root = 0;
            var leftChild = -1;

            tree.Add(root);
            tree.Add(leftChild);

            tree.Remove(root);

            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void RemoveLeaves()
        {
            var values = new int[] { 0, -5, -10, -3, 5, 10, 3 };
            var tree = new BST<int>(values);

            tree.Remove(-3);
            tree.Remove(-10);
            tree.Remove(3);
            tree.Remove(10);

            Assert.Equal(values.Length - 4, tree.Count);
        }
    }
}
