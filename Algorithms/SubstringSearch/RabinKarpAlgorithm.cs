using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Sorting;

namespace Algorithms.SubstringSearch
{

    class RabinKarpAlgorithmClient
    {
        public static void Run()
        {
            //Console.WriteLine(RabinKarpAlgorithm.GetMatchIndex("needle", "this is a needle in haystack"));

            Console.WriteLine(RabinKarpAlgorithm.GetHash("s is "));
            //Console.WriteLine(RabinKarpAlgorithm.GetHash("his is"));

            //Console.WriteLine(RabinKarpAlgorithm.GetHash(RabinKarpAlgorithm.GetHash("this i"),'t','s',"needle".Length));
            //Console.WriteLine(RabinKarpAlgorithm.GetHash("his is"));

            //Console.WriteLine(RabinKarpAlgorithm.GetHash("this i") - RabinKarpAlgorithm.GetHash("his i"));
            //Console.WriteLine('t' * Math.Pow(256, "needle".Length - 1) % 100007);
        }
    }

    class RabinKarpAlgorithm
    {
        private static ulong radix = 256;
        
        private static ulong Q = 100007; //a prime number to hash against; the larger this value, the lesser the chances of hash collision

        public static int GetMatchIndex(string pattern, string text)
        {
            ulong patternHash = GetHash(pattern);

            int index = 0;
            string textSegment = text.Substring(0, pattern.Length);

            ulong textSegmentHash = GetHash(textSegment);

            if (textSegmentHash == patternHash)
                return index;

            int maxIndex = text.Length;


            for (index = pattern.Length; index < maxIndex; index++)
            {
                char firstCharInPrevSegment = text[index - pattern.Length];
                char lastCharInNewSegment = text[index];
                textSegmentHash = GetHash(textSegmentHash, firstCharInPrevSegment, lastCharInNewSegment, pattern.Length);

                Console.WriteLine("{0}\t{1}\t{2}", firstCharInPrevSegment, lastCharInNewSegment, textSegmentHash);

                if(textSegmentHash==patternHash)
                    Console.WriteLine("found");

            }



            //Console.WriteLine(GetHash(textSegmentHash, text[0], text[index], pattern.Length));
            return -1;
        }



        public static int GetMatchIndex_slow(string pattern, string text)
        {
            ulong patternHash = GetHash(pattern);

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                string textSegment = text.Substring(i, pattern.Length);
                ulong textSegmentHash = GetHash(textSegment);    
                Console.WriteLine(textSegmentHash);
            }

            int index = 0;
            

            
            return -1;
        }



        public static ulong GetHash(string pattern)
        {
            ulong patternHash = 0;
            foreach (char p in pattern)
            {
                patternHash = (patternHash * radix + p) % Q;
            }
            return patternHash;
        }

        public static ulong GetHash(ulong prevHash, char firstchar, char nextchar, int patternLength)
        {
            ulong hash = 0;

            ulong firstcharHash = (ulong)(firstchar * Math.Pow(radix, patternLength-1)%Q) % Q;

            hash = ((prevHash - firstcharHash)*radix)%Q;
            hash = (hash + nextchar)%Q;

            return hash;
        }
    }
}
