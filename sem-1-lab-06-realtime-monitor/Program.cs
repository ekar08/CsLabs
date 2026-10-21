using System.Globalization;

namespace sem_1_lab_06_realtime_monitor;



internal class Program
{
    private static double[] cpuData = new double[20];
    private static double[] ramData = new double[20];
    private static double[] temData = new double[20];
    private static void Main()
    {
        // Создаём объект, который будет получать данные о состоянии сервера.
        var monitor = new ServerMonitor();

        // Сообщаем библиотеке, какой метод нужно вызывать
        // каждый раз, когда появляются новые данные.
        monitor.DataReceived += OnDataReceived;

        // Запускаем получение данных.
        // После этого библиотека сама будет передавать новые значения примерно раз в секунду.
        monitor.Start();

        Console.WriteLine("Мониторинг запущен.");
        Console.WriteLine("Для завершения программы нажмите Enter.");

        // Не даём программе завершиться сразу после запуска.
        Console.ReadLine();

        // При желании можно остановить получение данных.
        monitor.Stop();

        string[][] arr = DrawGrafics(cpuData);

        foreach (string[] arr_mini in arr)
        {
            foreach (string arr_mini_mini in arr_mini)
            {
                Console.Write(arr_mini_mini);
            }
            Console.WriteLine();
        }
    }


    //метод из примера использования библиотеки, записываю измерения в массив
    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        int index_cpu = FindLastEmptyIndex(cpuData, out cpuData);
        cpuData[index_cpu]=cpu;

        int index_ram = FindLastEmptyIndex(ramData, out ramData);
        ramData[index_ram]=ram;

        int index_temper = FindLastEmptyIndex(temData, out temData);
        temData[index_temper]=temperature;

        Console.WriteLine(cpu);
    }


    //ищу последний свободный индекс
    static int FindLastEmptyIndex(double[] Data_arr, out double[] Data_arr_new)
    {
        Data_arr_new = Data_arr;
        for (int i=0; i<Data_arr.Length; i++)
        {
            if (Data_arr[i] == 0)
            {
                return i;
            }
        }
        
        //если такого нет и я уже записала 20 измерений, то создаю новый список без 0го элемента

        Array.Copy(Data_arr, 1, Data_arr_new, 0, 19);
        return 18;
    }

    static string[][] DrawGrafics(double[] Data)
    {
        string[][] grafic = new string[22][];

        for (int i=0; i<22; i++)//столбец
        {
            string[] line = new string[22];

            for (int j=0; j<22; j++) //строка
            {
                line[0]=Convert.ToString(i+1);
                line[1]="|";
                if (i == 20)
                {
                    line[j]="__";
                }
                //if (i == 21)
                //{
                //    line[j]=$" {Convert.ToString(j)}";
                //}
            }
            grafic[i] = line;
        }
        return grafic;
    }
}

