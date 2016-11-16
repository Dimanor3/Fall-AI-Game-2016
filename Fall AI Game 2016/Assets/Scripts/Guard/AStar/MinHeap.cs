using System;


public class MinHeap<T> where T : IComparable<T>
{
	private int count;
	private int capacity;
	private T temp;
	private T mheap;
	private T[] array;
	private T[] tempArray;

	/// <summary>
	/// Gets the count.
	/// </summary>
	/// <value>The count.</value>
	public int Count
	{
	    get { return this.count; }
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MinHeap`1"/> class.
	/// </summary>
	public MinHeap() : this(16) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="MinHeap`1"/> class.
	/// </summary>
	/// <param name="capacity">Capacity.</param>
	public MinHeap(int capacity)
	{
	    this.count = 0;
	    this.capacity = capacity;
	    array = new T[capacity];
	}

	/// <summary>
	/// Builds the head.
	/// </summary>
	public void BuildHead()
	{
	    int position;
	    for (position = (this.count - 1) >> 1; position >= 0; position--)
	    {
	        this.MinHeapify(position);
	    }
	}

	/// <summary>
	/// Add the specified item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void Add(T item)
	{
	    this.count++;
	    if (this.count > this.capacity)
	    {
	        DoubleArray();
	    }
	    this.array[this.count - 1] = item;
	    int position = this.count - 1;
	
	    int parentPosition = ((position - 1) >> 1);
	
	    while (position > 0 && array[parentPosition].CompareTo(array[position]) > 0)
	    {
	        temp = this.array[position];
	        this.array[position] = this.array[parentPosition];
	        this.array[parentPosition] = temp;
	        position = parentPosition;
	        parentPosition = ((position - 1) >> 1);
	    }
	}

	/// <summary>
	/// Doubles the array.
	/// </summary>
	private void DoubleArray()
	{
	    this.capacity <<= 1;
	    tempArray = new T[this.capacity];
	    CopyArray(this.array, tempArray);
	    this.array = tempArray;
	}

	/// <summary>
	/// Copies the array.
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="destination">Destination.</param>
	private static void CopyArray(T[] source, T[] destination)
	{
	    int index;
	    for (index = 0; index < source.Length; index++)
	    {
	        destination[index] = source[index];
	    }
	}

	/// <summary>
	/// Peek this instance.
	/// </summary>
	public T Peek()
	{
	    if (this.count == 0)
	    {
	        throw new InvalidOperationException("Heap is empty");
	    }
	    return this.array[0];
	}
	
	/// <summary>
	/// Extracts the first.
	/// </summary>
	/// <returns>The first.</returns>
	public T ExtractFirst()
	{
	    if (this.count == 0)
	    {
	        throw new InvalidOperationException("Heap is empty");
	    }
	    temp = this.array[0];
	    this.array[0] = this.array[this.count - 1];
	    this.count--;
	    this.MinHeapify(0);
	    return temp;
	}

	/// <summary>
	/// Minimums the heapify.
	/// </summary>
	/// <param name="position">Position.</param>
	private void MinHeapify(int position)
	{
	    do
	    {
	        int left = ((position << 1) + 1);
	        int right = left + 1;
	        int minPosition;
	
	        if (left < count && array[left].CompareTo(array[position]) < 0)
	        {
	            minPosition = left;
	        }
	        else
	        {
	            minPosition = position;
	        }
	
	        if (right < count && array[right].CompareTo(array[minPosition]) < 0)
	        {
	            minPosition = right;
	        }
	
	        if (minPosition != position)
	        {
	            mheap = this.array[position];
	            this.array[position] = this.array[minPosition];
	            this.array[minPosition] = mheap;
	            position = minPosition;
	        }
	        else
	        {
	            return;
	        }
	
	    } while (true);
	}
}
