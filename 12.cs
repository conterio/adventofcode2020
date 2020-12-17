using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class _12
	{
		public Facing facing;
		public Facing previousFacing;
		public int currentAngle = 90;
		public int east = 0;
		public int north = 0;

		public int unitsEast = 10;
		public int unitsNorth = 1;


		public static List<Tuple<char, int>> commands;

		public List<string> lines = new List<string>();
		public _12()
		{
			lines = File.ReadAllLines("12.txt").ToList();
			commands = new List<Tuple<char, int>>();

			foreach (var command in lines)
			{
				int num = int.Parse(command.Substring(1));
				commands.Add(new Tuple<char, int>(command[0], num));
			}

			Part2();

			var ManhattanDistance = Math.Abs(north) + Math.Abs(east);

		}

		private void Part2()
		{
			foreach (var command in commands)
			{
				switch (command.Item1)
				{
					case 'N':
						unitsNorth += command.Item2;
						break;
					case 'S':
						unitsNorth -= command.Item2;
						break;
					case 'E':
						unitsEast += command.Item2;
						break;
					case 'W':
						unitsEast -= command.Item2;
						break;
					case 'L':
						previousFacing = facing;
						facing = ChangeFacing(-command.Item2);
						ChangeUnits();
						break;
					case 'R':
						previousFacing = facing;
						facing = ChangeFacing(command.Item2);
						ChangeUnits();
						break;
					case 'F':
						east = east + (unitsEast * command.Item2);
						north = north + (unitsNorth * command.Item2);
						break;
				}
				if (Math.Abs(unitsEast) > Math.Abs(unitsNorth))
				{
					if (unitsEast > 0)
					{
						UpdateFacing(Facing.East);
					}
					else
					{
						UpdateFacing(Facing.West);
					}
				}
				else
				{
					if (unitsNorth > 0)
					{
						UpdateFacing(Facing.North);
					}
					else
					{
						UpdateFacing(Facing.South);
					}
				}
			}

		}

		private void ChangeUnits()
		{
			int temp = 0;
			switch (facing)
			{
				case Facing.East:
					switch (previousFacing)
					{
						case Facing.North:
							temp = unitsEast;
							unitsEast = unitsNorth;
							unitsNorth = -temp;//negate
							break;
						case Facing.South:
							temp = unitsEast;
							unitsEast = Math.Abs(unitsNorth);
							unitsNorth = temp;//dont negate
							break;
						case Facing.West:
							unitsEast = Math.Abs(unitsEast);
							unitsNorth = -unitsNorth;//negate
							break;
					}
					break;
				case Facing.North:
					switch (previousFacing)
					{
						case Facing.East:
							temp = unitsEast;
							unitsEast = -unitsNorth;//negate
							unitsNorth = temp;//temp should always be positive
							break;
						case Facing.South:
							unitsEast = -unitsEast;//negate
							unitsNorth = Math.Abs(unitsNorth);//units norht should always be negative
							break;
						case Facing.West:
							temp = unitsEast;
							unitsEast = unitsNorth;//dont negate
							unitsNorth = Math.Abs(temp);
							break;
					}
					break;
				case Facing.South:
					switch (previousFacing)
					{
						case Facing.East:
							temp = unitsEast;
							unitsEast = unitsNorth;//dont negate
							unitsNorth = -temp;//temp should always be positive
							break;
						case Facing.North:
							unitsEast = -unitsEast;//negate
							unitsNorth = -unitsNorth;//negate
							break;
						case Facing.West:
							temp = unitsEast;
							unitsEast = -unitsNorth;//negate
							unitsNorth = temp;
							break;
					}
					break;
				case Facing.West:
					switch (previousFacing)
					{
						case Facing.East:
							unitsEast = -unitsEast;
							unitsNorth = -unitsNorth;
							break;
						case Facing.North:
							temp = unitsEast;
							unitsEast = -unitsNorth;//untisnorth should be positive
							unitsNorth = temp;//dont negate
							break;
						case Facing.South:
							temp = unitsEast;
							unitsEast = unitsNorth;
							unitsNorth = -temp;//negate
							break;
					}
					break;
			}
		}




		public enum Facing
		{
			East,
			North,
			South,
			West
		}

		private void UpdateFacing(Facing newFacing)
		{
			facing = newFacing;
			if (newFacing == Facing.North)
				currentAngle = 0;
			if (newFacing == Facing.East)
				currentAngle = 90;
			if (newFacing == Facing.South)
				currentAngle = 180;
			if (newFacing == Facing.West)
				currentAngle = 270;

		}

		private Facing ChangeFacing(int angle)
		{
			var newAngle = currentAngle + angle;

			if (newAngle > 270)
			{
				newAngle = newAngle % 360;
			}

			currentAngle = newAngle;

			switch (newAngle)
			{
				case 0:
					return Facing.North;
				case 90:
					return Facing.East;
				case 180:
					return Facing.South;
				case 270:
					return Facing.West;
				case -90:
					currentAngle = 270;
					return Facing.West;
				case -180:
					currentAngle = 180;
					return Facing.South;
				case -270:
					currentAngle = 90;
					return Facing.East;
			}

			throw new Exception("wont hit this");
		}


		private void Part1()
		{
			foreach (var command in commands)
			{
				switch (command.Item1)
				{
					case 'N':
						north += command.Item2;
						break;
					case 'S':
						north -= command.Item2;
						break;
					case 'E':
						east += command.Item2;
						break;
					case 'W':
						east -= command.Item2;
						break;
					case 'L':
						facing = ChangeFacing(-command.Item2);
						break;
					case 'R':
						facing = ChangeFacing(command.Item2);
						break;
					case 'F':
						switch (facing)
						{
							case Facing.North:
								north += command.Item2;
								break;
							case Facing.East:
								east += command.Item2;
								break;
							case Facing.South:
								north -= command.Item2;
								break;
							case Facing.West:
								east -= command.Item2;
								break;
						}
						break;
				}
			}
		}


	}
}
