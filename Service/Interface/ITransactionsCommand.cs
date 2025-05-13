using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsCommand
{
    Result<Transaction> CreateInitialTransaction(int initialBalance = 100);
    Transaction CreateRewardTransaction(string receiver, decimal amount);
    Result<Transaction>  AddTransaction(Transaction transaction);
    Result<List<Transaction>>  ClearPendingTransactions();
}