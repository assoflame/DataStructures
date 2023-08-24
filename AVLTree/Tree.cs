using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    internal class TreeNode<T> : IEnumerable<T>
        where T : IComparable
    {
        public T Data { get; private set; }

        public int Height { get; set; }

        public TreeNode<T>? Left;
        public TreeNode<T>? Right;

        public TreeNode(T data)
        {
            Data = data;
            Height = 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Left is not null)
                foreach (var node in Left)
                    yield return node;

            yield return Data;

            if (Right is not null)
                foreach (var node in Right)
                    yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Tree<T> : IEnumerable<T>
        where T : IComparable
    {
        private TreeNode<T>? _root;
        public int Height => _root?.Height ?? 0;
        public int Count { get; private set; }
        public T Min => GetMin(_root).Data;
        public T Max => GetMax(_root).Data;

        public Tree() { }
        public Tree(T data) => _root = new TreeNode<T>(data);
        public Tree(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        private bool Contains(T data, TreeNode<T>? currentNode)
        {
            if (currentNode is null)
                return false;

            int comparisonResult = data.CompareTo(currentNode.Data);

            if (comparisonResult == 0)
                return true;

            if (comparisonResult < 0)
                return Contains(data, currentNode.Left);

            return Contains(data, currentNode.Right);
        }

        public bool Contains(T data) => Contains(data, _root);


        private TreeNode<T> Add(T data, TreeNode<T>? currentNode)
        {
            if (currentNode is null) return new TreeNode<T>(data);

            if (data.CompareTo(currentNode.Data) < 0)
                currentNode.Left = Add(data, currentNode.Left);
            else
                currentNode.Right = Add(data, currentNode.Right);

            return Balance(currentNode);
        }

        public void Add(T data)
        {
            _root = Add(data, _root);
            ++Count;
        }

        private TreeNode<T> Balance(TreeNode<T> node)
        {
            RefreshHeight(node);

            var difference = GetDifference(node);

            if(difference == 2)
            {
                if (GetDifference(node.Right) < 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            } else if(difference == -2)
            {
                if (GetDifference(node.Left) > 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            return node;
        }

        private TreeNode<T> RotateLeft(TreeNode<T> node)
        {
            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;
            RefreshHeight(node);
            RefreshHeight(right);

            return right;
        }
        private TreeNode<T> RotateRight(TreeNode<T> node)
        {
            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;
            RefreshHeight(node);
            RefreshHeight(left);

            return left;
        }

        private TreeNode<T>? Remove(T data, TreeNode<T>? currentNode)
        {
            if (currentNode is null)
                return null;

            var comparisonResult = data.CompareTo(currentNode.Data);

            if (comparisonResult < 0)
                currentNode.Left = Remove(data, currentNode.Left);
            else if (comparisonResult > 0)
                currentNode.Right = Remove(data, currentNode.Right);
            else
            {
                --Count;
                var left = currentNode.Left;
                var right = currentNode.Right;

                if (right is null)
                    return left;

                var min = GetMin(right);
                min.Right = RemoveMin(right);
                min.Left = left;

                return Balance(min);
            }

            return Balance(currentNode);
        }

        public void Remove(T data) => _root = Remove(data, _root);

        private TreeNode<T>? RemoveMin(TreeNode<T> currentNode)
        {
            if (currentNode.Left is null)
                return currentNode.Right;

            currentNode.Left = RemoveMin(currentNode.Left);

            return Balance(currentNode);
        }

        private void RefreshHeight(TreeNode<T> node)
        {
            var leftHeight = node?.Left?.Height ?? 0;
            var rightHeight = node?.Right?.Height ?? 0;
            node.Height = Math.Max(leftHeight, rightHeight) + 1;
        }

        private int GetHeight(TreeNode<T>? node) => node?.Height ?? 0;

        private int GetDifference(TreeNode<T>? node) => GetHeight(node?.Right) - GetHeight(node?.Left);

        private TreeNode<T> GetMin(TreeNode<T>? root)
        {
            if (root is null)
                throw new ArgumentException("Tree is empty");

            var ptr = root;

            while (ptr.Left is not null)
                ptr = ptr.Left;

            return ptr;
        }

        private TreeNode<T> GetMax(TreeNode<T>? root)
        {
            if (root is null)
                throw new ArgumentException("Tree is empty");

            var ptr = root;

            while (ptr.Right is not null)
                ptr = ptr.Right;

            return ptr;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_root is not null)
                foreach (var node in _root)
                    yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
