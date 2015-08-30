using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 
 * Bubble Sort
 * 
 * time Complexity : O(n^2)
 * 
 * Most rudimentary comparison based sorting algorithm.
 * Basic idea: At each step, if two adjacent elements are not in order, they will be swapped. With each pass, larger elements will bubble towards the end. Hence the name.
 * Worst case : When te list is in descending order, every single pair will get swapped in every pass. There will be O(n^2) movements.
 * This algorithm is almost never recommended since Insertion sort has the same complexity and requires only O(n) swaps.
 * Bubble sort is a stable sorting alogrithm since two equal elements will never be swapped.
 * 
 * Reference:
 * http://www.algorithmist.com/index.php/Bubble_sort
 * 
 *
 * Author : Shravana Aadith
 */


namespace Algorithms.Sorting
{
    public class BubbleSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 6, 9, 9, 9, 4, 4, 4, 4, 6, 4, 4, 2, 2, 2, 5, 5 };
            BubbleSort.OptimizedSort2(a);
            Utils.Print(a);
        }
    }



    public class BubbleSort
    {
        public static void BasicSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length-1; j++)
                {
                    if (a[j] > a[j+1])
                        Utils.Swap(a, j, j+1);
                }
                //Utils.Print(a);
            }
        }


        /*
         * Optimizations to basic bubble sort:
         * 
         * 1. If a pass goes without any swap, the list is swapped and we need not proceed with further iterations
         * 2. With each pass, one additional element gets to its final position. Specifically, after the kth pass, last k elements in the list would be in sorted order and need not be iterated through in subsequent passes
         * 
         */

        public static void OptimizedSort(int[] a)
        {
            bool swapped = true;

            for (int i = 0; i < a.Length; i++)
            {
                if (swapped)
                {
                    swapped = false;
                    for (int j = 0; j < a.Length - i - 1  /*optimization 2 described above*/ ; j++)
                    {                        
                        if (a[j] > a[j + 1])
                        {
                            Utils.Swap(a, j, j + 1);
                            swapped = true;
                        }
                    }
                }
                Utils.Print(a);
            }

        }


        /*
         *Further optimization over Optimizedsort method.
         *If a[k] and a[k+1] were the elements last swapped in the previous pass, then, values from a[k+1] to a[end] would be in their final positions.
         * 
         */


        public static void OptimizedSort2(int[] a)
        {
            int bound = a.Length - 1;
            for (int i = 0; i < a.Length; i++)
            {                
                int newbound = 0;
                for (int j = 0; j < bound ; j++)
                {
                        if (a[j] > a[j + 1])
                        {
                            Utils.Swap(a, j, j + 1);
                            newbound = j;
                        }
                }
                bound = newbound;
            }
                Utils.Print(a);
        }
    }
}
