using System.Globalization;

namespace sem_1_lab_06_realtime_monitor;



internal class Program
{
    private static double[] cpuData = new double[20];
    private static double[] ramData = new double[20];
    private static double[] temData = new double[20];
    private static int index_cpu = 0;
    private static int index_ram = 0;
    private static int index_tem = 0;
    

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
    }


    //метод из примера использования библиотеки, записываю измерения в массив
    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        index_cpu = WriteInList(cpuData,cpu,index_cpu);
        string[][] cpu_arr1 = OneMeasurement(cpuData);
        string[] cpu_arr2 = ListWithString(cpu_arr1);
        string cpu_name = "CPU:";
        
        index_ram = WriteInList(ramData,ram,index_ram);
        string[][] ram_arr1 = OneMeasurement(ramData);
        string[] ram_arr2 = ListWithString(ram_arr1);
        string ram_name = "RAM";

        index_tem = WriteInList(temData,temperature,index_tem);
        string[][] tem_arr1 = OneMeasurement(temData);
        string[] tem_arr2 = ListWithString(tem_arr1);
        string tem_name = "TEMP:";

        Console.Clear();
        
        DrawGrafic(cpu_arr2, cpu_name);
        DrawGrafic(ram_arr2, ram_name);
        DrawGrafic(tem_arr2, tem_name);
    }

    //записывает данные в список
    static int WriteInList(double[] arrData, double data, int buffindex)
    {
        arrData[buffindex]=data;
        if (buffindex + 1 < 20)
        {
            buffindex++;
        }
        else
        {
            double[] arrData_old = arrData;
            Array.Copy(arrData_old, 1, arrData, 0, 19);
        }

        return buffindex;
    }


    static string[][] OneMeasurement(double[] buffData)
    {
        string[][] grafic = new string[20][];

        for (int j=0; j<20; j++)
        {
            double meas = buffData[j];
            int one_data = Convert.ToInt32(meas);
            one_data = one_data/5*5;

            string[] line = new string[20];

            for (int i=0; i<one_data/5; i++)
            {
                line[i]="#";
            }

            grafic[j] = line;
        }

        return grafic;
    }

    static string[] ListWithString(string[][] buffData)
    {
        string[] Data = new string[21];

        for (int i=0; i<20; i++)
        {
            string[] one_data = buffData[i];
            string line=$"{i+1,2}|";
            /*if (i < 9)
            {
                line=$"0{i+1}|";
            }*/

            foreach (string a in one_data)
            {
                line+=a;
            }
            Data[i]=line;
        }
        
        Data[20] = "  0 15 30 45 60 75 90 100";
        return Data;
    }

    static void DrawGrafic(string[] buffData, string name)
    {
        Console.WriteLine(name);
        foreach (string a in buffData)
        {
            Console.WriteLine(a);
        }
    }
    
}

