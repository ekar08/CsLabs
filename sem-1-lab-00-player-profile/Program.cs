namespace Lab_00;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Hello bro. Let's create your own character!\nName: ");
        string name = Console.ReadLine();
        Console.Write("Age: ");
        string tmp = Console.ReadLine();
        Console.WriteLine("Accepted!!");
        int age = Convert.ToInt32(tmp);
        Console.WriteLine("==================================================\nAdditional Information about your character:");
        Console.Write($"20 years later, the character's name will still be {name},\nbut the character will be ");
        if (age+20==67)
        {
            Console.Write("(hehehe~) ");

        }
        
        Console.WriteLine($"{age+20}, not {age}");
        
    }
}
