using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Params
    {
        void MethodTakingVariableNumberOfArguments(int someArg, params int[] moreArgs)
        {
            int sum = someArg;
            foreach(int x in moreArgs)
            {
                sum += x;
            }
        }

        void Client()
        {
            MethodTakingVariableNumberOfArguments(1, 2, 3, 4, 5);
        }
    }
}
