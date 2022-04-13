namespace MyStackLibrary;

public class MyLinkedList<T> : ICollection<T>, ICollection
{
    // node class
    private class Node
    {
        public T data;
        public Node? next;
        public Node(T data)
        {
            this.data = data;
            this.next = null;
        }
    }

    // fields
    private Node? _head;
    private Node? _tail;
    private int _count;

    // constructor
    public MyLinkedList()
    {
        this._head = null;
        this._tail = null;
        this._count = 0;
    }

    #region implement ICollection<T>
        public int Count => this._count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (this._head == null)
            {
                this._head = newNode;
                this._tail = newNode;
            }
            else
            {
                if (this._tail != null) 
                {
                    this._tail.next = newNode;
                    this._tail = newNode;
                }
            }
            this._count++;
        }
        public void Clear()
        {
            this._head = null;
            this._tail = null;
            this._count = 0;
        }
        public bool Contains(T item)
        {
            if (item == null) throw new ArgumentException("Passed object cannot be null");
            
            if (this._head == null) return false;

            Node current = this._head;
            while (current != null)
            {
                if (current.data != null && current.data.Equals(item))
                {
                    return true;
                }
                current = current.next!;
            }
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (this._head == null) throw new InvalidOperationException("Collection is empty");
            
            Node current = this._head;
            while (current != null)
            {
                array[arrayIndex++] = current.data;
                current = current.next!;
            }
        }
        public bool Remove(T item)
        {
            if (item == null) throw new ArgumentException("Passed object cannot be null");

            if (this._head == null) throw new InvalidOperationException("Collection is empty"); 

            Node current = this._head;
            Node? previous = null;
            while (current != null)
            {
                if (current.data != null && current.data.Equals(item))
                {
                    if (previous == null)
                    {
                        this._head = current.next;
                    }
                    else
                    {
                        previous.next = current.next;
                    }
                    this._count--;
                    return true;
                }
                previous = current;
                current = current.next!;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? current = this._head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    #endregion

    #region implement ICollection
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException("Passed array cannot be null");

            if (this._head == null) throw new InvalidOperationException("Collection is empty");

            Node? current = this._head;
            while (current != null)
            {
                array.SetValue(current.data, index++);
                current = current.next;
            }
        }
    #endregion

    // methods required for mystack
    public void AddFirst(T item)
    {
        if (item == null) throw new ArgumentNullException("Passed object cannot be null");
        
        Node newNode = new Node(item);
        if (this._head == null)
        {
            this._head = newNode;
            this._tail = newNode;
        }
        else
        {
            newNode.next = this._head;
            this._head = newNode;
        }
        this._count++;
    }
    public void RemoveFirst()
    {
        if (this._head == null) throw new InvalidOperationException("Collection is empty");

        this._head = this._head.next;
        this._count--;
    }
}
