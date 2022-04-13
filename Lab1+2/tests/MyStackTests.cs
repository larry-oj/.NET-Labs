namespace MyStackLibrary.Test;

[TestFixture]
public class MyStackTests
{
    [Test]
    public void Init_DifferentTypes_NoException()
    {
        var stack = new MyStack<int>();
        var stack2 = new MyStack<string>();
        var stack3 = new MyStack<bool>();
        var stack4 = new MyStack<object>();
        var stack5 = new MyStack<MyStack<object>>();

        Assert.Pass();
    }

    #region Count tests
        [Test]
        public void Count_EmptyStack_Zero()
        {
            var stack = new MyStack<int>();

            Assert.AreEqual(0, stack.Count);
        }

        [Test]
        public void Count_OneItem_One()
        {
            var stack = new MyStack<int>();
            stack.Push(1);

            Assert.AreEqual(1, stack.Count);
        }

        [Test]
        public void Count_PushThreePopOne_Two()
        {
            var stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Pop();

            Assert.AreEqual(2, stack.Count);
        }
    #endregion

    #region Push tests
        [Test]
        public void Push_MuslitpleItmes_NoException()
        {
            var stack = new MyStack<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.Pass();
        }

        [Test]
        public void Push_WrongType_Exception()
        {
            Assert.Throws<Microsoft.CSharp.RuntimeBinder.RuntimeBinderException>(() =>
            {
                var stack = new MyStack<int>();
                dynamic data = "string";
                stack.Push(data);
            });
        }

        [Test]
        public void Push_MultipleItems_RightOrder()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            var current = stack.Count - 1;

            foreach (int num in stack)
            {
                Assert.AreEqual(num, current--);
            }
        }

        [Test]
        public void Push_NullItem_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var stack = new MyStack<object>();
                stack.Push(null!);
            });
        }

        [Test]
        public void Push_Event_SuccessfulCall()
        {
            var stack = new MyStack<int>();
            bool called = false;
            stack.OnPush += () => { called = true; };
            stack.Push(1);
            Assert.IsTrue(called);
        }
    #endregion

    #region Pop tests
        [Test]
        public void Pop_EmptyStack_Exception()
        {
            var stack = new MyStack<int>();
            Assert.Throws<InvalidOperationException>(() => {
                stack.Pop();
            });
        }

        [Test]
        public void Pop_MultipleItems_RightOrder()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(i, stack.Pop());
            }
        }

        [Test]
        public void Pop_Event_SuccessfulCall()
        {
            var stack = new MyStack<int>();
            bool called = false;
            stack.OnPop += () => { called = true; };

            stack.Push(1);
            stack.Pop();

            Assert.IsTrue(called);
        }
    #endregion

    #region Peek test
        [Test]
        public void Peek_EmptyStack_Exception()
        {
            var stack = new MyStack<int>();
            Assert.Throws<InvalidOperationException>(() => {
                stack.Peek();
            });
        }

        [Test]
        public void Peek_MultipleTimes_NoChange()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(9, stack.Peek());
            }
        }

        [Test]
        public void Peek_PopOnce_Change()
        {
            var stack = new MyStack<int>();

            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Peek());

            stack.Pop();
            Assert.AreEqual(1, stack.Peek());
        }
    #endregion

    #region Contains test
        [Test]
        public void Contains_EmptyStack_False()
        {
            var stack = new MyStack<int>();
            Assert.IsFalse(stack.Contains(1));
        }

        [Test]
        public void Contains_MultipleItems_True()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.IsTrue(stack.Contains(9));
            Assert.IsTrue(stack.Contains(3));
        }

        [Test]
        public void Contains_MultipleItems_False()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.IsFalse(stack.Contains(11));
        }
    #endregion

    #region Clear tests
        [Test]
        public void Clear_EmptyStack_NoException()
        {
            var stack = new MyStack<int>();
            stack.Clear();
            Assert.Pass();
        }

        [Test]
        public void Clear_MultipleItems_NoException()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }
            
            stack.Clear();
            Assert.Pass();
        }

        [Test]
        public void Clear_Event_SuccessfulCall()
        {
            var stack = new MyStack<int>();
            bool called = false;
            stack.OnClear += () => { called = true; };
            stack.Clear();
            Assert.IsTrue(called);
        }
    #endregion
}