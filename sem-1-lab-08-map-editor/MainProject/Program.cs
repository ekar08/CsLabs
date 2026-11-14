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
        public Player _player;
    }

    struct Player
    {
        public Position _pos;
    }

    struct Position
    {
        public int X;
        public int Y;
    }

    static void Main()
    {
        string path_to_map = "save-01.txt";

        Map MyMap = new Map();
        try
        {
            MyMap = LoadMap(path_to_map);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }

        ShowMap(MyMap);
    }

    static Map LoadMap(string path)
    {   
        string ful_path = $"../../../maps/{path}";

        if (File.Exists(ful_path) == false)
        {
            throw new FileNotFoundException("Файл не найден");
        }

        string[] file = File.ReadAllLines(ful_path);
        
        string[] hw = file[0].Split("|");

        Map tempMap = new Map();

        tempMap._player = LoadPlayerPos(tempMap);
        tempMap._width = Convert.ToInt32(hw[0]);
        tempMap._height = Convert.ToInt32(hw[1]);

        char[,] shem = new char[tempMap._height,tempMap._width];

        for (int i=1; i<tempMap._height+1; i++)
        {
            string current = file[i];
            for (int j=0; j<tempMap._width; j++)
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

// возможно чтобы сохранять это разделение на бизнес/вывод нужно сделать один метод, который будет предлагать делать ввод, а другой уже с введенными данными работать
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
 
    static Player LoadPlayerPos(Map tempMap)
    {
        Player tempPlayer = new Player();

        for (int i=0; i<tempMap._height; i++)
        {
            for (int j=0;j<tempMap._width; j++)
            {
                if (tempMap._scheme[i, j] == 'P')
                {
                    tempPlayer._pos.X = j+1;
                    tempPlayer._pos.Y = i+1;
                }
            }
        }

        return tempPlayer;
    }


// добавить исключение на случай выход за координаты
    static void MovePlayer(Map tempMap)
    {
        Player tempPlayer = LoadPlayerPos(tempMap);
        DeletePlayer(tempMap);

        Console.Write("Введите новые координаты игрока:\nX:");
        tempPlayer._pos.X = Convert.ToInt32(Console.ReadLine());
        Console.Write("Y:");
        tempPlayer._pos.Y = Convert.ToInt32(Console.ReadLine());

        while (tempMap._scheme[tempPlayer._pos.Y-1,tempPlayer._pos.X-1] == 'W' || tempMap._scheme[tempPlayer._pos.Y-1,tempPlayer._pos.X-1] == '#')
        {
            Console.WriteLine($"Игрок не может переместиться в \"{tempMap._scheme[tempPlayer._pos.Y-1,tempPlayer._pos.X-1]}\"");

            Console.Write("Введите новые координаты игрока:\nX:");
            tempPlayer._pos.X = Convert.ToInt32(Console.ReadLine());
            Console.Write("Y:");
            tempPlayer._pos.Y = Convert.ToInt32(Console.ReadLine());
        }

        tempMap._scheme[tempPlayer._pos.Y-1,tempPlayer._pos.X-1] = 'P';
    }

    static void DeletePlayer(Map tempMap)
    {
        tempMap._scheme[tempMap._player._pos.Y-1,tempMap._player._pos.X-1] = '.';
    }


}

