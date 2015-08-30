using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 * Rapid Sort
 * 
 * Rapid sort only sorts keys. In other words, items to be sorted consist solely of the key; there is no additional data in items.
 * It is a kind of pigeonhole sort. It forms the basis of Counting Sort.
 * The algorithm is efficient if the range of keys is less than or equal to the number of items.
 * This is not a stable sorting algorithm.
 * 
 * References:
 * http://www.algorithmist.com/index.php/Counting_sort
 * http://xlinux.nist.gov/dads/HTML/rapidSort.html
 * http://en.wikipedia.org/wiki/Pigeonhole_sort
 * 
 * Author : Shravana Aadith
 */

namespace Algorithms.Sorting
{

    public class RapidSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 6, 9, 9, 9, 4, 4, 4, 4, 6, 4, 4, 2, 2, 2, 5, 5 };
            RapidSort.Sort(a);
            Utils.Print(a);
        }
    }


    public class RapidSort
    {
        public static void Sort(int[] a)
        {
            //b will be used to construct sorted array
            int[] b = new int[a.Length];

            //c used to maintain histogram of values in incoming array.
            //Each position in array will be used to store the frequency of value corresponding to that position's index
            int[] c = new int[a.ToList<int>().Max() + 1];


            //Step 1: store frequency of each value contained in a into c. 
            //this step takes O(n)
            for (int i = 0; i < a.Length; i++)
                c[a[i]] = c[a[i]] + 1;

            int p = 0;

            for (int i = 0; i < c.Length; i++)
            {
                for (int j = 0; j < c[i]; j++)
                {
                    a[p++] = i;
                }
            }

        }
    }

}
