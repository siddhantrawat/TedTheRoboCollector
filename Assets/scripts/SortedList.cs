using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sorted list
/// </summary>
public class SortedList<T> where T:IComparable
{
    List<T> items = new List<T>();

    // used in Add method
    List<T> tempList = new List<T>();
	
    #region Constructors

    /// <summary>
    /// No argument constructor
    /// </summary>
    public SortedList()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of items in the list
    /// </summary>
    /// <value>number of items in the list</value>
    public int Count
    {
        get { return items.Count; }
    }
	
    /// <summary>
    /// Gets the item in the array at the given index
    /// This property allows access using [ and ]
    /// </summary>
    /// <param name="index">index of item</param>
    /// <returns>item at the given index</returns>
    public T this[int index]
    {
        get { return items[index]; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds the given item to the list
    /// </summary>
    /// <param name="item">item</param>
    public void Add(T item)
    {
        
        // add your implementation below
        int locationToAdd = 0;

        tempList.Add(item);
       
        for ( locationToAdd = 0; locationToAdd < items.Count && item.CompareTo(items[locationToAdd]) < 0; locationToAdd++) ;


        if (locationToAdd == Count)
        {
            items.Add(item);
        }
        else
        {
            items.InsertRange(locationToAdd, tempList);
        }
        tempList.Remove(item);

    }

    /// <summary>
    /// Removes the item at the given index from the list
    /// </summary>
    /// <param name="index">index</param>
    public void RemoveAt(int index)
    {
        items.RemoveAt(index);
        Debug.Log("destoryed at index : "+index);
        // add your implementation below
    }

    /// <summary>
    /// Determines the index of the given item using binary search
    /// </summary>
    /// <param name="item">the item to find</param>
    /// <returns>the index of the item or -1 if it's not found</returns>
    public int IndexOf(T item)
    {
        int lowerBound = 0;
        int upperBound = items.Count - 1;
        int location = -1;

        // loop until found value or exhausted array
        while ((location == -1) &&
            (lowerBound <= upperBound))
        {
            // find the middle
            int middleLocation = lowerBound + (upperBound - lowerBound) / 2;
            T middleValue = items[middleLocation];

            // check for match
            if (middleValue.CompareTo(item) == 0)
            {
                location = middleLocation;
         //       Debug.Log(location);
            }
            else
            {
                // split data set to search appropriate side
                if (middleValue.CompareTo(item) > 0)
                {
                    
           //         Debug.Log("greater than middle");
                    if (lowerBound != items.Count - 1)
                        lowerBound = middleLocation + 1;
                    else
                        lowerBound = middleLocation;
                }
                else
                {
             //       Debug.Log("less than middle");
                    upperBound = middleLocation - 1;
                }
            }
        }
        //Debug.Log("index to destroy : "+location);
        return location;
    }

    /// <summary>
    /// Sorts the list
    /// </summary>
    public void Sort()
    {
        SortedList<T> temp = new SortedList<T>();
        for (int i = 0; i < items.Count; i++)
            temp.Add(items[i]);

        for (int i = 0; i < items.Count; i++)
            items[i] = temp[i];
    }

    #endregion
}
