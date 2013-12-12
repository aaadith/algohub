using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Insertion Sort
 * 
 * Time Complexity : O(n^2)
 * 
 * 
 * THe problem with insertion sort is the data movement needed to make space at the right slot for the key in consideration at each pass.
 * Library sort is an algorithm that alleviates this problem by having gaps in the array to accelerate subsequent insertions. It has been shown that
 * Library sort has a high probability of running in O(nlogn) as against O(n^2) that Insertion sort takes. Drawback of library sort is that it requires
 * extra space for the gaps
 * 
 * Reference:
 * 
 * http://en.wikipedia.org/wiki/Library_sort
 * 
 * 
 */



namespace Algorithms.Sorting
{

    public class InsertionSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            Utils.Print(a);
            InsertionSort.Sort(a);
            Utils.Print(a);
        }
    }


    class InsertionSort
    {
        public static void Sort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                int current = a[i];
                int j;
                for (j = i - 1; j > 0 && a[j] > current; j--)
                {
                    a[j + 1] = a[j];
                }
                a[j + 1] = current;
            }
        }


        //TODO: implement using lists so that elements dont have to be moved by one position in each iteration
        public static void EfficientImplementation(int[] a)
        {
            List<int> A = a.ToList<int>();

            for (int i = 1; i < A.Count; i++)
            {
                int current = A[i];
                int j;
                for (j = i - 1; j > 0 && a[j] > current; j--)
                {
                    a[j + 1] = a[j];
                }
                a[j + 1] = current;
            }
        }


    }
}
