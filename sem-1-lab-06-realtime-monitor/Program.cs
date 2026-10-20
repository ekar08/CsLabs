namespace sem_1_lab_06_realtime_monitor;



internal class Program
{
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

    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        // Этот метод автоматически вызывается библиотекой
        // каждый раз, когда появляются новые данные.

        // cpu — текущая загрузка процессора (в процентах).
        // ram — использование оперативной памяти (в процентах).
        // temperature — температура процессора (в градусах Цельсия).

        Console.WriteLine($"CPU: {cpu}%");
        Console.WriteLine($"RAM: {ram}%");
        Console.WriteLine($"Temperature: {temperature} °C");

        // Вместо этих строк вы можете написать свой код.
        // Например:
        // - сохранить значения в массивы;
        // - обновить график;
        // - вычислить статистику;
        // - вывести информацию в другом формате.
    }
}

