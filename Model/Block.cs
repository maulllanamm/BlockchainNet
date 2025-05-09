using System.Security.Cryptography;
using System.Text;

namespace BlockchainNet.Model;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }

    public Block(int index, List<Transaction> transactions, string previousHash = "")
    {
        Index = index;
        Transactions = transactions;
        Timestamp = DateTime.UtcNow;
        PreviousHash = previousHash;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        var transactionDetail = string.Join(',', Transactions.Select(t => $"{t.From}-{t.To}-{t.Amount}"));
        var input = Index + Timestamp.ToString() + PreviousHash + transactionDetail;
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }


}
