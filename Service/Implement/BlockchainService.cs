using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainService : IBlockchainService
{
    private readonly List<Block> _chain;
    private readonly IBlocksService _blocksService;
    private readonly int _difficulty = 2;
    private readonly int _reward = 50;
    private readonly ITransactionsService _transactionsService;

    public BlockchainService(IBlocksService blocksService, ITransactionsService transactionsService)
    {
        _blocksService = blocksService;
        _transactionsService = transactionsService;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksService.CreateGenesisBlock();
            _chain.Add(genesis);
            BlockchainStorage.Save(_chain);    
        }
    }
    
    public Result<List<Block>> GetChain()
    {
        return Result<List<Block>>.Ok(_chain);
    }

    public Result<Block> GetLatestBlock()
    {
        return Result<Block>.Ok(_chain.Last());
    }

    private bool VerifyChain()
    {
        for (int i = 1; i < _chain.Count; i++)
        {
            var currentBlock = _chain[i];
            var previousBlock = _chain[i - 1];
            
            if (currentBlock.Hash != _blocksService.CalculateHash(currentBlock))
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
    public Result<Block> Mine(string minerAddress)
    {
        if (!VerifyChain())
        {
            return Result<Block>.Fail("Blockchain is invalid. Aborting block addition.", 400);
        }
        
        var rewardTransaction = _transactionsService.CreateRewardTransaction(minerAddress, _reward);
        _transactionsService.AddTransaction(rewardTransaction);
        var previousBlock = GetLatestBlock();
        var pendingTransaction = _transactionsService.GetPendingTransactions();
        var block = _blocksService.CreateBlock(previousBlock.Data, pendingTransaction.Data);
        _blocksService.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionsService.ClearPendingTransactions();
        return Result<Block>.Ok(block);
    }
  
}