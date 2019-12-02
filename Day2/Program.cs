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

        static async Task Main(string[] args)
        {
            var program = new IntCodeProgram(await ReadProgramAsync());
            //var program = new IntCodeProgram(new List<int>{ 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 });
            while (!program.Finished)
            {
                program.Process();
            }
            Console.WriteLine(program);
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

        public IntCodeProgram(List<int> tape)
        {
            _tape = tape;
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
