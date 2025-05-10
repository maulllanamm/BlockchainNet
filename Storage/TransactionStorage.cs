using System.Text.Json;
using BlockchainNet.Model;

namespace BlockchainNet.Storage;

public class TransactionStorage
{
    private const string FilePath = "transaction.json";

    public static void Save(List<Transaction> transactions)
    {
        var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, json);
    }

    public static List<Transaction> Load()
    {
        if (!File.Exists(FilePath))
            return null;
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Transaction>>(json);
    }
}