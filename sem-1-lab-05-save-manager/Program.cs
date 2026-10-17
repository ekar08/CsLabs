using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager;

class Program
{
    class SaveEntry
    {
        public string Name;
        public string File;
        public DateTime UpdatedAt;
    }

    class SaveData
    {
        public string Character;
        public string Map;
        public Position Position;
        public int Health;        
        public List<string> Inventory;
    }

    struct Position
    {
        public int X;
        public int Y;
    }

    static void Main()
    {
        string indexPath = "../../../index.json";
        string savesPath = "../../../saves";
        string backupPath = "../../../backups";

        List<SaveEntry> indexList = LoadIndex(indexPath);
        int choice=0;
        //это и есть то самое меню, которое бесконечно выполняется, пока я из него не выйду
        do{
            Console.Write("\n\n1# Показать список сохранений.\n2# Посмотреть данные сохранения.\n3# Изменить данные персонажа и записать их обратно.\n4# Создать резервную копию.\n5# Восстановить резервную копию.\n6# Удалить сохранение.\n7# Выйти.\n\n=======================================\n\nВаш выбор# ");

            choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nСписок сохранений: ");
                    foreach (SaveEntry index_one in indexList)  
                        Console.WriteLine(index_one.Name);
                    break;

                case 2:
                    Console.Write("\nВведите имя сохранения: ");
                    string nameSave2 = Console.ReadLine();
                    SaveEntry interestEntry2 = FindByName(indexList,nameSave2);
                    if (interestEntry2 != null)
                    {
                        SaveData interestData2 = LoadSave(savesPath,interestEntry2);
                        Console.WriteLine(PrintSave(interestEntry2,interestData2));
                    }
                    
                    break;

                case 3:
                    Console.Write("\nВведите имя сохранения, которое хотите редактировать: ");
                    string nameSave3 = Console.ReadLine();
                    SaveEntry interestEntry3 = FindByName(indexList,nameSave3);
                    if (interestEntry3!=null) 
                    {
                        SaveData interestData3 = LoadSave(savesPath,interestEntry3);
                        SaveGame(savesPath,interestEntry3,indexPath,indexList);
                    }
                    break;

                case 4:
                    Console.Write("\nВведите имя сохранения, для которого хотите сделать резервную копию: ");
                    string nameSave4 = Console.ReadLine();
                    SaveEntry interestEntry4 = FindByName(indexList,nameSave4);
                    if (interestEntry4!=null)
                    {
                        CreateBackup(savesPath,backupPath,interestEntry4);
                    }
                    break;

                case 5:
                    Console.Write("\nВведите имя сохранения, чью резервную копию вы хотите восстановить: ");
                    string nameSave5 = Console.ReadLine();
                    SaveEntry interestEntry5 = FindByName(indexList,nameSave5);
                    if (interestEntry5 != null)
                    {
                        RestoreBackup(savesPath,backupPath,interestEntry5);
                    }
                    break;

                case 6:
                    Console.Write("\nВведите имя сохранения, которое хотите удалить: ");
                    string nameSave6 = Console.ReadLine();
                    SaveEntry interestEntry6 = FindByName(indexList,nameSave6);
                    if (interestEntry6 != null)
                    {
                        DeleteSave(savesPath,indexPath,indexList,interestEntry6);
                    }
                    break;
            }
        }
        while(choice!=7);
    }
    static List<SaveEntry> LoadIndex(string indexPath)
    {
        try 
        {
        List<SaveEntry> indexSave = JsonConvert.DeserializeObject<List<SaveEntry>>(File.ReadAllText(indexPath));
        return indexSave;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("JSON некорректен");
        }
        return null;
    }

    static SaveData LoadSave(string savesDirectory, SaveEntry entry)
    {
        try 
        {
        SaveData save = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText($"{savesDirectory}/{entry.File}"));;
        return save;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("JSON некорректен");
        }
        return null;
    }

    static string PrintSave(SaveEntry entry, SaveData save)
    {
        string[] inventory_arr = save.Inventory.ToArray();
        string inventory_str = "";

        foreach (string loot in inventory_arr)
        {
            if (inventory_str!="")
                inventory_str+=", ";
            
            inventory_str+=loot;
        }

        string printSave = $"Имя сохранения: {entry.Name}\nИмя персонажа: {save.Character}\nКарта: {save.Map}\nКоординаты: x={save.Position.X} ; y={save.Position.Y}\nЗдоровье: {save.Health}\nИнвентарь: {inventory_str}";
        return printSave;
    }

    static SaveEntry FindByName(List<SaveEntry> entries, string name)
    {
        bool find = false;
        foreach (SaveEntry entry in entries)
        {
            string userNameL = entry.Name.ToLower();
            string nameL = name.ToLower();
            if (userNameL == nameL)
            {
                find = true;
                return entry; 
            }  
        }

        if (find==false)
            Console.WriteLine("Файл не найден");
        
        return null;
        
    }

    static void SaveGame(string savesDirectory, SaveEntry entry, string indexPath,List<SaveEntry> entries)
    {
        Console.WriteLine($"Давайте изменим сохранение в {entry.Name}");
        SaveData save = LoadSave(savesDirectory,entry);
        SaveData new_save = new SaveData();

        new_save.Character = save.Character;

        Console.Write("Map: ");
        string new_saveMap = Console.ReadLine();
        if (new_saveMap.Trim() == "")
            new_save.Map = save.Map;
        else
            new_save.Map = new_saveMap;

        Console.Write("Position.\nX: ");
        string new_savePosition_X = Console.ReadLine();
        if (new_savePosition_X.Trim() == "")
            new_save.Position.X = save.Position.X;
        else
            new_save.Position.X = Convert.ToInt32(new_savePosition_X);

        Console.Write("\nY: ");
        string new_savePosition_Y = Console.ReadLine();
        if (new_savePosition_Y.Trim() == "")
            new_save.Position.Y = save.Position.Y;
        else
            new_save.Position.Y = Convert.ToInt32(new_savePosition_Y);

        Console.Write("Health: ");
        string new_saveHealth = Console.ReadLine();
        if (new_saveHealth.Trim() == "")
            new_save.Health = save.Health;
        else
            new_save.Health = Convert.ToInt32(new_saveHealth);

        Console.Write("Inventory: ");
        List<string> new_inventory = new List<string>();
        string loot;
        do
        {
            loot = Console.ReadLine();
            if (loot!="")
                new_inventory.Add(loot); 
        }
        while (loot!="");
        
        if (new_inventory.Count == 0)
            new_save.Inventory = save.Inventory;
        else
            new_save.Inventory = new_inventory;

        string user_save = JsonConvert.SerializeObject(new_save, Formatting.Indented);
        File.WriteAllText($"{savesDirectory}/{entry.File}", user_save);

        var settings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd'T'HH:mm:ss"
        };
        entry.UpdatedAt = DateTime.Now;
        string user_index = JsonConvert.SerializeObject(entries, Formatting.Indented,settings);
        File.WriteAllText(indexPath, user_index);
    }

    static void SaveIndex(string indexPath, List<SaveEntry> entries)
    {
        string user_index = JsonConvert.SerializeObject(entries, Formatting.Indented);

        File.WriteAllText($"{indexPath}", user_index);
    }

    static void CreateBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
    {
        SaveData saveData = LoadSave(savesDirectory,entry);
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText($"{backupsDirectory}/{entry.File}", json);
    }

    static void RestoreBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
    {
        SaveData saveData = LoadSave(backupsDirectory,entry);
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText($"{savesDirectory}/{entry.File}", json);
    }

    static void DeleteSave(string savesDirectory, string indexPath, List<SaveEntry> entries, SaveEntry entry)
    {
        File.Delete($"{savesDirectory}/{entry.File}");
        //int removeIndex = entries.FindIndex()
    }
}
