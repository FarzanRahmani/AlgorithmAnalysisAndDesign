using System;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
        {
            return BWT(text);
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
}
