namespace sem_1_lab_03_logs;

class Program
{
    static void Main()
    {
        string[] server_log = File.ReadAllLines("../../../event_server.log");

        DateTime main_dateTime_start = Convert.ToDateTime("01.01.01");
        string main_winner = "";
        string main_reward = "";

        for (int j=0; j<server_log.Length; j++)
        {
            string log = server_log[j];

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

            if (log.Contains("Событие началось:"))
            {
                main_dateTime_start = dateTime;
            }

            if (log.Contains("объявлены победителями события") && category == "Reward")
            {
                main_winner = message.Substring(0,message.IndexOf("объявлены победителями события")-1);
            }

            if (log.Contains("получил") && log.Contains("очков события") && category == "Reward" && !log.Contains("утешительную награду"))
            {
                int ind_start = message.IndexOf("получил")+7;
                int ind_end = message.IndexOf("очков события");
                main_reward = message.Substring(ind_start,ind_end-ind_start);
            }
        }

        Console.WriteLine(main_dateTime_start.ToString("dd.MM.yyyy"));
        Console.WriteLine(main_reward);
    }
}
