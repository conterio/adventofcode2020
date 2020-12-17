using System;
using System.Collections.Generic;
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
		static void Main(string[] args)
		{
			//new _8();
			//new _10();
			//new _11();
			//new _12();
			new _13();


			//string searchKey = "shiny gold";
			//Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();
			
			//var lines = File.ReadAllLines("7.txt").ToList();

			//foreach (var line in lines)
			//{
			//	var keyEndPosition = line.IndexOf("bag");
			//	var bagKey = line.Substring(0, keyEndPosition - 1);
			//	bags.Add(bagKey, null);

			//	var innerBags = line.Split(',');

			//	var innerBagStartPosition = line.IndexOf("contain");
			//	var innerBagCount = line.Substring(innerBagStartPosition + 8, 1);
				
			//	if (int.TryParse(innerBagCount, out var num))
			//	{
			//		var innerBagKeyEndPos = line.IndexOf("bag", innerBagStartPosition) - 1;
			//		var start = innerBagStartPosition + 10;
			//		var end = innerBagKeyEndPos - start;
			//		var innerBagKey = line.Substring(start, end);
			//		bags[bagKey] = new Dictionary<string, int>() { { innerBagKey, num } };

			//		for (int i = 1; i < innerBags.Count(); ++i)
			//		{
			//			innerBagCount = innerBags[i].Substring(1, 1);
			//			var innerBag1KeyEndPos = innerBags[i].IndexOf("bag") - 1;
			//			var start1 = 3;
			//			var end1 = innerBag1KeyEndPos - start1;
			//			var innerBagKey1 = innerBags[i].Substring(start1, end1);
			//			bags[bagKey][innerBagKey1] = int.Parse(innerBagCount);
			//		}
			//	}
			//	else
			//	{
			//		bags[bagKey] = new Dictionary<string, int>();
			//		continue;
			//	}

			//}

			//CountOfBags(bags, searchKey);


		}//end main

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