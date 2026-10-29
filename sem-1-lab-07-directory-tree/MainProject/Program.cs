using System;
using System.IO;

namespace lab07;
public class Program
{
    // Current line in file
    public static int _lineIndex;

    // Current nesting level
    public static int _depth;

    // File content
    public static string[] _lines = null!;

    public static void Main()
    {
        _lines = File.ReadAllLines("../../../tree.txt");

        Console.WriteLine(_lines[0]);

        _lineIndex = 1;

        PrintTree();

        Console.ReadLine();
    }

    public static void PrintTree()
    {
        while (_lineIndex < _lines.Length)
        {
            var line = _lines[_lineIndex].Trim();

            //обрабатывает пустые строки в файле, полезно
            if (line == "")
            {
                _lineIndex += 1;
                continue;
            }

            if (line == "END")
            {
                _depth--;

                _lineIndex++;
            }

            if (line.StartsWith("BEGIN "))
            {
                var name = line.Substring(6);

                Console.Write(GetIndent());
                Console.WriteLine("├─" + name);
                _lineIndex++;
                _depth++;
            }
        }
    }

    public static string GetIndent()
    {
        var result = "";

        for (var i = 0; i < _depth; i++)
        {
            result += "│ ";
        }

        return result;
    }
}