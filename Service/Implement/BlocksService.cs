using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Helper;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;

namespace BlockchainNet.Service.Implement;

public class BlocksService : IBlocksHasher, IBlocksCommand, IBlocksMiner
{
    private readonly IHelperHash _helperHash;
    public BlocksService(IHelperHash helperHash)
    {
        _helperHash = helperHash;
    }

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
                PublicKey = t.PublicKey,
                Signature = t.Signature
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
        return _helperHash.GenerateHash(input);
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