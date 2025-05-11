using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Helper;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class TransactionsesService : ITransactionsPool, ITransactionsFactory, ITransactionsValidation
{
    private readonly List<Transaction> _pendingTransactions;
    private readonly ICryptoHelper _cryptoHelper;
    private readonly IAccountsPool _accountsPool;

    public TransactionsesService(ICryptoHelper cryptoHelper, IAccountsPool accountsPool)
    {
        _cryptoHelper = cryptoHelper;
        _accountsPool = accountsPool;
        _pendingTransactions = new List<Transaction>();
        _pendingTransactions = TransactionStorage.Load() ?? new List<Transaction>();
    }

    public Result<Transaction> AddTransaction(Transaction transaction)
    {
        var isValidSign = _cryptoHelper.Verify(transaction);
        if (!isValidSign)
        {
            return Result<Transaction>.Fail("Cannot add invalid transaction");
        }

        var isValidTransaction = Validate(transaction);
        if (!isValidTransaction.Success)
        {
            return isValidTransaction;
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

    public Result<Transaction> Validate(Transaction transaction)
    {
        if (transaction.Amount <= 0 || transaction.Amount == null)
        {
            return Result<Transaction>.Fail("Invalid amount", 404);
        }

        var sender = _cryptoHelper.GenerateAddress(transaction.PublicKey);
        if (sender != transaction.Sender)
        {
            return Result<Transaction>.Fail("Sender address does not match public key", 404);
        }

        var balance = _accountsPool.GetBalance(sender);
        if (balance.Data < transaction.Amount)
        {
            return Result<Transaction>.Fail("Sender doesn't have enough funds", 404);
        }

        if (transaction.Sender == transaction.Receiver)
        {
            return Result<Transaction>.Fail("Can't send to yourself", 404);
        }

        return Result<Transaction>.Ok(transaction);
    }
}