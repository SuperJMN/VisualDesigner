using System;

namespace Glass.Basics.Core
{
    public class EventArgs<T> : EventArgs
    {
        // Property variable
        private readonly T data;

        // Constructor
        public EventArgs(T data)
        {
            this.data = data;
        }

        // Property for EventArgs argument
        public T Data
        {
            get { return data; }
        }
    }
}