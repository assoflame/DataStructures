using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    internal class Node<T>
    {
        public T Data { get; private set; }
        public Node<T>? Next { get; set; }

        public Node(T value)
        {
            Data = value;
            Next = null;
        }
    }

    public class MyQueue<T>
    {
        private Node<T>? _first;
        private Node<T>? _last;

        public int Count { get; private set; }

        public MyQueue() { }

        public MyQueue(T value) => Enqueue(value);

        public MyQueue(IEnumerable<T> values)
        {
            foreach (var value in values)
                Enqueue(value);
        }

        public bool IsEmpty => _first is null;

        public void Enqueue(T value)
        {
            var newNode = new Node<T>(value);
            if(_last is null)
            {
                _first = _last = newNode;
            } else
            {
                _last.Next = newNode;
                _last = _last.Next;
            }

            ++Count;
        }

        public T Dequeue()
        {
            if (_first is null)
                throw new InvalidOperationException("Queue is empty");

            var node = _first;
            if(_first == _last)
            {
                _first = _last = null;
            } else
            {
                _first = _first.Next;
            }
            --Count;

            return node.Data;
        }

        public T Peek => _first is not null
            ? _first.Data
            : throw new InvalidOperationException("Queue is empty");
    }
}
