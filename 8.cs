using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	class _8
	{
		private int changeThisNumber = 1;
		private static Tuple<Instruction, int> previousTupleChange;
		private static int previousNumChange;

		private Dictionary<int, Tuple<Instruction, int>> instructions = new Dictionary<int, Tuple<Instruction, int>>();
		private List<int> instructionsRun = new List<int>();

		public _8()
		{
			var lines = File.ReadAllLines("m.txt").ToList();

			int lineNumber = 1;
			foreach (var line in lines)
			{
				var instruction = line.Split(" ");
				var a = int.Parse(instruction[1]);
				instructions.Add(lineNumber, new Tuple<Instruction, int>(setInstuction(instruction[0]), int.Parse(instruction[1])));
				++lineNumber;
			}

			Console.WriteLine(Part2());
		}

		public int Part2()
		{
			int accumulator = 0;
			int programCounter = 1;

			while (true)
			{
				Instruction action = instructions[programCounter].Item1;
				int value = instructions[programCounter].Item2;
				
				if (instructionsRun.Contains(programCounter))
				{
					//change back previous
					if (previousTupleChange != null)
					{
						instructions[previousNumChange] = previousTupleChange;
					}

					//increment until you run into a nop or jump
					while (instructions[changeThisNumber].Item1 == Instruction.Acc)
					{
						++changeThisNumber;
					}

					//save state for next loop
					previousTupleChange = instructions[changeThisNumber];
					previousNumChange = changeThisNumber;

					//update next line or change number
					if (instructions[changeThisNumber].Item1 == Instruction.Nop)
					{
						instructions[changeThisNumber] = new Tuple<Instruction, int>(Instruction.Jmp, instructions[changeThisNumber].Item2);
					}
					else if (instructions[changeThisNumber].Item1 == Instruction.Jmp)
					{
						instructions[changeThisNumber] = new Tuple<Instruction, int>(Instruction.Nop, instructions[changeThisNumber].Item2);
					}
					else
					{
						return accumulator;
					}

					//reset vars, increment, and go again
					instructionsRun = new List<int>();
					++changeThisNumber;
					Part2();
				}

				instructionsRun.Add(programCounter);

				switch (action)
				{
					case Instruction.Nop:
						programCounter += 1;
						break;
					case Instruction.Acc:
						programCounter += 1;
						accumulator += value;
						break;
					case Instruction.Jmp:
						programCounter += value;
						break;
					case Instruction.End:
						return accumulator;
				}

			}

			throw new Exception("should never reach this");
		}

		public enum Instruction
		{
			Nop,
			Acc,
			Jmp,
			End
		}

		private Instruction setInstuction(string ins)
		{
			if (ins == "nop")
				return Instruction.Nop;
			if (ins == "acc")
				return Instruction.Acc;
			if (ins == "jmp")
				return Instruction.Jmp;
			if (ins == "end")
				return Instruction.End;

			throw new Exception($"no Instruction found for that {ins}");
		}

	}
}
