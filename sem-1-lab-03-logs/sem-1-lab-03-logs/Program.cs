namespace sem_1_lab_03_logs;

class Program
{
    static void Main()
    {
        string[] server_log = File.ReadAllLines("../../../event_server.log");

        DateTime dateTime = Convert.ToDateTime("01.01.01");
        string level = "";
        string category = "";
        string message = "";

        for (int j=0; j<server_log.Length; j++)
        {
            string log = server_log[j];

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

            Console.WriteLine($"{dateTime} hh {level} hh {category} hh {message}");
        }
    }
}
