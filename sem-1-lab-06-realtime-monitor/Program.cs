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

        //просто вывожу массив, чтобы понять, что туда записывается
        Console.WriteLine("CPU");
        int c=0;
        foreach(double data in cpuData)
        {
            c++;
            Console.WriteLine($"{c}.{data}");
        }

        Console.WriteLine("RAM");
        int a=0;
        foreach(double data in ramData)
        {
            a++;
            Console.WriteLine($"{a}.{data}");
        }

        Console.WriteLine("TEM");
        int s=0;
        foreach(double data in temData
        )
        {
            s++;
            Console.WriteLine($"{s}.{data}");
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
}

