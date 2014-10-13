using System;

namespace LennysWpfLibrary.Collections
{
    public class CollectionItemEventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public CollectionItemEventArgs() : base() { }

        public CollectionItemEventArgs(T item)
            : base()
        {
            this.Item = item;
        }
    }
}
