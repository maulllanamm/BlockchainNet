using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainService : IBlockchainMiner, IBlockchainReader
{
    private readonly List<Block> _chain;
    private readonly IBlocksHasher _blocksHasher;
    private readonly IBlocksFactory _blocksFactory;
    private readonly IBlocksMiner _blocksMiner;
    private readonly int _difficulty = 2;
    private readonly int _reward = 50;
    private readonly ITransactionFactory _transactionFactory;
    private readonly ITransactionsPool _transactionsPool;

    public BlockchainService(IBlocksHasher blocksHasher, 
        IBlocksFactory blocksFactory, 
        IBlocksMiner blocksMiner, 
        ITransactionFactory transactionFactory, 
        ITransactionsPool transactionsPool)
    {
        _blocksHasher = blocksHasher;
        _blocksFactory = blocksFactory;
        _blocksMiner = blocksMiner;
        _transactionFactory = transactionFactory;
        _transactionsPool = transactionsPool;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksFactory.CreateGenesisBlock();
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
        
        var rewardTransaction = _transactionFactory.CreateRewardTransaction(minerAddress, _reward);
        _transactionsPool.AddTransaction(rewardTransaction);
        var previousBlock = GetLatestBlock();
        var pendingTransaction = _transactionsPool.GetPendingTransactions();
        var block = _blocksFactory.CreateBlock(previousBlock.Data, pendingTransaction.Data);
        _blocksMiner.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionsPool.ClearPendingTransactions();
        return Result<Block>.Ok(block);
    }
  
}