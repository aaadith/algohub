using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Combinatorics;

namespace Algorithms
{
    

    public class Program
    {
        static void Main(string[] args)
        {
            PowerSetGenerator p = new PowerSetGenerator();
            int[] a = new int[] { 1, 2 };
            Console.WriteLine(a.ToString1());
            /*
            foreach(int[] s in p.GetSubsets(a))
            {
                ArrayUtils.Print(s);
            }*/
        }
    }
}
