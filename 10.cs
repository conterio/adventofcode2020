using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class _10
	{
		//get indexes that meet citeria			
			//if none set the value of that index to 1 - go on to next index				
			//if more return the the sum of all of them
		public static ulong total;

		public static List<int> lines = new List<int>();
		public static Dictionary<long, long> cache = new Dictionary<long, long>();
		public _10()
		{
			lines = File.ReadAllLines("10.txt").Select(x => int.Parse(x)).ToList();
			
			lines.Add(0);
			lines.Add(lines.Max() + 3);
			lines = lines.OrderBy(x => x).ToList();

			var result = Part2(lines.Count -1);

		}
		public long Part2(int index)
		{
			if (index == -1)
			{
				return cache[0];
			}

			var indexes = lines
				.Where(x => x > lines[index] && x <= lines[index] + 3)
				.Select(x => lines.IndexOf(x)).ToList();

			if (indexes.Count == 0)
			{
				cache[index] = 1;
			}
			else
			{
				long total = 0;
				foreach (var i in indexes)
				{
					total += cache[i];
				}
				cache[index] = total;
			}

			return Part2(--index);
		}

		


		

		public void Part1(List<int> lines)
		{
			

			int diff1 = 0;
			int diff3 = 0;

			var min = lines.Min();

			lines = lines.OrderBy(x => x).ToList();

			for (int i = 0; i < lines.Count; ++i)
			{
				if ( i + 1 >= lines.Count)
				{
					var total = diff1 * diff3;
					break;
				}

				int diff = lines[i+1] - lines[i];

				if (diff == 1)
				{
					++diff1;
				}
				else if (diff == 3)
				{
					++diff3;
				}
				else
				{
					Console.WriteLine(diff);
				}
			}

		}
		
	}
}
