using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AVLTree.Tests
{
    public class TreeTests
    {
        [Fact]
        public void Add_SingleElement_InEmptyTree()
        {
            // Arrange
            var tree = new Tree<int>();
            int element = 0;

            // Act
            tree.Add(element);

            // Assert
            Assert.Equal(1, tree?.Count);
            Assert.Equal(1, tree?.Height);
            Assert.True(tree?.Contains(element));
        }

        [Fact]
        public void Add_100000RandomElements()
        {
            // Arrange
            int count = (int)1e5;
            var tree = new Tree<int>();
            var rnd = new Random();

            // Act
            for(int i = 0; i < count; ++i)
            {
                tree.Add(rnd.Next(-100, 100));
                Assert.True(tree.Height <= tree.Count.GetUpperHeightBound());
            }

            // Assert

            Assert.Equal(tree.Count, count);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6})]
        [InlineData(new int[] { 2, 4, -13, 123 })]
        [InlineData(new int[] { 1, 1, 1, 1, 1 })]
        [InlineData(new int[] { 4, 5, 7, 2, 1, 3, 6})]
        public void Add_Collection(int[] collection)
        {
            // Arrange
            var tree = new Tree<int>(collection);

            // Assert
            Assert.All(collection, value => tree.Contains(value));
            Assert.Equal(collection.Length, tree.Count);
            Assert.True(tree.Height <= tree.Count.GetUpperHeightBound());
        }

        [Fact]
        public void Traverse_Check()
        {
            // Arrange
            var collection = new string[] { "abc", "cde", "rwtwe", "123", "" };
            var tree = new Tree<string>(collection);

            // Act
            var list = tree.ToList();
            Array.Sort(collection);

            // Assert

            for (int i = 0; i < collection.Length; ++i)
                Assert.Equal(collection[i], list[i]);
        }

        [Fact]
        public void Remove_FromEmptyTree()
        {
            // Arrange
            var tree = new Tree<int>();
            int element = 0;

            // Act
            tree.Remove(element);

            // Assert
            Assert.Equal(0, tree?.Count);
            Assert.Equal(0, tree?.Height);
            Assert.True(!tree?.Contains(element));
        }

        [Fact]
        public void Remove_ExistingElement()
        {
            // Arrange
            var tree = new Tree<int>();
            int element = 0;
            tree.Add(element);

            // Act
            tree.Remove(element);

            // Assert
            Assert.Equal(0, tree.Count);
            Assert.Equal(0, tree?.Height);
            Assert.True(!tree?.Contains(element));
        }

        [Fact]
        public void Check_MinAndMax()
        {
            // Arrange
            var collection = new int[] { 1, -123, 234542, 231, -52, 293 };
            var tree = new Tree<int>(collection);

            // Act
            var treeMin = tree.Min;
            var expectedMin = collection.Min();

            var treeMax = tree.Max;
            var expectedMax = collection.Max();

            // Assert
            Assert.Equal(expectedMin, treeMin);
            Assert.Equal(expectedMax, treeMax);
        }
    }

    public static class Extensions
    {
        public static double GetUpperHeightBound(this int count)
            => Math.Floor(1.45 * Math.Log2(count + 2));
    }
}
