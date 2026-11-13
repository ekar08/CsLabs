using System.Net.NetworkInformation;
using System.Reflection.Metadata;

namespace MainProject;

class Program
{
    

    class Map()
    {
        public int _width;
        public int _height;
        public char[,] _scheme; 
    }

    static void Main()
    {
        string path_to_map = "map.txt";
        LoadMap(path_to_map);

        Map My_Map = LoadMap(path_to_map);
        ShowMap(My_Map);

        Map My_New_Map = CreateMap();


        ShowMap(My_New_Map);
    }

    static Map LoadMap(string path)
    {
        string[] file = File.ReadAllLines($"../../../{path}");
        string[] hw = file[0].Split("|");

        Map tempMap = new Map();

        
        tempMap._width = Convert.ToInt32(hw[0]);
        tempMap._height = Convert.ToInt32(hw[1]);

        char[,] shem = new char[tempMap._height,tempMap._width];

        for (int i=1; i<file.Length; i++)
        {
            string current = file[i];
            for (int j=0; j<current.Length; j++)
            {
                shem[i-1,j] = current[j];
            }
        }

        tempMap._scheme = shem;
        return tempMap;
    }

    static void ShowMap(Map tempMap)
    {
        char[,] shem = tempMap._scheme;

        for (int i=0; i<tempMap._height; i++)
        {
            for (int j=0;j<tempMap._width; j++)
            {
                Console.Write(shem[i,j]);
            }
            Console.WriteLine();
        }
    }

    static Map CreateMap()
    {
        Map New_Map = new Map();

        Console.Write("Введите высоту новой карты: ");
        New_Map._height = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите ширину новой карты: ");
        New_Map._width = Convert.ToInt32(Console.ReadLine());

        char [,] newShem = new char[New_Map._height,New_Map._width];

        for (int i=0; i<New_Map._height; i++)
        {
            string currentString = Console.ReadLine();

            for (int j=0; j<New_Map._width; j++)
            {
                newShem[i,j] = currentString[j];
            }
        }

        New_Map._scheme = newShem;

        return New_Map;
    }

}
