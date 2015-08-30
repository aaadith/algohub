using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Sorting;


/*
 * Quick Select
 * 
 * Finds kth element in linear time on average.
 * 
 * min : k=0, max : k=N-1 (where N is the length of the array), median : N/2
 * 
 * Quick Select is a variation of Quick sort. In each iteration, partition the array as in quick sort, but
 * after partition check the boundary of partitions against k. If k is in the left of the partition boundary,
 * proceed with left partition alone and ignore the right partition and vice versa. Repeat the process until
 * either the size of the parition has reached 1 or the boundray of parition is equal to k.
 * 
 * Note : While performance is linear on average, it will be quadratic on average (1/2 N^2).
 * By shuffling the array, chances of getting worst case performance is reduced significantly enough as to 
 * not to worry about it.
 * 
 */


namespace Algorithms.Selection
{
    public class QuickSelectionClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            int k=16;
            int kth = QuickSelection.QuickSelect(a,k);
            Console.WriteLine(kth);
        }
    }

    public class QuickSelection
    {
        public static int QuickSelect(int[] a, int k)
        {
            //shuffling done for guaranteed average performance
            Shuffling.Shuffler.Shuffle(a);

            int lo = 0, hi = a.Length-1;
            while (lo < hi)
            {
                int boundary = QuickSort.Partition(a, lo, hi);
                if (k < boundary)
                    hi = boundary - 1;
                else if (k > boundary)
                    lo = boundary + 1;
                else
                    return a[k];
            }
            return a[k];
        }
    }
}
