using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class TransactionService : ITransactionService
{
    private readonly List<Transaction> _pendingTransactions;

    public TransactionService()
    {
        _pendingTransactions = new List<Transaction>();
        _pendingTransactions = TransactionStorage.Load() ?? new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        _pendingTransactions.Add(transaction);
        TransactionStorage.Save(_pendingTransactions);
    }

    public List<Transaction> GetPendingTransactions() => _pendingTransactions;

    public void ClearPendingTransactions()
    {
        _pendingTransactions.Clear();
        TransactionStorage.Save(_pendingTransactions);
    }

    public Transaction CreateRewardTransaction(string miner, decimal rewardAmount)
    {
        return new Transaction
        {
            Sender = "SYSTEM",
            Receiver = miner,
            Amount = rewardAmount,
        };
    }
}