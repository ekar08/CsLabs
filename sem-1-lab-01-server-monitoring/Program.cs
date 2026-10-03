using System.Linq;
using System.IO;
namespace Lab_01;

class Program
{
    // Есть одна проблемка.. я сижу на линуксе и путь к файлу написала в файловой системе линукс Т-Т
    static void Main(string[] args)
    {
        string path_to_data = "../../../data.txt"; //путь к файлу
        File.AppendAllText(path_to_data,"");


        Console.WriteLine("Запись данных...");


        Console.Write("Дата (dd-mm-yyyy): ");
        string w_date = Console.ReadLine();
        File.AppendAllText(path_to_data,w_date+';');
         
        Console.Write("Кол-во зарегестрированных игроков на сервере: ");
        string w_gamers = Console.ReadLine();
        File.AppendAllText(path_to_data,w_gamers+';');

        Console.Write("Кол-во активных игроков на сервере: ");
        string w_activity = Console.ReadLine();
        File.AppendAllText(path_to_data,w_activity+';');

        Console.WriteLine("Координаты последнего уровня: ");
        Console.Write("X: ");
        string w_coordinate_x = Console.ReadLine();
        File.AppendAllText(path_to_data,w_coordinate_x+';');
        Console.Write("Y: ");
        string w_coordinate_y = Console.ReadLine();
        File.AppendAllText(path_to_data,w_coordinate_y+';');

        
        string data = File.ReadAllText(path_to_data); //данные из файла, но в строке
        

        int count_writing = 0; //сколько сделано записей
        if (data.Count(n => n ==';')==5)
        {
            count_writing=1;
        }
        else if (data.Count(n => n ==';')==10)
        {
            count_writing=2;
        }
        else if (data.Count(n => n ==';')==15)
        {
            count_writing=3;
        }

        int i_sep = -1; // индекс последнего разделителя
        int count_sep = 0; // количество разделителей

        string heading = "| Дата       | Зареганых | Активных |  X  |  Y  |";



        string tmp_r_date1 = "";
        string tmp_r_gamers1 = "";
        string tmp_r_activity1 = "";
        string tmp_r_coordinate_x1 = "";
        string tmp_r_coordinate_y1 = "";

        string tmp_r_date2 = "";
        string tmp_r_gamers2 = "";
        string tmp_r_activity2 = "";
        string tmp_r_coordinate_x2 = "";
        string tmp_r_coordinate_y2 = "";

        int r_gamers1 = 0;
        int r_activity1 = 0;
        double r_coordinate_x1 = 0;
        double r_coordinate_y1 = 0;

        int r_gamers2 = 0;
        int r_activity2 = 0;
        double r_coordinate_x2 = 0;
        double r_coordinate_y2 = 0;

        if (count_writing == 1)
        {
            Console.WriteLine("Для аналитики сервера слишком мало данных, повторите запись позже\n(Еще раз запустите файл и проведите запись)");
        }
        if (count_writing == 3)
        {
            Console.WriteLine("Для аналитики сервера слишком много данных, старые данные будут удалены");
            int c=0;
            string data_new = "";
            for (int i=0; i<data.Length; i++)
            {
                if (data[i] == ';')
                {
                    c++;
                    
                }
                if (c == 5)
                {
                    data_new = data[(i+1)..data.Length];
                    File.WriteAllText(path_to_data,data_new);
                    c++;
                }
            }
            count_writing = 2;
        }

        if (count_writing == 2)
        {
            Console.WriteLine("Для аналитики сервера данных достаточно\nНачинаем Аналитику!!!\n");
            data = File.ReadAllText(path_to_data); // записываем измененные данные
            for (int i=0; i<data.Length; i++)
            {
                if (data[i] == ';')
                {
                    count_sep++;
                    if (count_sep==1)
                    {
                        tmp_r_date1+=data[(i_sep+1)..i];
                    }
                    if (count_sep==2)
                    {
                        tmp_r_gamers1+=data[(i_sep+1)..i];
                        r_gamers1 = Convert.ToInt32(tmp_r_gamers1);
                    }
                    if (count_sep==3)
                    {
                        tmp_r_activity1+=data[(i_sep+1)..i];
                        r_activity1 = Convert.ToInt32(tmp_r_activity1);
                    }
                    if (count_sep==4)
                    {
                        tmp_r_coordinate_x1+=data[(i_sep+1)..i];
                        r_coordinate_x1 = Convert.ToDouble(tmp_r_coordinate_x1);
                    }
                    if (count_sep==5)
                    {
                        tmp_r_coordinate_y1+=data[(i_sep+1)..i];
                        r_coordinate_y1 = Convert.ToDouble(tmp_r_coordinate_y1);
                    }
                    if (count_sep==6)
                    {
                        tmp_r_date2+=data[(i_sep+1)..i];
                    }
                    if (count_sep==7)
                    {
                        tmp_r_gamers2+=data[(i_sep+1)..i];
                        r_gamers2 = Convert.ToInt32(tmp_r_gamers2);
                    }
                    if (count_sep==8)
                    {
                        tmp_r_activity2+=data[(i_sep+1)..i];
                        r_activity2 = Convert.ToInt32(tmp_r_activity2);
                    }
                    if (count_sep==9)
                    {
                        tmp_r_coordinate_x2+=data[(i_sep+1)..i];
                        r_coordinate_x2 = Convert.ToDouble(tmp_r_coordinate_x2);
                    }
                    if (count_sep==10)
                    {
                        tmp_r_coordinate_y2+=data[(i_sep+1)..i];
                        r_coordinate_y2 = Convert.ToDouble(tmp_r_coordinate_y2);
                    }
                    i_sep=i;
                }
            }

            Console.WriteLine(heading);
            Console.WriteLine($"| {tmp_r_date1} |    {tmp_r_gamers1}     |    {tmp_r_activity1}    | {tmp_r_coordinate_x1} | {tmp_r_coordinate_y1} |");
            Console.WriteLine($"| {tmp_r_date2} |    {tmp_r_gamers2}     |    {tmp_r_activity2}    | {tmp_r_coordinate_x2} | {tmp_r_coordinate_y2} |");


            Console.WriteLine( "\n--------------------Изменения--------------------");
            Console.WriteLine($"|            |    {r_gamers2-r_gamers1}     |    {r_activity2-r_activity1}    | {r_coordinate_x2-r_coordinate_x1} | {r_coordinate_y2-r_coordinate_y1} |");
        }



    }
}
