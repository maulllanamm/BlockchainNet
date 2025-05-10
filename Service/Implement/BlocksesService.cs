using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;

namespace BlockchainNet.Service.Implement;

public class BlocksesService : IBlocksService
{
    public Block CreateGenesisBlock()
    {
        var block =  new Block
        {
            Timestamp = DateTime.UtcNow,
            Transactions = new List<Transaction>(),
            PreviousHash = "0",
            nonce = 0
        };
        
        block.Hash = CalculateHash(block);
        return block;
    }
    
    public Block CreateBlock(Block previousBlock, List<Transaction> transactions)
    {
        var block =  new Block
        {
            Timestamp = DateTime.UtcNow,
            Transactions = transactions.Select(t => new Transaction
            {
                Receiver = t.Receiver,
                Sender = t.Sender,
                Amount = t.Amount,
            }).ToList(),
            PreviousHash = previousBlock.Hash,
            nonce = 0
        };
        
        block.Hash = CalculateHash(block);
        return block;
    }
    
    public string CalculateHash(Block block)
    {
        var transactionDetail = string.Join(',', block.Transactions.Select(t => $"{t.Sender}-{t.Receiver}-{t.Amount}"));
        var input = block.Timestamp.ToString() + block.PreviousHash + transactionDetail + block.nonce.ToString();
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }

    public void MineBlock(int difficulty, Block block)
    {
        string target = new string('0', difficulty);
        while (!block.Hash.StartsWith(target))
        {
            block.nonce++;
            block.Hash = CalculateHash(block);
        }
    }

}