namespace sem_1_lab_06_realtime_monitor;



internal class Program
{
    private static double[] cpuData = new double[20];
    private static List<double> ramData = new();
    private static List<double> temData = new();
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
        int c=0;
        foreach(double data in cpuData)
        {
            c++;
            Console.WriteLine($"{c}.{data}");
        }
    }

    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        int index = FindLastEmptyIndex(cpuData, out cpuData);
        cpuData[index]=cpu;

        Console.WriteLine(cpu);
    }

    static int FindLastEmptyIndex(double[] Data_arr, out double[] Data_arr_new)
    {
        Data_arr_new = Data_arr;
        for (int i=19; i>=0; i--)
        {
            if (Data_arr[i] == 0)
            {
                return i;
            }
        }
        
        Array.Copy(Data_arr, 1, Data_arr_new, 0, 19);
        return 18;
    }
}

