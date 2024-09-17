using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1
{
	public class E1Processors
	{

		public static string ProcessQ3SubStrings(string inStr, Func<long, string, long> Solve)
		{
			var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			var n = long.Parse(lines[0]);

			String s = lines[1];

			return Solve(n, s).ToString();
		}

		public static string ProcessQ3FindAllOccur(string inStr, Func<string, long, long> Solve)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var n = long.Parse(lines[0]);

            String s = lines[1];

            return Solve(s, n).ToString();
        }
	}
}