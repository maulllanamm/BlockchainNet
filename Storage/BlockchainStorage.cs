using System.IO;
using System.Text.Json;
using BlockchainNet.Model;

namespace BlockchainNet.Storage;

public class BlockchainStorage
{
    private const string FilePath = "blockchain.json";

    public static void Save(List<Block> chains)
    {
        var json = JsonSerializer.Serialize(chains, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, json);
    }

    public static List<Block> Load()
    {
        if (!File.Exists(FilePath))
            return null;
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Block>>(json);
    }
}