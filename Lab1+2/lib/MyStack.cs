namespace MyStackLibrary;

// stack implementation using MyLinkedList
public class MyStack<T> : IEnumerable<T>, ICollection
{
    private MyLinkedList<T> _list;

    public MyStack()
    {
        this._list = new MyLinkedList<T>();
    }

    #region implement ICollection
        public int Count => this._list.Count;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public void CopyTo(Array array, int index)
        {
            this._list.CopyTo(array, index);
        }
    #endregion

    #region implement IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    #endregion

    // events
    public delegate void EventHandler();
    public event EventHandler OnPush = delegate { };
    public event EventHandler OnPop = delegate { };
    public event EventHandler OnClear = delegate { };

    // methods
    public T Pop()
    {
        if (this._list.Count == 0) throw new InvalidOperationException("Stack is empty");

        T item = this._list.First();
        this._list.RemoveFirst();
        this.OnPop.Invoke();

        return item;
    }
    public void Push(T item)
    {
        this._list.AddFirst(item);
        this.OnPush.Invoke();
    }
    public T Peek()
    {
        if (this._list.Count == 0) throw new InvalidOperationException("Stack is empty");

        return this._list.First();
    }
    public T[] ToArray()
    {
        T[] array = new T[this._list.Count];
        this._list.CopyTo(array, 0);

        return array;
    }
    // TrimExcess() can not be implemented using MyLinkedList
    public bool Contains(T item)
        => this._list.Contains(item);
    public void Clear()
    {
        this._list.Clear();
        this.OnClear.Invoke();
    }
}
