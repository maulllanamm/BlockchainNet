using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainService : IBlockchainService
{
    private readonly List<Block> _chain;
    private readonly IBlockService _blockService;

    public BlockchainService(IBlockService blockService)
    {
        _blockService = blockService;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blockService.CreateGenesisBlock();
            _chain.Add(genesis);
            BlockchainStorage.Save(_chain);    
        }
    }
    
    public List<Block> GetChain() => _chain;
    public Block GetLatestBlock() => _chain.Last();

    public void AddBlock(NewBlock newBlock)
    {
        var block = new Block
        {
            Index = newBlock.Index,
            Timestamp = DateTime.UtcNow,
            PreviousHash = GetLatestBlock().Hash,
            Transactions = newBlock.Transactions
        };
        block.Hash = _blockService.CalculateHash(block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
    }

    public bool VerifyChain()
    {
        for (int i = 1; i < _chain.Count; i++)
        {
            var currentBlock = _chain[i];
            var previousBlock = _chain[i - 1];
            
            if (currentBlock.Hash != _blockService.CalculateHash(currentBlock))
            {
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
        }
        return true;
    }
}