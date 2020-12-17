using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class _13
	{
		public List<string> lines = new List<string>();
		public int departTime;
		public List<int> buses = new List<int>();
		public Dictionary<int, int> cache = new Dictionary<int, int>();
		public static int index0 = 0;
		public static int index1 = 1;


		public _13()
		{
			lines = File.ReadAllLines("test.txt").ToList();

			//departTime = int.Parse(lines[0]);

			//buses.AddRange(lines[1].Replace("x,", "").Split(',').Select(x => int.Parse(x)).OrderBy(x => x));
			buses.AddRange(lines[1].Replace("x,", "-1,").Split(',').Select(x => int.Parse(x)));

			//var bus = Part1();

			var ans = Part2();
		}

		private int Part2()
		{
			
			
			for (int i = 0; i < buses.Count - 1; ++i)
			{
				var resume = helper(buses[i], buses[i + 1], i, i + 1);
				if (!resume)
				{
					i = -1;
				}
			}




			return 0;
		}

		private bool helper(int first, int second, int indexFirst, int indexSecond)
		{
			if (second == -1)
			{
				cache[indexSecond] = cache[indexSecond - 1] + 1;
				return true;
			}
			var mutableFirst = first;
			var mutableSecond = second;

			//if second has key so does first
			if (cache.ContainsKey(indexSecond))
			{
				mutableSecond = cache[indexSecond];
				mutableFirst = cache[indexFirst] + first;//increment first for next iteration

			}
			else if (cache.ContainsKey(indexFirst))
			{
				mutableFirst = cache[indexFirst];
				if (ShouldStartOver(mutableFirst, second))
				{
					return false;
				}
			}
			
			
			while(true)
			{
				if (mutableSecond > mutableFirst)
				{
					if (mutableFirst + 1 == mutableSecond)
					{
						cache[indexFirst] = mutableFirst;
						cache[indexSecond] = mutableSecond;
						return true;
					}
					mutableFirst += first;
					continue;
				}
				mutableSecond += second;
			}
		}

		private bool ShouldStartOver(int first, int second)
		{
			//16 3
			var divsor = first / second;
			var result = divsor * second + second;
			if (first + 1 == result)
				return false;
			return true;
		}

		private bool CheckCache()
		{
			for (int i = 0; i < cache.Count-1; ++i)
			{
				if (cache[0] + 1 != cache[1])
					return false;
			}

			return true;
		}

		private int Part2Helper(int big, int small)
		{

			//var first = buses[0];
			//var second = buses[0 + 1];

			//mutable    base
			int larger = big;
			int smaller = small;
			
			//15%4 = 3, 15%8 = 7 15%12 = 3 .. 45%4 = 1
			var result = larger % smaller;
					
			while (result != 1)
			{
				smaller += small;//increment by base
				if (smaller > larger)
				{
					larger += big;//incrment by base and start over
					smaller = small;//reset
				}
				result = larger % smaller;
			}

			var divisor = larger / smaller;
			var earliestTimestamp = divisor * smaller;

			return earliestTimestamp;
		}

		private int Part1()
		{
			int earliestDepartTime = int.MaxValue;
			int lowestbus = 0;

			foreach (var bus in buses)
			{
				var diff = departTime / bus;
				if (diff == 0)
				{
					return bus;
				}

				var lowest = (diff * bus) + bus;

				if (lowest < earliestDepartTime)
				{
					earliestDepartTime = lowest;
					lowestbus = bus;
				}
			}

			return lowestbus * (earliestDepartTime - departTime);
		}
	}
}
