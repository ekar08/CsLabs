using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager;

class Program
{
    class SaveEntry
    {
        public string Name;
        public string File;
        public string UpdatedAt;
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
        List<SaveEntry> index = LoadIndex("../../../index.json");
        Console.WriteLine(index[0].Name);
        Console.WriteLine(index[1].File);

        SaveData saveData = LoadSave("../../../saves",index[1]);
        Console.WriteLine(saveData.Character);
    }

    static List<SaveEntry> LoadIndex(string indexPath)
    {
        List<SaveEntry> indexSave = JsonConvert.DeserializeObject<List<SaveEntry>>(File.ReadAllText(indexPath));
        return indexSave;
    }

    static SaveData LoadSave(string savesDirectory, SaveEntry entry)
    {
        SaveData save = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText($"{savesDirectory}/{entry.File}"));;

        return save;
    }

}
