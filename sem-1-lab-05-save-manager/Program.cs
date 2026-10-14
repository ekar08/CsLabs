using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager;

class Program
{
    class IndexData
    {
        public string Name;
        public string File;
        public string UpdatedAt;
    }

    static void Main()
    {
        List<IndexData> index = LoadIndex("../../../index.json");
        Console.WriteLine(index[0].Name);
    }

    static List<IndexData> LoadIndex(string indexPath)
    {
        List<IndexData> indexSave = JsonConvert.DeserializeObject<List<IndexData>>(File.ReadAllText(indexPath));
        return indexSave;
    }

    /*static string[] LoadIndex(string indexPath)
    {
        string[] indexSave = File.ReadAllLines(indexPath);
        return indexSave;
    }*/

}
