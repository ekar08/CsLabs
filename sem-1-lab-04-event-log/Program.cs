using System.ComponentModel.DataAnnotations;

namespace sem_1_lab_04_event_log;

class Program
{
    //превращает записи типа LogEntry обратно в string (для проверки работы)
    static string LogEntry_to_String(LogEntry log)
    {
        string result = $"{log.Timestamp} [{log.Level}][{log.Category}] {log.Message}";

        return result;
    }

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

        LogEntry[] Test = ParseLog(server_log);

        foreach (LogEntry log in Test)
        {
            Console.WriteLine(LogEntry_to_String(log));
        }
    }

    static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] logEntries_array = new LogEntry[lines.Length];

        for (int j=0; j<lines.Length; j++)
        {
            string log = lines[j];
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

                if (log[i]=='[') // записываем индекс открытой скобки
                {
                    ind_square_start = i;
                    count_squarebr++;
                }

                if (count_squarebr == 1 && log[i]==']')
                {
                    logEntry.Level = log.Substring(ind_square_start+1,i-ind_square_start-1);
                }

                if (count_squarebr == 2 && log[i]==']')
                {
                    logEntry.Category = log.Substring(ind_square_start+1,i-ind_square_start-1);
                }

                if (count_squarebr == 2 && count_space == 3 && log[i]==' ')
                {
                    logEntry.Message = log.Substring(i+1,log.Length-i-1);
                    
                }
            }
            
            logEntries_array[j] = logEntry;

        }
        return logEntries_array;
    }

    static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        List<LogEntry> filter_list = new List<LogEntry>();

        foreach (LogEntry log in entries)
            if(log.Timestamp.Date == date.Date)
                filter_list.Add(log);

        LogEntry[] filter_result = filter_list.ToArray();
            
        return filter_result;
    }

    static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
    {
        List<LogEntry> filter_list = new List<LogEntry>();

        foreach (LogEntry log in entries)
            if(log.Level == level)
                filter_list.Add(log);

        LogEntry[] filter_result = filter_list.ToArray();
            
        return filter_result;
    }

    static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
    {
        List<LogEntry> filter_list = new List<LogEntry>();

        foreach (LogEntry log in entries)
            if(log.Category == category)
                filter_list.Add(log);

        LogEntry[] filter_result = filter_list.ToArray();
            
        return filter_result;
    }

    static LogEntry[] Search(LogEntry[] entries, string text)
    {
        List<LogEntry> filter_list = new List<LogEntry>();

        foreach (LogEntry log in entries)
        {
            string message = log.Message.ToLower();
            string text_low = text.ToLower();
            if (message.Contains(text_low))
                filter_list.Add(log);
        }
        LogEntry[] filter_result = filter_list.ToArray();
            
        return filter_result;
    }

    static int CountByLevel(LogEntry[] entries, string level)
    {
        LogEntry[] filter_level = FilterByLevel(entries,level);
        int count_levels = filter_level.Length;
        return count_levels;
    }

    static string GetServerStatus(LogEntry[] entries)
    {
        int error_count = CountByLevel(entries,"Error");
        int fatal_count = CountByLevel(FilterByCategory(entries,"Server"),"Fatal");

        string server_status = "";
        if (error_count==0 && fatal_count==0)
            server_status = "Сервер работает штатно";
        
        if (error_count!=0 && fatal_count==0)
            server_status = "Есть ошибки: требуется проверка";

        if (fatal_count!=0)
            server_status = "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
        
        return server_status;
    }
}
