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
    
    
}
