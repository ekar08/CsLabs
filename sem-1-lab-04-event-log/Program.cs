using System.ComponentModel.DataAnnotations;

namespace sem_1_lab_04_event_log;

class Program
{
    struct LogEntry()
    {
        //выбираю структуру, потому что не собираюсь менять оригинальные логи
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;
    }
    
    static void Main()
    {
        string[] server_log = File.ReadAllLines("../../../event_server.log");

        foreach (string log in server_log)
        {
            Console.WriteLine(log);
        }
        
    }

    static string[] ParseLog(string[] lines)
    {
        return [];
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
