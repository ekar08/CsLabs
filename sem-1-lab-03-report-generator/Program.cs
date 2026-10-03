namespace sem_1_lab_03_report_generator;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Начинаем вести отчет");
        string name_ivent = Name_Ivent();
        DateTime date_ivent = Date_Ivent();
        string count_players = Count_players();
        string winner = Winner();
        string length = Length();
        string prize_fund = Prize_Fund();

        Console.WriteLine(name_ivent);
        Console.WriteLine(date_ivent);
        Console.WriteLine(count_players);
        Console.WriteLine(winner);
        Console.WriteLine(length);
        Console.WriteLine(count_players);
        Console.WriteLine(prize_fund);
    }

    static string Name_Ivent()
    {
        Console.Write("Введите название события: ");
        string name_ivent = Console.ReadLine();

        if (name_ivent.Trim() == "")
        {
            return "Нет названия события";
        }
        return $"Событие называется «{name_ivent}»";
    }

    static DateTime Date_Ivent()
    {
        Console.WriteLine("Заполним дату проведения события.");
        Console.Write("Введите дату (DD.MM.YY): ");
        string date_DMY = Console.ReadLine();

        Console.Write("Введите время (hh:mm:ss): ");
        string date_hms = Console.ReadLine();

        DateTime date_all = Convert.ToDateTime(date_DMY+" "+date_hms);

        return date_all;
    }

    static string Count_players()
    {
        Console.Write("Введите количество игроков: ");
        string count_pl = Console.ReadLine();

        if (count_pl.Trim() == "")
        {
            return "Неизвестно сколько участвовало игроков";
        }

        return $"Участвовало {count_pl} игроков";
    }

    static string Winner()
    {
        Console.Write("Введите имя победителя: ");
        string winner = Console.ReadLine();

        if (winner.Trim() == "")
        {
            return "Неизвестно кто стал победителем";
        }

        return $"Победил {winner}";
    }

    static string Length()
    {
        return "0";
    }

    static string Prize_Fund()
    {
        Console.Write("Введите призовой фонд: ");
        string prize_fund = Console.ReadLine();

        if (prize_fund.Trim() == "")
        {
            return "Неизвестно сколько составлял призовой фонд";
        }
        return $"Призовой фонд составил {prize_fund}";
    }

}
