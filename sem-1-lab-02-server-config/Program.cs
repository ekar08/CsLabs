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

        int max_pl = 50;
        int min_pl_solo = 3;
        int min_pl_team = 4;
        int count_pl_duel = 2;

        Console.Write("\nКол-во игроков, желающих зайти на сервер: ");
        string tmp_count_pl = Console.ReadLine();
        int count_pl = Convert.ToInt32(tmp_count_pl);

        string result = "";
        
        if (tmp_game_mode == "1")
        {
            result = Solo_mode(count_pl);
        } 
        Console.WriteLine(result);
    }

    public static string Solo_mode(int buff_count_pl)
    {
        string result_solo="";
        int max_pl = 50;
        int min_pl_solo = 3;

        if (buff_count_pl<min_pl_solo)
            result_solo = "Сервер запустить невозможно, слишком мало игроков, подождите еще";
        else if (buff_count_pl>max_pl)
            result_solo = "Слишком много игроков, часть из них уйдет в ожидание след раунда\nВведите код админа";
        else
            result_solo = "Сервер можно запускать";

        return result_solo;
    }

}
