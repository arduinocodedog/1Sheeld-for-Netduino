using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class CircularBuffer : Queue
    {
        int max = 0;

        public CircularBuffer(int maxsize)
            : base() 
        {
            max = maxsize; 
        }

        public void push(Object obj)
        {
            if (base.Count == max)
                return;
            base.Enqueue(obj);
        }

        public Object pop()
        {
            if (base.Count == 0)
                return null; // If the programmer is doing his job, we should NEVER get here!!!
            return base.Dequeue();
        }

        public int remain
        {
            get { return max - base.Count; }
            set { }
        }
    };
}
