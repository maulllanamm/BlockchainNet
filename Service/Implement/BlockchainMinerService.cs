using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainMinerService : IBlockchainMiner
{
    private readonly IBlocksHasher _blocksHasher;
    private readonly IBlocksCommand _blocksCommand;
    private readonly IBlocksMiner _blocksMiner;
    private readonly ITransactionsCommand _transactionsCommand;
    private readonly ITransactionsQuery _transactionsQuery;
    private readonly int _difficulty = 2;
    private readonly int _reward = 50;
    private readonly List<Block> _chain;

    public BlockchainMinerService(IBlocksHasher blocksHasher, 
        IBlocksCommand blocksCommand, 
        IBlocksMiner blocksMiner, 
        ITransactionsCommand transactionsCommand, 
        ITransactionsQuery transactionsQuery)
    {
        _blocksHasher = blocksHasher;
        _blocksCommand = blocksCommand;
        _blocksMiner = blocksMiner;
        _transactionsCommand = transactionsCommand;
        _transactionsQuery = transactionsQuery;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksCommand.CreateGenesisBlock();
            _chain.Add(genesis);
            BlockchainStorage.Save(_chain);    
        }
    }

    private bool VerifyChain()
    {
        for (int i = 1; i < _chain.Count; i++)
        {
            var currentBlock = _chain[i];
            var previousBlock = _chain[i - 1];
            
            if (currentBlock.Hash != _blocksHasher.CalculateHash(currentBlock))
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
        
        var rewardTransaction = _transactionsCommand.CreateRewardTransaction(minerAddress, _reward);
        var addRewardTransaction = _transactionsCommand.AddTransaction(rewardTransaction);
        if (!addRewardTransaction.Success)
        {
            return Result<Block>.Fail(addRewardTransaction.Message, 400);
        }
        var previousBlock = _chain.Last();
        var pendingTransaction = _transactionsQuery.GetPendingTransactions();
        var block = _blocksCommand.CreateBlock(previousBlock, pendingTransaction.Data);
        _blocksMiner.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionsCommand.ClearPendingTransactions();
        return Result<Block>.Ok(block);
    }
  
}