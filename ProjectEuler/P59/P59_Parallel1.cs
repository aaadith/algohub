using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectEuler.P59
{
    public class P59_Parallel1 : P59Base
    {
        static List<string> GenerateAllKeys()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        string key = new string(new char[] { (char)(i + 97), (char)(j + 97), (char)(k + 97) });
                        result.Add(key);
                    }
                }
            }
            return result;
        }

        public void FindKey()
        {
            string cipherText = GetCipherText();
            HashSet<string> dictionary = GetDictionary();
            List<string> keys = GenerateAllKeys();

            Stopwatch sw = new Stopwatch();

            object syncroot = new object();

            sw.Start();
            string decryptedText = null;
            float resultScore = 0.0f;
            Parallel.For(0, keys.Count, (i) =>
            {
                string key = keys[i];
                string result = Decrypt(cipherText, key);
                float score = GetPercentMatch(result, dictionary);
                if (score > resultScore)
                {
                    lock (syncroot)
                    {
                        if (score > resultScore)
                        {
                            decryptedText = result;
                            resultScore = score;
                        }
                    }
                }
            });
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

        }
    }
}
