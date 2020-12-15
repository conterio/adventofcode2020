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

		public _11()
		{
			
			lines = File.ReadAllLines("11.txt").ToList();

			var rows = lines.Count;
			var columns = lines[0].Length;

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

			Part1(seatlayout);


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
					else if (seat == SeatState.Occupied  && neighbors.Count(x => x == SeatState.Occupied) >= 4)
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

		private bool CompareStates(SeatState[,] oldState, SeatState[,] newState)
		{
			for (int row = 0; row < lines.Count; ++row)
			{
				for (int column = 0; column < lines[row].Length; ++column)
				{
					if (oldState[row, column] != newState[row, column])
					{
						return false;
					}
				}
			}

			return true;
		}

		private SeatState[] getNeighbors(int row, int column, SeatState[,] seatLayout)
		{
			SeatState[] neighbors = new SeatState[8];
			//set what you can others default to NA
			try	{ neighbors[0] = seatLayout[row - 1, column]; }	catch { }
			try	{neighbors[1] = seatLayout[row - 1, column + 1]; } catch { }
			try { neighbors[2] = seatLayout[row, column + 1]; } catch { }
			try { neighbors[3] = seatLayout[row + 1, column + 1]; } catch { }
			try { neighbors[4] = seatLayout[row + 1, column]; } catch { }
			try { neighbors[5] = seatLayout[row + 1, column - 1]; } catch { }
			try { neighbors[6] = seatLayout[row, column - 1]; } catch { }
			try { neighbors[7] = seatLayout[row - 1, column - 1]; }	catch { }
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


	}
}