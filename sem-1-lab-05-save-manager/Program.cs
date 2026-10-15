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

        List<SaveEntry> index = LoadIndex(indexPath);

        RestoreBackup(savesPath, backupPath, index[1]);
    }

    static List<SaveEntry> LoadIndex(string indexPath)
    {
        List<SaveEntry> indexSave = JsonConvert.DeserializeObject<List<SaveEntry>>(File.ReadAllText(indexPath));
        return indexSave;
        //сделать соо об ошибке
    }

    static SaveData LoadSave(string savesDirectory, SaveEntry entry)
    {
        SaveData save = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText($"{savesDirectory}/{entry.File}"));;

        return save;
        //сделать соо об ошибке
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
        foreach (SaveEntry entry in entries)
        {
            if (entry.Name == name)
                return entry;
        }

        return new SaveEntry
        {
            Name = "Не найдено",
            File = "autosave.json",
            UpdatedAt = DateTime.Now
        };
    }

    static void SaveGame(string savesDirectory, SaveEntry entry, SaveData save)
    {
        Console.WriteLine($"Давайте изменим сохранение в {save}");
        SaveData new_save = new SaveData();

        Console.Write("Character: ");
        new_save.Character = Console.ReadLine();

        Console.Write("Map: ");
        new_save.Map = Console.ReadLine();

        Console.Write("Position.\nX: ");
        new_save.Position.X = Convert.ToInt32(Console.ReadLine());
        Console.Write("Y: ");
        new_save.Position.Y = Convert.ToInt32(Console.ReadLine());
        Console.Write("Health: ");

        new_save.Health = Convert.ToInt32(Console.ReadLine());
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
        new_save.Inventory = new_inventory;

        string user_save = JsonConvert.SerializeObject(new_save, Formatting.Indented);
        File.WriteAllText($"{savesDirectory}/save-03.json", user_save);
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
}
