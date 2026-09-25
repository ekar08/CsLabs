using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace sem_1_lab_02_server_config;

class Program
{
    public static void Main()
    {
        Console.Write("Начинаем анализ сервера для запуска...\nВведите пароль администратора для продолжения действий (придумайте): ");
        string password_admin = Console.ReadLine();

        Console.Write("\nРежимы игры:\n#1 Сам за себя\n#2 Командный бой\n#3 Дуэль\nВыберите режим игры: ");
        string tmp_game_mode = Console.ReadLine();

        Console.Write("\nКол-во игроков, желающих зайти на сервер: ");
        string tmp_count_pl = Console.ReadLine();
        int count_pl = Convert.ToInt32(tmp_count_pl);

        string result = "";
        
        if (tmp_game_mode == "1")
        {
            result = Solo_mode(count_pl);
        } 
        if (tmp_game_mode == "2")
        {
            result = Team_mode(count_pl);
        }
        if (tmp_game_mode == "3")
        {
            result = Duel_mode(count_pl);
        }  

        Console.WriteLine(result);
    }

    public static string Solo_mode(int buff_count_pl)
    {
        string result_solo="";
        int max_pl = 50;
        int min_pl_solo = 3;

        if (buff_count_pl<min_pl_solo)
        {
            result_solo = "Слишком мало игроков, подождите еще\n###Запуск невозможен";
        }
        else if (buff_count_pl>max_pl)
        {
            result_solo = "Слишком много игроков, часть из них уйдет в ожидание след раунда\n###Сервер запущен с предупреждением";
        }
        else
        {
            result_solo = "###Сервер готов к запуску";
        }
        return result_solo;
    }

    public static string Team_mode(int buff_count_pl)
    {
        string result_team="";
        int max_pl = 50;
        int min_pl_in_team = 2;
        
        Console.Write("Кол-во команд: ");
        string tmp_count_team = Console.ReadLine();
        int count_team = Convert.ToInt32(tmp_count_team);

        Console.Write("Игроков в команде: ");
        string tmp_pl_in_team = Console.ReadLine();
        int pl_in_team = Convert.ToInt32(tmp_pl_in_team);

        if (buff_count_pl < min_pl_in_team)
        {
            result_team = "Слишком мало игроков, подождите еще\n###Запуск невозможен";
        }
        else if (buff_count_pl>max_pl)
        {
            result_team = "Слишком много игроков, часть из них уйдет в ожидание след раунда\n###Сервер запущен с предупреждением";
        }
        
        do
        {
            if (pl_in_team<min_pl_in_team)
            {
                Console.Write("В команде слишком мало игроков, измените параметры\nИгроков в команде: ");
                tmp_pl_in_team = Console.ReadLine();
                pl_in_team = Convert.ToInt32(tmp_pl_in_team);
            }
        } 
        while (pl_in_team<min_pl_in_team);

        if (count_team*pl_in_team!=buff_count_pl)
        {
            result_team = "Не все игроки распределены по командам, часть из них уйдет в ожидание след раунда\n###Сервер запущен с предупреждением";
        }
        else
        {
            result_team = "###Сервер готов к запуску";
        }

        return result_team;
    }

    public static string Duel_mode(int buff_count_pl)
    {
        string result_duel="";
        int max_pl = 50;
        int pl_duel = 2;
        int min_pl_duel = 2;

        if (buff_count_pl<min_pl_duel)
        {
            result_duel = "Слишком мало игроков, подождите еще\n###Запуск невозможен";
        }
        else if (buff_count_pl>max_pl || buff_count_pl%pl_duel!=0)
        {
            result_duel = "Слишком много игроков, часть из них уйдет в ожидание след раунда\n###Сервер запущен с предупреждением";
        }
        else
        {
            result_duel = "###Сервер готов к запуску";
        }
        return result_duel;
    }


}
