using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    /*
     * Given that, you can issue N keywtorkes and only following keystrokes are allowed : A, Ctrl+A (“Select All”), Ctrl+C ("Copy"), Ctrl+V ("Paste")
     * Find the length of the longest char sequence that can be produced.
     */
    public class MaxCharsWithGivenKeyStrokes
    {
        public static int GetMaxCharsWithGivenKeyStrokes(int numStrokes)
        {
            return GetMaxCharsWithGivenKeyStrokes(numStrokes, 0, 0, 0);
        }

        static Dictionary<Tuple<int, int, int, int>, int> lookup = new Dictionary<Tuple<int, int, int, int>, int>();

        static int GetMaxCharsWithGivenKeyStrokes(int remainingStrokes, int clipboardLength, int selectionLength, int lengthSoFar)
        {
            if (remainingStrokes == 0)
                return lengthSoFar;

            Tuple<int, int, int, int> key = new Tuple<int, int, int, int>(remainingStrokes, clipboardLength, selectionLength, lengthSoFar);

            int result;

            if (lookup.ContainsKey(key))
                result = lookup[key];
            else
            {
                int A = GetMaxCharsWithGivenKeyStrokes(remainingStrokes - 1, clipboardLength, 0, lengthSoFar + 1);
                int CtrlA = GetMaxCharsWithGivenKeyStrokes(remainingStrokes - 1, clipboardLength, lengthSoFar, lengthSoFar);
                int CtrlC = GetMaxCharsWithGivenKeyStrokes(remainingStrokes - 1, selectionLength, 0, lengthSoFar);
                int CtrlV = GetMaxCharsWithGivenKeyStrokes(remainingStrokes - 1, clipboardLength, 0, lengthSoFar + selectionLength);

                result = (new int[] { A, CtrlA, CtrlC, CtrlV }).Max();
                lookup[key] = result;
            }
            
            return result;
        }
    }
}
