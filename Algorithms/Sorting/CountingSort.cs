using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Count Sort
 * 
 * Time complexity : O(n)
 * 
 * Counting sort is a stable sorting alogirthm ie, order of input records with the same key will be preserved in the output.
 * Unlike most sorting algorithms, Counting sort is not comparison based. Exploits the fact that the inputs are in a limited range to beat thhe O(nlogn) comparison algorithms.
 * It is a kind of histogram sorting technique
 * 
 * 
 * 
 * Problems:
 * Not too practical if the range of values to be sorted is too large. Solution : Radix sort. Counting sort forms the basis of radix sort
 * 
 * Resources:
 * http://courses.csail.mit.edu/6.006/spring11/rec/rec11.pdf (has good explanation)
 * http://www.algorithmist.com/index.php/Counting_sort
 *
 * Author : Shravana Aadith
 */




namespace Algorithms.Sorting
{

    public class CountingSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 6, 9, 9, 9, 4, 4, 4, 4, 6, 4, 4, 2, 2, 2, 5, 5 };
            int[] result = CountingSort.Sort(a);
            Utils.Print(result);
        }
    }


    public class CountingSort
    {
        public static int[] Sort(int[] a)
        {
            //b will be used to construct sorted array
            int[] b = new int[a.Length];

            //c used to maintain histogram of values in incoming array.
            //Each position in array will be used to store the frequency of value corresponding to that position's index
            int[] c = new int[a.ToList<int>().Max()+1];


            //Step 1: store frequency of each value contained in a into c. 
            //this step takes O(n)
            for (int i = 0; i < a.Length; i++)
                c[a[i]] = c[a[i]] + 1;

            //Step 2: store cumulative frequency of values in c
            //this would be the position of the last occurence of the elements in the final array
            //this step takes O(k), where k is te number of distinct elements
            for (int i = 1; i < c.Length; i++)
                c[i] = c[i] + c[i - 1];

            //Step 3: Populated sorted values into b using c (step through to understand)
            //in each iteration we populate the kth occurence of the given element in input array into the final result 
            //this step takes O(n)
            //note : here we iterate backwards to preserve the order of the duplicate elements. this makes it a stable sorting algorithm.
            for (int i = a.Length-1; i>=0 ; i--)
            {
                b[c[a[i]] - 1] = a[i];
                c[a[i]] = c[a[i]] - 1;
            }


            //Overal : O(n+k+n) = O(2n+k) = O(n)
            return b;

        }
    }
}
