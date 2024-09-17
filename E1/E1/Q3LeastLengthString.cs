using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace E1
{
    public class Q3LeastLengthString : Processor
    {
        public Q3LeastLengthString(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        E1Processors.ProcessQ3FindAllOccur(inStr, Solve);

        public long Solve(string text, long k)
        {
            // throw new NotImplementedException();
            int[] prefixFunc = ComputePrefixFunction(text);
            int longestBorder = prefixFunc[text.Length-1];
            return text.Length + (k - 1)*(text.Length - longestBorder);
        }

        private static int[] ComputePrefixFunction(string p)
        {
            int[] res = new int[p.Length];
            res[0] = 0;
            int border = 0;
            for (int i = 1; i < p.Length; i++)
            {
                while (border > 0 && p[i] != p[border])
                {
                    border = res[border - 1];
                }
                if (p[i] == p[border])
                {
                    border++;
                }
                else
                {
                    border = 0;
                }
                res[i] = border;
            }
            return res;
        }
    }
}