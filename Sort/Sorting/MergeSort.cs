using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Sorting
{

    public class MergeSortClient
    {
        public static void Run()
        {
            int[] a = { 6, 16, 239, 75, 1214, 34, 49, 840, 96, 4, 104, 432, 2, 29, 62, 5, 14 };
            Utils.Print(a);
            MergeSort.Sort(a);
            Utils.Print(a);
        }
    }


    public class MergeSort
    {
        public static void Sort(int[] a, int left=-1, int right=-1)
        {
            if (left == -1 && right == -1)
            { left = 0; right = a.Length-1; }

            if (left < right)
            {
                int mid = (left + right) / 2;

                Sort(a, left, mid);
                Sort(a, mid + 1, right);
                Merge(a, left, mid, right);
                Utils.Print(a);
            }
        }

        static void Merge(int[] a, int left,int mid, int right)
        {
            Console.WriteLine("\n\nleft:{0},mid:{1};right:{2}\n\n",left,mid,right);
            int[] t=new int[right-left+1];
            int i1 = left, i2 = mid + 1;
            int i=0;
            while(i1<=mid && i2<=right)
            {
                if (a[i1] < a[i2])
                {
                    t[i++] = a[i1++];
                }
                else
                {
                    t[i++] = a[i2++];
                }
            }
            while(i1<=mid)
                t[i++] = a[i1++];

            while (i2 <= right)
                t[i++] = a[i2++];

            for (i = 0; i < t.Length; i++,left++)
            {
                a[left]=t[i];
            }
            
        }

    }
}
