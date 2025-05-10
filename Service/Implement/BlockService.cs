using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;

namespace BlockchainNet.Service.Implement;

public class BlockService : IBlockService
{
    
    public Block CreateGenesisBlock()
    {
        var block =  new Block
        {
            Index = 0,
            Timestamp = DateTime.UtcNow,
            Transactions = new List<Transaction>(),
            PreviousHash = "0",
        };
        
        block.Hash = CalculateHash(block);
        return block;
    }
    
    
    public string CalculateHash(Block block)
    {
        var transactionDetail = string.Join(',', block.Transactions.Select(t => $"{t.From}-{t.To}-{t.Amount}"));
        var input = block.Index + block.Timestamp.ToString() + block.PreviousHash + transactionDetail;
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }

}