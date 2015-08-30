using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Sorting
{

    public class HeapSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            Utils.Print(a);
            HeapSort.Sort(a);
            Utils.Print(a);
        }
    }


    public class HeapSort
    {

        public static void Sort(int[] a)
        {
            for (int i = a.Length; i > 0 ; i--)
            {
                Heapify(a, i);
                Utils.Swap(a, 0, i - 1);
                Utils.Print(a);
            }
        }




        //Heapifies the first n elements in the given array a
        public static void Heapify(int[] a, int n)
        {
            for (int i = n / 2; i > 0; i--)
            {
                int left = i * 2-1, right = i * 2, current=i-1 ;

                //Console.WriteLine("i:{0},current:{1},left:{2},right:{3}",i,current,left,right);

                int max_child;
                if (right >= n)
                    max_child = left;
                else
                    max_child = (a[left] > a[right]) ? left : right;
                if (a[current] < a[max_child])
                {
                    //Console.WriteLine("swapping {0}({1}) with {2}({3})", a[current], current, a[max_child], max_child);
                    Utils.Swap(a, current, max_child);                    
                }
            }
            Console.Write("heapify:");
            Utils.Print(a);
        }
        
    }
}
