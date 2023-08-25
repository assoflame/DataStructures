using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Queue.Tests
{
    public class QueueTests
    {
        [Fact]
        public void Enqueue_OneElement_InEmptyQueue()
        {
            // Arrange
            var que = new MyQueue<int>();
            int value = 0;

            // Act
            que.Enqueue(value);

            // Assert
            Assert.Equal(1, que.Count);
            Assert.Equal(value, que.Peek);
        }

        [Fact]
        public void Enqueue_SeveralElems_InEmptyQueue()
        {
            // Arrange
            var que = new MyQueue<int>();
            var values = new int[] { 1, 2, 3 };

            // Act
            foreach (var value in values)
                que.Enqueue(value);

            // Assert
            Assert.Equal(values.Length, que.Count);
            for(int i = 0; i < values.Length; ++i)
            {
                Assert.Equal(values[i], que.Peek);
                que.Dequeue();
            }
        }

        [Fact]
        public void Dequeue_FromEmptyQueue()
        {
            // Arrange
            var que = new MyQueue<int>();
            
            // Assert
            Assert.Throws<InvalidOperationException>(() => que.Dequeue());
        }

        [Fact]
        public void GetPeek_InEmptyQueue()
        {
            // Arrange
            var que = new MyQueue<int>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => que.Peek);
        }

        [Fact]
        public void Deque_AllElems()
        {
            // Arrange
            var que = new MyQueue<int>(new int[] { 1, 2, 3 });

            // Act
            while (!que.IsEmpty)
                que.Dequeue();

            // Assert
            Assert.True(que.IsEmpty);
            Assert.Equal(0, que.Count);
        }
    }
}
