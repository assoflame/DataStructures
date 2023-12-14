namespace Stack
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

    public class MyStack<T>
    {
        private Node<T>? _head;

        public int Count { get; private set; }

        public T Head => _head is not null
            ? _head.Data
            : throw new InvalidOperationException("Stack is empty");

        public MyStack() { }

        public void Push(T value)
        {
            var newNode = new Node<T>(value);

            if (_head is null)
            {
                _head = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head = newNode;
            }

            ++Count;
        }

        public T Pop()
        {
            if (_head is null)
                throw new InvalidOperationException("Stack is empty");

            var node = _head;
            _head = _head.Next;
            --Count;

            return node.Data;
        }

        public bool IsEmpty() => _head is null;
    }

}
