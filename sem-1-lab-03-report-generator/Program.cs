namespace sem_1_lab_03_report_generator;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Начинаем вести отчет");
        string name_ivent = Name_Ivent();
        string date_ivent = Date_Ivent();
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
        Console.Write("Название события: ");
        string name_ivent = Console.ReadLine();

        if (name_ivent.Trim() == "")
        {
            return "Нет названия события";
        }
        return $"Событие называется «{name_ivent}»";
    }

    static string Date_Ivent()
    {
        return "0";
    }

    static string Count_players()
    {
        Console.Write("Количество игроков: ");
        string count_pl = Console.ReadLine();

        if (count_pl.Trim() == "")
        {
            return "Неизвестно сколько участвовало игроков";
        }

        return $"Участвовало {count_pl} игроков";
    }

    static string Winner()
    {
        Console.Write("Победителем стал: ");
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
        Console.Write("Призовой фонд: ");
        string prize_fund = Console.ReadLine();

        if (prize_fund.Trim() == "")
        {
            return "Неизвестно сколько составлял призовой фонд";
        }
        return $"Призовой фонд составил {prize_fund}";
    }

    
}
