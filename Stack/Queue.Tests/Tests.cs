using Xunit;

namespace Stack.Tests
{
    public class Tests
    {
        [Fact]
        public void Push_InEmptyStack()
        {
            // Arrange
            var stack = new MyStack<int>();
            int value = 0;

            // Act
            stack.Push(value);

            // Assert
            Assert.Equal(1, stack.Count);
            Assert.Equal(value, stack.Head);
        }

        [Fact]
        public void Push_InNotEmptyStack()
        {

            // Arrange
            var stack = new MyStack<int>();

            // Act
            stack.Push(0);
            stack.Push(1);

            // Assert
            Assert.Equal(2, stack.Count);
            Assert.Equal(1, stack.Head);
        }
    }
}
