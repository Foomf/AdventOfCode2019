using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        private const string InputFileName = "input.txt";
        private const int TargetOutput = 19690720;

        static async Task Main(string[] args)
        {
            var tape = await ReadProgramAsync();
            for (var noun = 0; noun <= 99; ++noun)
            {
                for (var verb = 0; verb <= 99; ++verb)
                {
                    if (Run(tape, noun, verb) == TargetOutput)
                    {
                        var value = 100 * noun + verb;
                        Console.WriteLine($"The value is {value}");
                    }
                }
            }
        }

        public static int Run(IEnumerable<int> tape, int noun, int verb)
        {
            var program = new IntCodeProgram(tape);
            program[1] = noun;
            program[2] = verb;
            while (!program.Finished)
            {
                program.Process();
            }

            return program[0];
        }

        static async Task<List<int>> ReadProgramAsync()
        {
            var text = await File.ReadAllTextAsync(InputFileName);
            var values = text.Split(',');
            return values.Select(int.Parse).ToList();
        }
    }

    class IntCodeProgram
    {
        private List<int> _tape;
        private int _position = 0;

        public bool Finished { get; private set; } = false;

        public IntCodeProgram(IEnumerable<int> tape)
        {
            _tape = new List<int>(tape);
        }

        public int this[int idx]
        {
            get => _tape[idx];
            set => _tape[idx] = value;
        }

        public void Process()
        {
            var opCode = _tape[_position++];
            switch (opCode)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Multiply();
                    break;
                case 99:
                    Finished = true;
                    break;
                default:
                    Console.WriteLine($"Unknown opcode {opCode}");
                    Finished = true;
                    break;
            }
        }

        private void Add()
        {
            var v1Ptr = _tape[_position++];
            var v2Ptr = _tape[_position++];
            var outPtr = _tape[_position++];
            var result = _tape[v1Ptr] + _tape[v2Ptr];
            _tape[outPtr] = result;
        }

        private void Multiply()
        {
            var v1Ptr = _tape[_position++];
            var v2Ptr = _tape[_position++];
            var outPtr = _tape[_position++];
            var result = _tape[v1Ptr] * _tape[v2Ptr];
            _tape[outPtr] = result;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var code in _tape)
            {
                builder.Append(code).Append(", ");
            }

            if (_tape.Any())
            {
                builder.Length -= 2;
            }

            return builder.ToString();
        }
    }
}
