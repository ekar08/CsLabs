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
        LoadMap(path_to_map);

        Map My_Map = LoadMap(path_to_map);
        ShowMap(My_Map);

        //Map My_New_Map = CreateMap();


        //ShowMap(My_New_Map);

        Player My_Player = LoadPlayerPos(My_Map);
        Console.WriteLine($"{My_Player._pos.X}, {My_Player._pos.Y}");


        MovePlayer(My_Map);
        ShowMap(My_Map);
    }

    static Map LoadMap(string path)
    {
        string[] file = File.ReadAllLines($"../../../maps/{path}");
        string[] hw = file[0].Split("|");

        Map tempMap = new Map();

        tempMap._player = LoadPlayerPos(tempMap);
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
        DeletePlayer(tempMap, tempPlayer);

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

    static void DeletePlayer(Map tempMap, Player tempPlayer)
    {
        tempMap._scheme[tempPlayer._pos.Y-1,tempPlayer._pos.X-1] = '.';
    }


}

