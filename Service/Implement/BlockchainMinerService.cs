using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainMinerService : IBlockchainMiner
{
    private readonly IBlocksHasher _blocksHasher;
    private readonly IBlocksFactory _blocksFactory;
    private readonly IBlocksMiner _blocksMiner;
    private readonly ITransactionsFactory _transactionsFactory;
    private readonly ITransactionsPool _transactionsPool;
    private readonly int _difficulty = 2;
    private readonly int _reward = 50;
    private readonly List<Block> _chain;

    public BlockchainMinerService(IBlocksHasher blocksHasher, 
        IBlocksFactory blocksFactory, 
        IBlocksMiner blocksMiner, 
        ITransactionsFactory transactionsFactory, 
        ITransactionsPool transactionsPool)
    {
        _blocksHasher = blocksHasher;
        _blocksFactory = blocksFactory;
        _blocksMiner = blocksMiner;
        _transactionsFactory = transactionsFactory;
        _transactionsPool = transactionsPool;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksFactory.CreateGenesisBlock();
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
        
        var rewardTransaction = _transactionsFactory.CreateRewardTransaction(minerAddress, _reward);
        _transactionsPool.AddTransaction(rewardTransaction);
        var previousBlock = _chain.Last();
        var pendingTransaction = _transactionsPool.GetPendingTransactions();
        var block = _blocksFactory.CreateBlock(previousBlock, pendingTransaction.Data);
        _blocksMiner.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionsPool.ClearPendingTransactions();
        return Result<Block>.Ok(block);
    }
  
}