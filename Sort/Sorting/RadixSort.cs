using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Radix sort
 * 
 * Time Complexity : O(n)
 * 
 * Basic idea : Put each item in input into a separate bucket based on key derived from the item. Then gather elements from
 * the buckets and arrange them sequentially. Repeat this for diffrent keys, the keys moving from LSD to MSD of each element.
 * 
 * Reference:
 * http://www.cprogramming.com/tutorial/computersciencetheory/radix.html
 * http://www.personal.kent.edu/~rmuhamma/Algorithms/MyAlgorithms/Sorting/radixSort.htm
 * 
 * Author : Shravana Aadith
 * 
 */


namespace Algorithms.Sorting
{

    public class RadixSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            Utils.Print(a);
            RadixSort.Sort(a);
            Utils.Print(a);            
        }
    }


    public class RadixSort
    {
        public static void Sort(int[] a)
        {
            bool GotNonZeroKeyforAtLeastOneElementInInput = false;

            int position = 1;

            // Repeat the following until all digits from LSD(least significant digit) to MSD(most..) 
            // have been considered from every item in the input:
            //1 Initialize the buckets; one bucket per possible key (digits in this case)
            //2 For each element, get the ith digit. This would be the key for thhe current iteration. 
            //    Use the key to put the element into the right bucket
            //3 Gather elements from buckets and arrange them sequentially
            do
            {
                GotNonZeroKeyforAtLeastOneElementInInput = false;
                
                List<List<int>> buckets = new List<List<int>>();
                for (int i = 0; i <= 9; i++)
                {
                    buckets.Add(new List<int>());
                }

                for (int i = 0; i < a.Length; i++)
                {
                    int key = GetDigitAtPosition(a[i],position);
                    if (key != -1)
                    {
                        GotNonZeroKeyforAtLeastOneElementInInput = true;
                    }
                    else
                    {
                        key = 0;
                    }
                    buckets[key].Add(a[i]);
                }

                int k = 0;
                for (int i = 0; i < buckets.Count; i++)
                {
                    List<int> bucket = buckets[i];
                    for (int j = 0; j < bucket.Count; j++)
                    {
                        a[k++] = bucket[j];
                    }
                }
                position++;
                Utils.Print(a);
            }
            while (GotNonZeroKeyforAtLeastOneElementInInput);
            
        }

        //Returns the digit at a given position from n. position=1 returns digits at units position,
        //2 returns digit at tens position and so on
        public static int GetDigitAtPosition(int n, int position)
        {
            n=n/((int) Math.Pow(10, position-1));
            if(n==0)
                return -1;
            return n%10;
        }



    }
}
