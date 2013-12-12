using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Sorting
{

    public class QuickSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            //int[] a = { 5, 2, 1 };
            //int[] a = {6,16, 14,29,5,2,4 };
            Utils.Print(a);
            QuickSort.Sort(a);
            Utils.Print(a);
        }
    }




    class QuickSort
    {

        public static void Sort(int[] a)
        {
            Quicksort(a, 0, a.Length-1);
        }

        public static void Quicksort(int[] a, int left, int right)
        {
            Console.WriteLine("left:{0},right:{1}",left,right);
            if (left >= right)
                return;
            int boundary = Partition(a, left, right);
            Console.WriteLine("boundary:{0}",boundary);
            Utils.Print(a);
            Console.WriteLine("===");
            Quicksort(a, left, boundary-1);
            Quicksort(a, boundary+1, right);
        }

        public static int Partition(int[] a, int left, int right)
        {
            int pivot_idx = (left + right) / 2;
            int pivot = a[pivot_idx];
            Console.WriteLine("pivot:{0}({1})",pivot,pivot_idx);
            Utils.Swap(a,left,pivot_idx);
            pivot_idx=left;
            int i = left+1, j = right;
            
            while (true)
            {
                while ((i<=right) && (a[i] < pivot))
                {
                    i++;
                    if (i == right)
                        break;
                }
                while (a[j] > pivot)
                {
                    j--;
                    if (j==left)
                        break;
                }


                if (i > j)
                    break;
                    

                //Console.WriteLine("swapping {0}({1}) with {2}({3})", a[left], left, a[right], right);
                Utils.Swap(a, i, j);
                //Utils.Print(a);
            }
            Utils.Swap(a, j, pivot_idx);
            return j;

        }

    }
}
