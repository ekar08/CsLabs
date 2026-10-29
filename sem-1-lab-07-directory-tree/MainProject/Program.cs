using System;
using System.IO;

internal class Program
{
    // Current line in file
    private static int _lineIndex;

    // Current nesting level
    private static int _depth;

    // File content
    private static string[] _lines = null!;

    private static void Main()
    {
        _lines = File.ReadAllLines("../../../tree.txt");

        Console.WriteLine(_lines[0]);

        _lineIndex = 1;

        PrintTree();

        Console.ReadLine();
    }

    private static void PrintTree()
    {
        while (_lineIndex < _lines.Length)
        {
            var line = _lines[_lineIndex].Trim();

            if (line == "")
            {
                _lineIndex += 2;
                continue;
            }

            if (line == "END")
            {
                _depth--;

                _lineIndex++;

                return;
            }

            if (line.StartsWith("BEGIN "))
            {
                var name = line.Substring(5);

                Console.Write(GetIndent());
                Console.WriteLine("├─" + name);
                _lineIndex++;
                _depth++;

                PrintTree();
            }

            _lineIndex++;
        }
    }

    private static string GetIndent()
    {
        var result = "";

        for (var i = 0; i < _depth; i++)
        {
            result += "│ ";
        }

        return result;
    }
}