using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Shuffling
{
    public class SufflerClient
    {
        public static void Run()
        {
            int[] a = { 2,5,6,7,9,12,15};
            Shuffler.Shuffle(a);
            Utils.Print(a);
        }
    }
    public class Shuffler
    {
        public static void Shuffle(int[] a)
        {
            Random r= new Random();

            for (int i = 0; i < a.Length; i++)
            {
                int k=r.Next(0,i+1);
                Utils.Swap(a, i, k);
            }
        }
    }
}
