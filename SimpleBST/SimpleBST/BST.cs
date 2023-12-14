using System.Collections;

namespace Tree
{
    internal class Node<T> : IEnumerable<T>
        where T : IComparable
    {
        public T Data { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }

        public Node(T data)
        {
            Data = data;
            Left = Right = null;
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

    public class BST<T> : IEnumerable<T>
        where T : IComparable
    {
        private Node<T>? _root;

        public int Count { get; private set; }
        public T Min => GetMin(_root).Data;
        public T Max => GetMax(_root).Data;

        public BST() { }

        public BST(T value)
        {
            Add(value);
        }

        public BST(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        public void Add(T key)
        {
            var newNode = new Node<T>(key);
            ++Count;
            if (_root is null)
            {
                _root = newNode;
                return;
            }

            var parent = _root;
            var child = key.CompareTo(parent.Data) < 0 ? parent.Left : parent.Right;
            while (child is not null)
            {
                parent = child;
                child = key.CompareTo(parent.Data) < 0 ? parent.Left : parent.Right;
            }

            if (key.CompareTo(parent.Data) < 0)
                parent.Left = newNode;
            else
                parent.Right = newNode;
        }


        public void Remove(T key)
        {
            if (_root is null)
                return;

            var currentNode = _root;

            if (key.CompareTo(currentNode.Data) == 0)
            {
                --Count;
                if (_root.Right is null)
                {
                    _root = _root.Left;
                    return;
                }
                ChangeOnRightTreeMin(currentNode);
                return;
            }

            var pairForRemove = GetNodeForRemove(_root, key);

            if (pairForRemove.nodeToRemove is null) return;

            --Count;
            if (pairForRemove.nodeToRemove.Right is null)
            {
                if (pairForRemove.parent.Right == pairForRemove.nodeToRemove)
                    pairForRemove.parent.Right = pairForRemove.nodeToRemove.Left;
                else
                    pairForRemove.parent.Left = pairForRemove.nodeToRemove.Left;
                return;
            }

            ChangeOnRightTreeMin(pairForRemove.nodeToRemove);
        }

        private (Node<T>? parent, Node<T>? nodeToRemove) GetNodeForRemove(Node<T> root, T key)
        {
            var parent = root;
            var child = key.CompareTo(parent.Data) < 0 ? parent.Left : parent.Right;

            while (child is not null && key.CompareTo(child.Data) != 0)
            {
                parent = child;
                child = key.CompareTo(parent.Data) < 0 ? parent.Left : parent.Right;
            }

            if (child is null)
                return (null, null);

            return (parent, child);
        }

        private void ChangeOnRightTreeMin(Node<T> node)
        {
            var ptr = node.Right;
            if (ptr.Left is null)
            {
                node.Data = ptr.Data;
                node.Right = ptr.Right;
                return;
            }

            while (ptr.Left.Left is not null)
                ptr = ptr.Left;

            var minParent = ptr;
            var min = minParent.Left;
            node.Data = min.Data;

            if (min.Right is null)
                minParent.Left = null;
            else
                minParent.Left = min.Right;
        }

        public bool Contains(T key)
        {
            if (_root is null)
                return false;

            var currentNode = _root;

            while (currentNode is not null && key.CompareTo(currentNode.Data) != 0)
            {
                currentNode = key.CompareTo(currentNode.Data) < 0
                    ? currentNode.Left
                    : currentNode.Right;
            }

            return currentNode != null;
        }

        private Node<T> GetMin(Node<T>? root)
        {
            if (root is null)
                throw new InvalidOperationException("tree is empty");

            var currentNode = root;
            while (currentNode.Left is not null)
                currentNode = currentNode.Left;

            return currentNode;
        }

        private Node<T> GetMax(Node<T>? root)
        {
            if (root is null)
                throw new InvalidOperationException("tree is empty");

            var currentNode = root;
            while (currentNode.Right is not null)
                currentNode = currentNode.Right;

            return currentNode;
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
