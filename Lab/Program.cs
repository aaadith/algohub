using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Utils;

namespace Utils
{
    
    public class Program
    {
        

        static void Main(string[] args)
        {
            int[] a = new int[] { 1, 2, 3 };

            IEnumerable<int> e = a.Where(i => i > 2);
        }
    }
}
