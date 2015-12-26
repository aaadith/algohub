using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class StringBuilderUsage
    {
        public void TestUsage()
        {
            StringBuilder s = new StringBuilder();
            s.Append("a");
            s.Append('a'); // appending char
            Console.WriteLine(s.Length); // stringbuilder has length property which gives length at given point in time
            s.Append(1); //appending int
            Console.WriteLine(s.ToString());
        }
    }
}
