using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Helper;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class TransactionsService : ITransactionsPool, ITransactionFactory
{
    private readonly List<Transaction> _pendingTransactions;
    private readonly ICryptoHelper _cryptoHelper;

    public TransactionsService(ICryptoHelper cryptoHelper)
    {
        _cryptoHelper = cryptoHelper;
        _pendingTransactions = new List<Transaction>();
        _pendingTransactions = TransactionStorage.Load() ?? new List<Transaction>();
    }

    public Result<Transaction> AddTransaction(Transaction transaction)
    {
        var isValid = _cryptoHelper.Verify(transaction);
        if (!isValid)
        {
            return Result<Transaction>.Fail("Cannot add invalid transaction");
        }
        _pendingTransactions.Add(transaction);
        TransactionStorage.Save(_pendingTransactions);
        return Result<Transaction>.Ok(transaction);
    }

    public Result<List<Transaction>> GetPendingTransactions()
    {
        return Result<List<Transaction>>.Ok(_pendingTransactions);
    }

    public Result<List<Transaction>> ClearPendingTransactions()
    {
        _pendingTransactions.Clear();
        TransactionStorage.Save(_pendingTransactions);
        return Result<List<Transaction>>.Ok(_pendingTransactions);
    }

    public Transaction CreateRewardTransaction(string miner, decimal rewardAmount)
    {
        return new Transaction
        {
            Sender = "SYSTEM",
            Receiver = miner,
            Amount = rewardAmount,
            PublicKey = "SYSTEM",
            Signature = "SYSTEM"
        };
    }
}