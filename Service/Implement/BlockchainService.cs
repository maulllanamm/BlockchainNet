using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainService : IBlockchainService
{
    private readonly List<Block> _chain;
    private readonly IBlockService _blockService;
    private readonly int _difficulty = 2;
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
    public Result<Block> Mine(string minerAddress)
    {
        if (!VerifyChain())
        {
            return Result<Block>.Fail("Blockchain is invalid. Aborting block addition.", 400);
        }
        
        var rewardTransaction = _transactionService.CreateRewardTransaction(minerAddress, _reward);
        _transactionService.AddTransaction(rewardTransaction);
        var previousBlock = GetLatestBlock();
        var pendingTransaction = _transactionService.GetPendingTransactions();
        var block = _blockService.CreateBlock(previousBlock.Data, pendingTransaction.Data);
        _blockService.MineBlock(_difficulty, block);
        _chain.Add(block);
        BlockchainStorage.Save(_chain);
        _transactionService.ClearPendingTransactions();
        return Result<Block>.Ok(block);
    }

    public Result<decimal> GetBalanceOfAddress(string address)
    {
        decimal balance = 0;
        foreach (var block in _chain)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.Sender == address)
                {
                    balance -= transaction.Amount;
                }

                if (transaction.Receiver == address)
                {
                    balance += transaction.Amount;
                }
            }
        }
        return Result<decimal>.Ok(balance);
    }
}