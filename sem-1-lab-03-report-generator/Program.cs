namespace sem_1_lab_03_report_generator;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Начинаем вести отчет");
        string name_ivent = Name_Ivent();
    }

    static string Name_Ivent()
    {
        Console.Write("Название события");
        string name = Console.ReadLine();

        if (name.Trim() == "")
        {
            return "Нет названия события";
        }
        return "Есть название события";
    }

    static string Date_Ivent()
    {
        
    }

    static string Count_players()
    {
        
    }

    static string Winner()
    {
        
    }

    static string Length()
    {
        
    }

    static string Prize_Fund()
    {
        
    }

    
}
