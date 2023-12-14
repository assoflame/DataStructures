using System.Collections;

namespace LinkedList
{
    internal class Node<T>
    {
        public T Data { get; private set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Prev { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T>? _begin;
        private Node<T>? _end;

        public int Count { get; private set; }

        public T Begin => _begin is not null
            ? _begin.Data
            : throw new InvalidOperationException("List is empty");

        public T End => _end is not null
            ? _end.Data
            : throw new InvalidOperationException("List is empty");

        public LinkedList() { }

        public LinkedList(T value)
        {
            var newNode = new Node<T>(value);
            _begin = newNode;
            _end = newNode;
        }

        public LinkedList(IEnumerable<T> values)
        {
            foreach (var value in values)
                PushEnd(value);
        }

        public void PushEnd(T value)
        {
            var newNode = new Node<T>(value);
            if (_begin is null)
            {
                _begin = newNode;
                _end = newNode;
            }
            else
            {
                _end.Next = newNode;
                newNode.Prev = _end;
                _end = newNode;
            }

            ++Count;
        }

        public void PushBegin(T value)
        {
            var newNode = new Node<T>(value);
            if (_begin is null)
            {
                _begin = newNode;
                _end = newNode;
            }
            else
            {
                _begin.Prev = newNode;
                newNode.Next = _begin;
                _begin = newNode;
            }

            ++Count;
        }

        public void PopEnd()
        {
            if (_begin is null)
                throw new InvalidOperationException("List is empty");

            if (_begin == _end)
                _begin = _end = null;

            _end = _end.Prev;
            _end.Next = null;

            --Count;
        }

        public void PopBegin()
        {
            if (_begin is null)
                throw new InvalidOperationException("List is empty");

            if (_begin == _end)
                _begin = _end = null;

            _begin = _begin.Next;
            _begin.Prev = null;

            --Count;
        }

        public bool Contains(T value)
        {
            foreach (var e in this)
                if (value.Equals(e))
                    return true;

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _begin;

            while (currentNode is not null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
