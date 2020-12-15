using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace AdventOfCode
{
	class _9
	{
		public static List<long> puzzle = new List<long>();
		public static int offset = 25;
		public _9()
		{
			var lines = File.ReadAllLines("9.txt").ToList();
			foreach (var line in lines)
			{
				puzzle.Add(long.Parse(line));
			}

			Part1();
		}

		public void Part1()
		{
			long sum1 = 0;
			for (int i = offset; i < puzzle.Count(); ++i)
			{
				bool found = false;
				//get starting posotion
				int startIndex = i - offset;
				
				//get previous offset numbers
				var sumList = puzzle.GetRange(startIndex, offset).ToList();

				sum1 = sumList.Sum();
				if (sum1 < 731031916)
				{
					Console.WriteLine(sum1);
				}
				else
				{
					for (int t = 0; t < sumList.Count; ++t)
					{
						for (int q = t+1; q < sumList.Count; ++q)
						{
							
							sum1 = sumList.Skip(t).Take(q).Sum();
							if (sum1 > 731031916)
								continue;
							if (sum1 == 731031916)
							{
								Console.WriteLine(sum1);

								var listt = sumList.Skip(t).Take(q).ToList();
								var ans = listt.Max() + listt.Min();
							}
						}
						
					}
				}


				for (int x = 0; x < sumList.Count; ++x)
				{
					if (found)
					{
						break;
					}
					for (int y = x + 1; y < sumList.Count; y++)
					{
						if (sumList[x] == sumList[y])
						{
							continue;
						}
						var one = sumList[x];
						var two = sumList[y];
						var sum = one + two;
						var total = puzzle[i];
						if (sumList[x] + sumList[y] == puzzle[i])
						{
							found = true;
							break;
						}
					}
				}

				if (!found)
				{
					Console.WriteLine($"bad item is line {i + 1} number {puzzle[i]}");
				}

			}

		}

		public void Part2()
		{

		}



	}	
}
