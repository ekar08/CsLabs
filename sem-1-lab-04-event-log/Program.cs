using System.ComponentModel.DataAnnotations;

namespace sem_1_lab_04_event_log;

class Program
{
    struct LogEntry()
    {
        //выбираю структуру, потому что потом мне нужны будут результаты фильтрации, а ориг логи должны сохраниться
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;
    }

    
    static void Main()
    {
        string[] server_log = File.ReadAllLines("../../../event_server.log");

        LogEntry[] logEntries_array = new LogEntry[server_log.Length];
        
        // заполняю LogEntry переменными
        for (int j=0; j<server_log.Length; j++)
        {
            string log = server_log[j];
            LogEntry logEntry = new LogEntry();

            int count_space = 0;
            int count_squarebr = 0;
            int ind_square_start = 0;//индекс открытой квадратной скобки
            for (int i=0; i<log.Length; i++)
            {
                if (log[i]==' ') // считаем пробелы
                {
                    count_space++;
                }
                
                logEntry.Timestamp = Convert.ToDateTime(log.Substring(0,23));

                if (log[i]=='[') // записываем индкс открытой скобки
                {
                    ind_square_start = i;
                    count_squarebr++;
                }

                if (count_squarebr == 1 && log[i]==']')
                {
                    logEntry.Level = log.Substring(ind_square_start,i-ind_square_start+1);
                }

                if (count_squarebr == 2 && log[i]==']')
                {
                    logEntry.Category = log.Substring(ind_square_start,i-ind_square_start+1);
                }

                if (count_squarebr == 2 && count_space == 3 && log[i]==' ')
                {
                    logEntry.Message = log.Substring(i+1,log.Length-i-1);
                    
                }
            }
            
            logEntries_array[j] = logEntry;
        }

        foreach (string log in server_log)
            Console.WriteLine(log);
         
        DateTime test_date = Convert.ToDateTime("2026-09-01 12:10:01.101"); 
        
        string[] ByDate = FilterByDate(logEntries_array, test_date);
    }

    static string[] ParseLog(string[] lines)
    {
        return lines;
    }

    static string[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        
        return [];
    }

    static string[] FilterByLevel(LogEntry[] entries, string level)
    {
        return [];
    }

    static string[] FilterByCategory(LogEntry[] entries, string category)
    {
        return [];
    }

    static string[] Search(LogEntry[] entries, string text)
    {
        return [];
    }

    static int CountByLevel(LogEntry[] entries, string level)
    {
        return 0;
    }

    static string GetServerStatus(LogEntry[] entries)
    {
        return "0";
    }
}
