using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Selection Sort
 * 
 * Time complexity : O(n^2)
 * 
 * In-place comparison based sorting technique. Known for its simplicity.
 * Has performance advantages over more complicated algorithms in certain situations.
 * 
 * Basic idea : At ith pass, bring ith smallest value to position i.
 * 
 * Selection sort is not a stable sort in general.
 * Reason : Consider this list - 4 4 3 5
 * In the first pass, 4 n first position will get swapped with 3, giving 3 4 4 5. Thhe ordering of the two 4s has changed here.
 * 
 * However, Selection Sort can be implemented as a stable sort. If instead of swapping elements, the minimum value is inserted 
 * into its position (ie all intervening elements moved down), the algorithm becomes stable. This modification requires use of
 * data structure like linked lsit that allows efficient insertions and eletions. OTherwise, it leads to Θ(n^2) writes.
 */





namespace Algorithms.Sorting
{
    public class SelectionSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 6, 9, 9, 9, 4, 4, 4, 4, 6, 4, 4, 2, 2, 2, 5, 5 };
            SelectionSort.Sort(a);
            Utils.Print(a);
        }
    }


    public class SelectionSort
    {

        public static void Sort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = i; j < a.Length; j++)
                {
                    if (a[i] > a[j])
                        Utils.Swap(a, i, j );
                }
                //Utils.Print(a);
            }
        }

    }
}
