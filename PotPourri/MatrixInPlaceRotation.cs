using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotPourri
{
    
    public class MatrixInPlaceRotation
    {
        void Rotate(int[,] a, Tuple<int, int> p, Tuple<int, int> start)
        {
            int n = a.GetLength(0) - 1;
            int i = p.Item1, j = p.Item2;
            int newVal = a[n - j, i];  //previous element of the cycle
            Tuple<int, int> next = new Tuple<int, int>(j, n - i); //net element of the cycle
            if (!next.Equals(start))
                Rotate(a, next, start);
            a[i, j] = newVal;
        }

        public void Rotate(int[,] a)
        {
            int n = a.GetLength(0);
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i; j < n - i - 1; j++)
                {
                    Tuple<int, int> p = new Tuple<int, int>(i, j);
                    Rotate(a, p, p);
                    //Console.WriteLine("===================");
                    //Console.WriteLine(ArrayUtils.ToString(a));
                    //Console.WriteLine("===================");
                }
            }
        }


        /*
         *
         * 	b					a	
            7	4	1			1	2	3
            8	5	2			4	5	6
            9	6	3			7	8	9
	            r	c			r	c	
	            0	0			2	0	
	            0	1			1	0	
	            0	2			0	0	
	            1	0			2	1	
	            1	1			1	1	
	            1	2			0	1	
	            2	0			2	2	
	            2	1			1	2	
	            2	2			0	2	

         * 
         * b[i][j] = a[n-j][i]
         * 
         * 
         * 
         * 
         * 	a					b	
            1	2	3			7	4	1
            4	5	6			8	5	2
            7	8	9			9	6	3
            r	c				r	c	
            0	0				0	2	
            0	1				1	2	
            0	2				2	2	
            1	0				0	1	
            1	1				1	1	
            1	2				2	1	
            2	0				0	0	
            2	1				1	0	
            2	2				2	0	

         * b[j][n-i] => a[i][j]
         * 
         */


        /*
        static void Main()
        {
            int c = 1;
            int[,] a = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int[,] b = Transform(a);
            Console.WriteLine(ArrayUtils.ToString(b));
        }

        static int[,] Transform(int[,] a)
        {
            int n = a.GetLength(0);
            int[,] b = new int[n, n];
            
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<n;j++)
                {
                    Console.WriteLine("({0},{1})=>({2},{3})", i, j, j, n - i-i);
                    b[j, n - i-1]=a[i,j];
                }
            }
            return b;
        }*/


    }

}
