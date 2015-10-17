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
    public class P59Base
    {
        protected string GetCipherText()
        {
            string line = File.ReadAllLines("P59\\cipher.txt").First();
            string[] codes = line.Split(',');
            char[] x = Array.ConvertAll(codes, code => (char)Int32.Parse(code));
            string cipherText = new string(x);
            return cipherText;
        }

        protected HashSet<string> GetDictionary()
        {
            string[] lines = File.ReadAllLines("P59\\CommonEnglishWords.txt");
            HashSet<string> dictionary = new HashSet<string>(lines);
            return dictionary;
        }

        protected string Decrypt(string cipherText, string key)
        {
            char[] decryptedChars = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                decryptedChars[i] = (char)(cipherText[i] ^ key[i % key.Length]);
            }

            string result = new string(decryptedChars);
            return result;
        }

        protected float GetPercentMatch(string text, HashSet<string> dictionary)
        {
            float result = 0;

            int total = text.Length;
            int matched = 0;
            char[] separators = new char[] { ' ', ',', '.', '?', '!', '(', ')' };
            matched = text.Count(x => separators.Contains(x));
            foreach (string word in text.Split(separators))
            {
                if (dictionary.Contains(word.ToLower()))
                    matched += word.Length;
            }
            result = matched * 1.0f / total;
            return result;
        }

    }
}
