using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonProblems
{
    class SquareRoot
    {
        static double error = 0.00009;
        
        /*
         * basic idea : let our initial estimate for square root of S be x. If x is an overestimate of the square root of x, then S/x would be an underestimate.
         * further, the extent to which x is an overestimate, S/x is an understimate by the same extent. Average of x and S/x would be a closer apporximation of 
         * the square root. By applyng this repeatedly, we will rapidly converge towards yhe square root.
         * 
         */

        static double NewtonsMethod(int n)
        {
            double root = n;

            while (Math.Abs(root*root - n) > error)
            {
                
                root = (root + n /root) / 2;
            }


            return root;
        }

        public static void Main()
        {
            Console.WriteLine(NewtonsMethod1(3));
        }
    }
}
