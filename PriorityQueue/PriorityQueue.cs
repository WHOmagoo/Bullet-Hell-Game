using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueue<T>
        {
            private LinkedList<ValueTuple<long, T>> queue;

            public PriorityQueue()
            {
                queue = new LinkedList<ValueTuple<long, T>>();
            }

            public void Add(long time, T item)
            {

                //Empty queue, we just Add it
                if (ReferenceEquals(queue.First, null))
                {
                    var newItem = new LinkedListNode<ValueTuple<long, T>>(
                        new ValueTuple<long, T>(time, item));
                    queue.AddFirst(newItem);
                    return;
                }

                LinkedListNode<ValueTuple<long, T>> curNode = queue.First;

                //Find proper spot to insert our new node and proper amount of time to insert after previous node
                while (!ReferenceEquals(curNode, null) && time - curNode.Value.Item1 >= 0)
                {
                    time -= curNode.Value.Item1;
                    curNode = curNode.Next;
                }

                //Create the node
                var newNode = new LinkedListNode<ValueTuple<long, T>>(new ValueTuple<long, T>(time, item));


                //If current node or next node is null do this
                if (ReferenceEquals(curNode, null))
                {
                    queue.AddLast(newNode);
                }
                else
                {
                    //Otherwise, insert the new node after the current node and update curNode.next to
                    //reduce the time it needs to wait after the previous item.

                    //Update time of the node after the location of our new node.
                    var newVal = curNode.Value;
                    newVal.Item1 -= time;
                    curNode.Value = newVal;

                    //Add our new node
                    queue.AddBefore(curNode, newNode);
                }
            }

            public LinkedList<T> Pop(long timePassed)
            {
                var result = new LinkedList<T>();

                foreach (var item in queue)
                {
                    if (item.Item1 <= timePassed)
                    {
                        result.AddLast(item.Item2);
                        timePassed -= item.Item1;
                    } else {
                        var modifying = queue.Find(item);

                        if (modifying != null)
                        {
                            var newValue = modifying.Value;
                            newValue.Item1 -= timePassed;
                            modifying.Value = newValue;
                        }
                        break;
                    }
                }

                for (int i = 0; i < result.Count; i++)
                {
                    queue.RemoveFirst();
                }

                return result;
            }

        // puts the next queue item to be ready
        public void FastForward(){
                var modifying = queue.Find(queue.First.Value);
                if(!ReferenceEquals(queue.First, null)){
                    var newVal = modifying.Value;
                    newVal.Item1 = 1;
                    modifying.Value = newVal;
                }
            }
        }
}