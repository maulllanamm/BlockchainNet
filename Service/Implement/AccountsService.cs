using BlockchainNet.Model;
using BlockchainNet.Service.Interface;

namespace BlockchainNet.Service.Implement;

public class AccountsService : IAccountsPool
{
    private readonly IBlockchainQuery _blockchainQuery;

    public AccountsService(IBlockchainQuery blockchainQuery)
    {
        _blockchainQuery = blockchainQuery;
    }


    public Result<decimal> GetBalance(string address)
    {
        decimal balance = 0;
        var chainResult = _blockchainQuery.GetChain();
        foreach (var block in chainResult.Data)
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

    public Result<List<TransactionWithBlockInfo>> GetTransactions(string address)
    {
        var chainResult = _blockchainQuery.GetChain();
        var transactions = chainResult
            .Data
            .SelectMany(t => t.Transactions.Select(x => new TransactionWithBlockInfo
            {
                Transaction = x,
                Timestamp = t.Timestamp,
                BlockHash = t.Hash
            }))
            .Where(tx => tx.Transaction.Sender == address || tx.Transaction.Receiver == address)
            .ToList();
        return Result<List<TransactionWithBlockInfo>>.Ok(transactions);
    }
}