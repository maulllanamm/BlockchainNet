using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainService : IBlockchainService
{
    private readonly List<Block> _chain;
    private readonly IBlockService _blockService;
    private readonly int _difficulty = 5;
    private readonly int _reward = 50;
    private readonly ITransactionService _transactionService;

    public BlockchainService(IBlockService blockService, ITransactionService transactionService)
    {
        _blockService = blockService;
        _transactionService = transactionService;
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
    public void Mine(string minerAddress)
    {
        if (!VerifyChain())
        {
            throw new InvalidOperationException("Blockchain is invalid. Aborting block addition.");
        }
        
        var rewardTransaction = _transactionService.CreateRewardTransaction(minerAddress, _reward);
        _transactionService.AddTransaction(rewardTransaction);
        var previousBlock = GetLatestBlock();
        var block = _blockService.CreateBlock(previousBlock, _transactionService.GetPendingTransactions());
        _blockService.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionService.ClearPendingTransactions();
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