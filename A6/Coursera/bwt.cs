using System;
using System.Linq;

class BurrowsWheelerTransform
    {
        static public void Main(string[] args)
        {
            string text = Console.ReadLine();
            string res = BWT(text);
            System.Console.WriteLine(res);
        }

        private static string BWT(string text)
        {
            string[]  cyclicRotations = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                cyclicRotations[i] = text.Substring(i) + text.Substring(0,i);
            }
            cyclicRotations = cyclicRotations.OrderBy(s => s).ToArray();
            string BWT_text = "";
            foreach (string s in cyclicRotations)
            {
                BWT_text += s[text.Length - 1];
            }
            return BWT_text;
        }
    }