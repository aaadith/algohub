using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class IntUtils
    {
        public static int GetNumDigits(this int x)
        {
            return (int)Math.Log10(x) + 1;
        }
    }
}
