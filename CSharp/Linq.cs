using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Linq
{
    public class SomeClass
    {
        public int A { get; set; }

        public int B { get; set; }
    }
    public class LinqUsage
    {
        public static void GetMax()
        {
            SomeClass a = new SomeClass { A = 3, B = 2 };
            SomeClass b = new SomeClass { A = 1, B = 2 };
            SomeClass c = new SomeClass { A = 2, B = 2 };
            SomeClass d = new SomeClass { A = 4, B = 2 };


            SomeClass[] l = new SomeClass[] { a, b, c, d };
            List<SomeClass> s = l.Where(x=>x.B==2).ToList<SomeClass>();
            
            int? max = s.Max(x => x.A);
            Console.WriteLine(max);

        }
    }
}
