using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsCommand
{
    Transaction CreateRewardTransaction(string receiver, decimal amount);
    Result<Transaction>  AddTransaction(Transaction transaction);
    Result<List<Transaction>>  ClearPendingTransactions();
}