using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
	public class ArrayHourGlass
	{
		//public int[,] arr = new int[,] {
		//	{ 1, 1, 1, 0, 0, 0 }, 
		//	{ 0, 1, 0, 0, 0, 0 }, 
		//	{ 1, 1, 1, 0, 0, 0 },
		//	{ 0, 0, 2, 4, 4, 0 },
		//	{ 0, 0, 0, 2, 0, 0 },
		//	{ 0, 0, 1, 2, 4, 0 },
		//};
		public int HourGlassTotal = 0;

		public List<List<int>> arr = new List<List<int>>()
		{
			new List<int>() { -1, -1,  0, -9, -2, -2 },
			new List<int>() { -2, -1, -6, -8, -2, -5 },
			new List<int>() { -1, -1, -1, -2, -3, -4 },
			new List<int>() { -1, -9, -2, -4, -4, -5 },
			new List<int>() { -7, -3, -3, -2, -9, -9 },
			new List<int>() { -1, -3, -1, -2, -4, -5 },
		};
		//-6, -20, -25, -24
		//-22, 

		public ArrayHourGlass()
		{
			HourGlassTotal = hourglassSum(arr);
		}

		public static int hourglassSum(List<List<int>> arr)
		{
			int maxGlass = int.MinValue;

			for (int i = 0; i < arr.Count - 2; ++i)
			{
				for (int j = 0; j < arr[i].Count - 2; ++j)
				{
					int top = arr[i][j] + arr[i][j + 1] + arr[i][j + 2];
					int mid = arr[i + 1][j + 1];
					int bot = arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];

					int hourGlasstotal = top + mid + bot;
					if (hourGlasstotal > maxGlass)
					{
						maxGlass = hourGlasstotal;
					}
				}
			}

			return maxGlass;
		}


	}
}
