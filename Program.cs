using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		public static HashSet<string> exceptKeys = new HashSet<string>();
		public static int realCount = 0;
		public static List<int> addList = new List<int>();
		public static List<int> multList = new List<int>();
		public static StreamReader file = new StreamReader("new.txt");
		static void Main(string[] args)
		{

			var line = file.ReadLine().Split(" ").Select(Int32.Parse).ToList();


			

			minimumBribes(line);
			//minimumBribes(new List<int>() { 2, 5, 1, 7, 9 });


		}



		//2 4 5 3 1 8 9 7 11 10 6 13 15 12 14 18 16 17 21 20 19 24 23 26 27 22 29
		public static void minimumBribes(List<int> q)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();

			int n = q.Count;
			int[] arr = new int[n];
			int bribes = 0;
			int start = 0;

			for (int i = 0; i < n; i++)
			{
				arr[q[i] - 1] = 1;

				int currentBribes = 0; 

				for (int j = q[i] - 1; j >= start; j--)
				{
					if (arr[j] == 0)
					{
						++currentBribes;
						if (currentBribes > 2)
						{
							Console.WriteLine("Too chaotic");
							return;
						}
					}
				}

				if (currentBribes == 0)
				{
					start = q[i];
				}
				else
				{
					bribes += currentBribes;
				}
			}

			Console.WriteLine(bribes);

			timer.Stop();
			var time = timer.Elapsed;

		}


































		public static int fillMissingBrackets(string s)
		{
			Stack<char> stack = new Stack<char>();

			int count = 0;

			foreach (var ch in s)
			{
				if (ch == '(' || ch == '[')
				{
					stack.Push(ch);
				}
				else if (ch == ']')
				{
					if (stack.Count == 0)
					{
						return count;
					}
					else if (stack.Peek() == '[' || stack.Peek() == '?')
					{
						++count;
						stack.Pop();
					};
				}
				else if (ch == ')')
				{
					if (stack.Count == 0)
					{
						return count;
					}
					else if (stack.Peek() == '(' || stack.Peek() == '?')
					{
						++count;
						stack.Pop();
					};
				}
				else if (ch == '?')
				{
					if (stack.Count == 0)
					{
						stack.Push(ch);
					}
					else if (stack.Peek() == '(' || stack.Peek() == '[' || stack.Peek() == '?')
					{
						++count;
						stack.Pop();
					}
				}
			}

			return count;

			//[] [? () (?  ?] ?) ??

			
			//does first make pair with second
			//yes - increment base - move base back if possible else move forward
			//no - increment base 
			int balancedStrings = 0;

			for (int i = 0; i < s.Length; ++i)
			{
				if (i + 1 >= s.Length)
					return balancedStrings;

				var first = s[i];
				var second = s[i + 1];

				var doesMakePair = MakesPair(s[i], s[i + 1]);

				if (doesMakePair)
				{
					++balancedStrings;
					string newString = s.Remove(i, 2);
					return fillMissingBrackets(newString) + balancedStrings;
				}

			}
			return balancedStrings;

		}

		private static bool MakesPair(char first, char second)
		{
			switch (first)
			{
				case '(':
					if (second == ')' || second == '?')
						return true;
					break;
				case '[':
					if (second == ']' || second == '?')
						return true;
					break;
				case '?':
					if (second == ')' || second == ']' || second == '?')
						return true;
					break;
				default:
					return false;
			}
			return false;
		}


		public static void minimumBribes1(List<int> q)
		{
			int bribe = 0;
			for (int i = 0; i < q.Count; ++i)
			{
				int coutOfLessThanToTheRight = q.GetRange(i + 1, q.Count - (i + 1)).Count(x => x < q[i]);

				if (coutOfLessThanToTheRight > 2)
				{
					Console.WriteLine("Too chaotic");
					return;
				}

				bribe += coutOfLessThanToTheRight;
			}
			Console.WriteLine(bribe);
		}


		public static List<int> rotLeft(List<int> a, int d)
		{
			//var result = rotLeft(new List<int>() { 1, 2, 3, 4, 5 }, 10);
			if (a.Count > d)
			{
				var beginingPart = a.GetRange(0, d);
				var remainingPart = a.GetRange(d, a.Count - d);
				return remainingPart.Concat(beginingPart).ToList();
			} 
			else if (a.Count < d)
			{
				var mod = d % a.Count;
				if (mod != 0)
				{
					return rotLeft(a, mod);
				}
			}

			return a;
		}

		public static long repeatedString(string s, long n)
		{
			//var count = repeatedString("a", 1000000000000);
			long count = 0;
			//string longer
			if (s.Length > n)
			{
				s = s.Substring(0, (int)n);
				count = s.Count(x => x == 'a');
			}
			//same
			else if (s.Length == n)
			{
				s.Count(x => x == 'a');
				count = s.Count(x => x == 'a');
			}
			//string shorter
			else
			{
				long div = n / s.Length;
				var mod = n % s.Length;
				if (mod != 0)
				{
					var moddedString = s.Substring(0, (int)mod);
					count += moddedString.Count(x => x == 'a');
				}
				count += s.Count(x => x == 'a') * div;
			}

			return count;
		}

		public static int jumpingOnClouds(List<int> c)
		{
			//var jumps = jumpingOnClouds(new List<int>() { 0, 0, 0, 0, 1, 0 });
			int jumps = 0;

			for (int i = 0; i < c.Count; ++i)
			{
				if (i + 2 < c.Count && c[i + 2] == 0)
				{
					++jumps;
					i += 1;
				}
				else if (i + 1 < c.Count && c[i + 1] == 0)
				{
					++jumps;
				}
			}

			return jumps;
		}


		public static int countingValleys(int steps, string path)
		{
			//var valleys = countingValleys(8, "UDDDUDUU");
			int alt = 0;
			int valleys = 0;

			foreach (var step in path)
			{
				switch (step)
				{
					case 'U':
						alt += 1;
						break;
					case 'D':
						alt -= 1;
						break;
					default:
						throw new Exception("invalid character");
				}

				if (step == 'U' && alt == 0)
				{
					++valleys;
				}
			}

			return valleys;
		}

		
		public static int sockMerchant(int n, List<int> ar)
		{
			//	var pairs = sockMerchant(9, new List<int>() { 10, 20, 20, 10, 10, 30, 50, 10, 20 });		
			int pairs = 0;

			ar.Sort();

			for (int i = 0; i < n; ++i)
			{
				if (i == n -1)
				{
					break;
				}
				if (ar[i] == ar[i +1])
				{
					++pairs;
					++i;
				}
			}

			return pairs;
		}





		//private static int CountOfBags(Dictionary<string, Dictionary<string, int>> bags, string searchKey)
		//{
		//	var bagsInsideMyBag = bags[searchKey].ToList();

		//	if (bagsInsideMyBag.Count == 0)
		//	{
		//		return 0;
		//	}

		//	int sib = 0;
		//	foreach (var bag in bagsInsideMyBag)
		//	{
		//		//addList.Add(bag.Value);
		//		var innerBags = CountOfBags(bags, bag.Key);
		//		if (innerBags == 0)
		//		{
		//			addList.Add(bag.Value);
		//		}
		//		else
		//		{
		//			innerBags *= bag.Value;
		//			innerBags += bag.Value;
		//		}
		//		sib += innerBags;

		//	}

		//	if (addList.Count == 0)
		//	{
		//		return sib;
		//	}

		//	//at this point add the add list
		//	var sum = addList.Sum(x => x);
		//	addList = new List<int>();
		//	return sum;
		//}

		//private static void ContainBagCount(Dictionary<string, Dictionary<string, int>> bags, string searchKey)
		//{
		//	var bagsThatContainKey = bags.Where(x => x.Value.ContainsKey(searchKey)).ToList();

		//	foreach (var bag in bagsThatContainKey)
		//	{
		//		if (!exceptKeys.Contains(bag.Key))
		//		{
		//			 ContainBagCount(bags, bag.Key);
		//		}
		//		exceptKeys.Add(bag.Key);
		//	}

		//}




	}//
}//