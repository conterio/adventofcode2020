using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public class _11
	{	
		public List<string> lines = new List<string>();
		public SeatState[,] matrix;
		public int occupied;
		public int round = 0;
		public bool[,] cache;
		public bool changed;
		public int max;
		public int maxCol;

		public _11()
		{
			
			lines = File.ReadAllLines("11.txt").ToList();

			var rows = lines.Count;
			var columns = lines[0].Length;
			max = rows-1;
			maxCol = columns - 1;

			matrix = new SeatState[rows, columns];
			cache = new bool[rows, columns];


			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					matrix[row, column] = GetSeatFromChar(lines[row][column]);
				}
			}

			var seatlayout = matrix.Clone() as SeatState[,];
			//printState(seatlayout);

			Part2(seatlayout);


		}

		private void Part2(SeatState[,] seatLayoutState)
		{
			var newState = seatLayoutState.Clone() as SeatState[,];
			changed = false;

			//foreach seat run a round
			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					var seat = seatLayoutState[row, column];

					if (seat == SeatState.Floor || cache[row, column] == true)
					{
						continue;
					}
					//checked cache
					var neighbors = getNeighbors(row, column, seatLayoutState);
					//can cache 1 time
					if (neighbors.Count(x => x == SeatState.Floor) > 5)
					{
						cache[row, column] = true;
					}


					//rules
					if (seat == SeatState.Empty && !neighbors.Contains(SeatState.Occupied))
					{
						newState[row, column] = SeatState.Occupied;
						changed = true;
					}
					else if (seat == SeatState.Occupied && neighbors.Count(x => x == SeatState.Occupied) >= 5)
					{
						newState[row, column] = SeatState.Empty;
						changed = true;
					}

				}
			}
			//printState(newState, round);
			Console.WriteLine(++round);

			//compare new state to old state if same get number of occupied
			if (changed == false)
			{
				occupied = GetOccupiedCount(newState);
				return;
			}

			Part2(newState);
		}

		private void printState(SeatState[,] seatLayout, int round)
		{
			Console.WriteLine($"Round {round}");
			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					var letter = GetCharFromSeat(seatLayout[row, column]);
					Console.Write(letter);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}



		private int GetOccupiedCount(SeatState[,] seatLayout)
		{
			int occupied = 0;
			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					if (seatLayout[row, column] == SeatState.Occupied)
					{
						++occupied;
					}
				}
			}
			return occupied;
		}

		private SeatState KeepLookingForNeighbor(int row, int column, SeatState[,] seatLayout, int direction)
		{
			if (direction == 0)
			{
				while(row >= 0 )
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					--row;
				}
				return SeatState.Floor;
			}
			else if (direction == 1)
			{
				while (row >= 0 && column <= maxCol)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					++column;
					--row;
				}
				return SeatState.Floor;
			}
			else if (direction == 2)
			{
				while (column <= maxCol)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					++column;
				}
				return SeatState.Floor;
			}
			//downright
			else if (direction == 3)
			{
				while (row <= max && column <= maxCol)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					++column;
					++row;
				}
				return SeatState.Floor;
			}
			else if (direction == 4)
			{
				while (row <= max)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					++row;
				}
				return SeatState.Floor;
			}
			//down left
			else if (direction == 5)
			{
				while (row <= max && column >= 0)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					++row;
					--column;
				}
				return SeatState.Floor;
			}
			else if (direction == 6)
			{
				while (column >= 0)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					--column;
				}
				return SeatState.Floor;
			}
			else if (direction == 7)
			{
				while (column >= 0 && row >= 0)
				{
					if (seatLayout[row, column] == SeatState.Empty || seatLayout[row, column] == SeatState.Occupied)
					{
						return seatLayout[row, column];
					}
					--column;
					--row;
				}
				return SeatState.Floor;
			}

			return SeatState.Empty;
		}

		private SeatState[] getNeighbors(int row, int column, SeatState[,] seatLayout)
		{
			SeatState[] neighbors = new SeatState[8];

			if (row == 0 && column == 0)
			{
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
				neighbors[3] = KeepLookingForNeighbor(row + 1, column + 1, seatLayout, 3);
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
			}
			else if (row == 0 && column == maxCol)
			{
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
				neighbors[5] = KeepLookingForNeighbor(row + 1, column - 1, seatLayout, 5);
				neighbors[6] = KeepLookingForNeighbor(row, column - 1, seatLayout, 6);
			}
			else if (row == max && column == 0)
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[1] = KeepLookingForNeighbor(row - 1, column + 1, seatLayout, 1);
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
			}
			else if (row == max && column == maxCol)
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[6] = KeepLookingForNeighbor(row, column, seatLayout, 6);
				neighbors[7] = KeepLookingForNeighbor(row - 1, column - 1, seatLayout, 7);
			}
			else if (row == 0)
			{
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
				neighbors[3] = KeepLookingForNeighbor(row + 1, column + 1, seatLayout, 3);
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
				neighbors[5] = KeepLookingForNeighbor(row + 1, column - 1, seatLayout, 5);
				neighbors[6] = KeepLookingForNeighbor(row, column - 1, seatLayout, 6);
			}
			else if (row == max)
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[1] = KeepLookingForNeighbor(row - 1, column + 1, seatLayout, 1);
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
				neighbors[6] = KeepLookingForNeighbor(row, column - 1, seatLayout, 6);
				neighbors[7] = KeepLookingForNeighbor(row - 1, column - 1, seatLayout, 7);
			}
			else if (column == 0)
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[1] = KeepLookingForNeighbor(row - 1, column + 1, seatLayout, 1);
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
				neighbors[3] = KeepLookingForNeighbor(row + 1, column + 1, seatLayout, 3);
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
			}
			else if (column == maxCol)
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
				neighbors[5] = KeepLookingForNeighbor(row + 1, column - 1, seatLayout, 5);
				neighbors[6] = KeepLookingForNeighbor(row, column - 1, seatLayout, 6);
				neighbors[7] = KeepLookingForNeighbor(row - 1, column - 1, seatLayout, 7);
			}
			else
			{
				neighbors[0] = KeepLookingForNeighbor(row - 1, column, seatLayout, 0);
				neighbors[1] = KeepLookingForNeighbor(row - 1, column + 1, seatLayout, 1);
				neighbors[2] = KeepLookingForNeighbor(row, column + 1, seatLayout, 2);
				neighbors[3] = KeepLookingForNeighbor(row + 1, column + 1, seatLayout, 3);
				neighbors[4] = KeepLookingForNeighbor(row + 1, column, seatLayout, 4);
				neighbors[5] = KeepLookingForNeighbor(row + 1, column - 1, seatLayout, 5);
				neighbors[6] = KeepLookingForNeighbor(row, column - 1, seatLayout, 6);
				neighbors[7] = KeepLookingForNeighbor(row - 1, column - 1, seatLayout, 7);

			}


			return neighbors;
		}

		private SeatState[] getNeighborsPart1(int row, int column, SeatState[,] seatLayout)
		{
			SeatState[] neighbors = new SeatState[8];

			if (row == 0 && column == 0)
			{
				neighbors[2] = seatLayout[row, column + 1];
				neighbors[3] = seatLayout[row + 1, column + 1];
				neighbors[4] = seatLayout[row + 1, column];
			}
			else if (row == 0 && column == maxCol)
			{
				neighbors[4] = seatLayout[row + 1, column];
				neighbors[5] = seatLayout[row + 1, column - 1];
				neighbors[6] = seatLayout[row, column - 1];
			}
			else if (row == max && column == 0)
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[1] = seatLayout[row - 1, column + 1];
				neighbors[2] = seatLayout[row, column + 1];
			}
			else if (row == max && column == maxCol)
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[6] = seatLayout[row, column - 1];
				neighbors[7] = seatLayout[row - 1, column - 1];
			}
			else if (row == 0)
			{
				neighbors[2] = seatLayout[row, column + 1];
				neighbors[3] = seatLayout[row + 1, column + 1];
				neighbors[4] = seatLayout[row + 1, column];
				neighbors[5] = seatLayout[row + 1, column - 1];
				neighbors[6] = seatLayout[row, column - 1];
			}
			else if (row == max)
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[1] = seatLayout[row - 1, column + 1];
				neighbors[2] = seatLayout[row, column + 1];
				neighbors[6] = seatLayout[row, column - 1];
				neighbors[7] = seatLayout[row - 1, column - 1];
			}
			else if (column == 0)
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[1] = seatLayout[row - 1, column + 1];
				neighbors[2] = seatLayout[row, column + 1];
				neighbors[3] = seatLayout[row + 1, column + 1];
				neighbors[4] = seatLayout[row + 1, column];
			}
			else if (column == maxCol)
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[4] = seatLayout[row + 1, column];
				neighbors[5] = seatLayout[row + 1, column - 1];
				neighbors[6] = seatLayout[row, column - 1];
				neighbors[7] = seatLayout[row - 1, column - 1];
			}
			else
			{
				neighbors[0] = seatLayout[row - 1, column];
				neighbors[1] = seatLayout[row - 1, column + 1];
				neighbors[2] = seatLayout[row, column + 1];
				neighbors[3] = seatLayout[row + 1, column + 1];
				neighbors[4] = seatLayout[row + 1, column];
				neighbors[5] = seatLayout[row + 1, column - 1];
				neighbors[6] = seatLayout[row, column - 1];
				neighbors[7] = seatLayout[row - 1, column - 1];

			}

			//set what you can others default to NA
			//try	{ neighbors[0] = seatLayout[row - 1, column]; }	catch { }
			//try	{ neighbors[1] = seatLayout[row - 1, column + 1]; } catch { }
			//try { neighbors[2] = seatLayout[row, column + 1]; } catch { }
			//try { neighbors[3] = seatLayout[row + 1, column + 1]; } catch { }
			//try { neighbors[4] = seatLayout[row + 1, column]; } catch { }
			//try { neighbors[5] = seatLayout[row + 1, column - 1]; } catch { }
			//try { neighbors[6] = seatLayout[row, column - 1]; } catch { }
			//try { neighbors[7] = seatLayout[row - 1, column - 1]; }	catch { }
			return neighbors;
		}

		private SeatState GetSeatFromChar(char c)
		{
			switch (c)
			{
				case 'L':
					return SeatState.Empty;
				case '#':
					return SeatState.Occupied;
				case '.':
					return SeatState.Floor;
				default:
					throw new Exception("nope");

			}
		}

		private char GetCharFromSeat(SeatState c)
		{
			switch (c)
			{
				case SeatState.Empty:
					return 'L';
				case SeatState.Occupied:
					return '#';
				case SeatState.Floor:
					return '.';
				default:
					return ' ';

			}
		}

		public enum SeatState
		{
			Floor,
			Empty,
			Occupied
		}


		private void Part1(SeatState[,] seatLayoutState)
		{
			var newState = seatLayoutState.Clone() as SeatState[,];
			changed = false;

			//foreach seat run a round
			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					var seat = seatLayoutState[row, column];

					if (seat == SeatState.Floor || cache[row, column] == true)
					{
						continue;
					}
					//checked cache
					var neighbors = getNeighbors(row, column, seatLayoutState);
					//can cache 1 time
					if (neighbors.Count(x => x == SeatState.Floor) > 4)
					{
						cache[row, column] = true;
					}


					//rules
					if (seat == SeatState.Empty && !neighbors.Contains(SeatState.Occupied))
					{
						newState[row, column] = SeatState.Occupied;
						changed = true;
					}
					else if (seat == SeatState.Occupied && neighbors.Count(x => x == SeatState.Occupied) >= 4)
					{
						newState[row, column] = SeatState.Empty;
						changed = true;
					}

				}
			}
			//printState(newState, round);
			Console.WriteLine(++round);

			//compare new state to old state if same get number of occupied
			if (changed == false)
			{
				occupied = GetOccupiedCount(newState);
				return;
			}

			Part1(newState);
		}


	}
}