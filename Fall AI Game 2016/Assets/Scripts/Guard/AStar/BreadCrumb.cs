using UnityEngine;
using System;

public class BreadCrumb : IComparable<BreadCrumb>
{	
	public Point position;
	public BreadCrumb prev;
	public BreadCrumb next;
	public int cost = Int32.MaxValue;
	public bool onClosedList = false;
	public bool onOpenList = false;

	/// <summary>
	/// Initializes a new instance of the <see cref="BreadCrumb"/> class.
	/// </summary>
	/// <param name="position">Position.</param>
    public BreadCrumb(Point position)
    {
        this.position = position;
    }

	/// <summary>
	/// Overrides default Equals so we check on position instead of object memory location.
	/// </summary>
	/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="BreadCrumb"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="BreadCrumb"/>;
	/// otherwise, <c>false</c>.</returns>
	public override bool Equals(object obj)
	{
		return (obj is BreadCrumb) && ((BreadCrumb)obj).position.X == this.position.X && ((BreadCrumb)obj).position.Y == this.position.Y;
	}

	/// <summary>
	/// Faster Equals for if we know something is a BreadCrumb.
	/// </summary>
	/// <param name="breadcrumb">The <see cref="BreadCrumb"/> to compare with the current <see cref="BreadCrumb"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="BreadCrumb"/> is equal to the current <see cref="BreadCrumb"/>; otherwise, <c>false</c>.</returns>
	public bool Equals(BreadCrumb breadcrumb)
	{
	    return breadcrumb.position.X == this.position.X && breadcrumb.position.Y == this.position.Y;
	}

	/// <summary>
	/// Serves as a hash function for a <see cref="BreadCrumb"/> object.
	/// </summary>
	/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
	public override int GetHashCode()
	{
	    return position.GetHashCode();
	}

	/// <summary>
	/// Compares to.
	/// </summary>
	/// <returns>The to.</returns>
	/// <param name="other">Other.</param>
	#region IComparable<> interface
	public int CompareTo(BreadCrumb other)
	{
	    return cost.CompareTo(other.cost);
	}
	#endregion
}

