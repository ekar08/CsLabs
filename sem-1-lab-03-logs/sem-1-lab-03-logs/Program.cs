namespace sem_1_lab_03_logs;

class Program
{
    
    static void Main()
    {
        string[] server_log = File.ReadAllLines("../../../event_server.log");

        string[] server_log_ivent = Event_Continue(server_log);
        DateTime main_dateTime_start = Convert.ToDateTime("01.01.01");
        string main_winner = "";
        string main_reward = "";
        string main_loot = "";
        string main_reward_sad = "";
        int count_warning = 0;
        int count_errors = 0;

        for (int j=0; j<server_log_ivent.Length; j++)
        {
            string log = server_log_ivent[j];

            DateTime dateTime = Convert.ToDateTime("01.01.01");
            string level = "";
            string category = "";
            string message = "";

            int count_space = 0;
            int count_squarebr = 0;
            int ind_square_start = 0;//индекс открытой квадратной скобки
            

            for (int i=0; i<log.Length; i++)
            {
                if (log[i]==' ') // считаем пробелы
                {
                    count_space++;
                }
                
                dateTime = Convert.ToDateTime(log.Substring(0,23));

                if (log[i]=='[') // записываем индекс открытой скобки
                {
                    ind_square_start = i;
                    count_squarebr++;
                }

                if (count_squarebr == 1 && log[i]==']')
                {
                    level = log.Substring(ind_square_start+1,i-ind_square_start-1);
                }

                if (count_squarebr == 2 && log[i]==']')
                {
                    category = log.Substring(ind_square_start+1,i-ind_square_start-1);
                }

                if (count_squarebr == 2 && count_space == 3 && log[i]==' ')
                {
                    message = log.Substring(i+1,log.Length-i-1);
                    
                }
            }

            //дата
            if (log.Contains("Событие началось:"))
            {
                main_dateTime_start = dateTime;
            }

            //победитель
            if (log.Contains("объявлены победителями события") && category == "Reward")
            {
                main_winner = message.Substring(0,message.IndexOf("объявлены победителями события")-1);
            }

            //очки победителя
            if (log.Contains($"{main_winner} получили") && log.Contains("очков события") && category == "Reward" && !log.Contains("утешитель"))
            {
                int ind_start = message.IndexOf("получили")+9;
                int ind_end = message.IndexOf("очков события")-1;
                main_reward = message.Substring(ind_start,ind_end-ind_start);
            }

            //ивентовый предмет
            if (log.Contains($"{main_winner} получили ивентовый предмет:") && category == "Loot")
            {
                int ind_start = message.IndexOf("получили ивентовый предмет:")+28;
                int ind_end = message.Length;
                main_loot = message.Substring(ind_start,ind_end-ind_start);
            }

            //утешительная награда
            if (log.Contains($"{main_winner} получили") && log.Contains("очков события") && category == "Reward" && log.Contains("утешитель"))
            {
                int ind_start = message.IndexOf("утешительную награду:")+22;
                int ind_end = message.Length;
                main_reward_sad = message.Substring(ind_start,ind_end-ind_start);
            }

            //предупреждения
            if (level == "Warning")
            {
                count_warning++;
            }

            //ошибки
            if (level == "Error")
            {
                count_errors++;
            }

        }

        Console.WriteLine(main_dateTime_start.ToString("dd.MM.yyyy"));
        Console.WriteLine(main_reward);
        Console.WriteLine(main_winner);
        Console.WriteLine(main_loot);
        Console.WriteLine(main_reward_sad);
        Console.WriteLine(count_warning);
        Console.WriteLine(count_errors);
    }

    static string[] Event_Continue(string[] entries)
    {
        List<string> ivent = new List<string>();

        bool isEventRunning = false;

        foreach (string log in entries)
        {
            if (log.Contains("Событие началось:"))
                isEventRunning = true;

            if (log.Contains("Событие") && log.Contains("закрыто"))
                isEventRunning = false;

            if (isEventRunning)
                ivent.Add(log);
        }
        return ivent.ToArray();
    }
}
