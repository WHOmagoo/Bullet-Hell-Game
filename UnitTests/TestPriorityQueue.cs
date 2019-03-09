using System.Collections.Generic;
using NUnit.Framework;
using PriorityQueue;

namespace UnitTests
{
    
    [TestFixture]
    public class TestPriorityQueue
    {
        [Test]
        public void testQueueInsertionInOrder()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            for (int i = 1; i <= 10; i++)
            {
                queue.Add(i, i);
            }

            for (int i = 0; i < 10; i++)
            {
                var result = new LinkedList<int>();
                result.AddFirst(i + 1);
                Assert.AreEqual(result, queue.Pop(1));
            }
        }
        
        [Test]
        public void testQueueInsertionDuplicateTimings()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            for (int i = 1; i <= 10; i++)
            {
                queue.Add(i, i);
                queue.Add(i, i * 10);
            }

            for (int i = 0; i < 10; i++)
            {
                var result = new LinkedList<int>();
                result.AddFirst(i + 1);
                result.AddLast((i + 1) * 10);
                Assert.AreEqual(result, queue.Pop(1));
            }
        }

        [Test]
        public void testQueueOutOfOrderInsertion()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            
            queue.Add(4,4);
            queue.Add(5,5);
            queue.Add(2,2);
            queue.Add(1,1);
            queue.Add(3,3);

            for (int i = 1; i <= 5; i++)
            {
                var result = new LinkedList<int>();
                result.AddFirst(i);
                
                Assert.AreEqual(result, queue.Pop(1));
            }
        }

    }
}