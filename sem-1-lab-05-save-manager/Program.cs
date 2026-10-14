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
        
        List<SaveEntry> index = LoadIndex("../../../index.json");/*
        Console.WriteLine(index[0].Name);
        Console.WriteLine(index[1].File);

        SaveData saveData = LoadSave("../../../saves",index[0]);
        Console.WriteLine(saveData.Character);


        string stats = PrintSave(index[0],saveData);
        Console.WriteLine(stats);*/

        SaveEntry lost = FindByName(index,"slo");
        Console.WriteLine(PrintSave(lost,LoadSave("../../../saves",lost)));
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
}
