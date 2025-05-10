using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsPool
{
    Result<Transaction>  AddTransaction(Transaction transaction);
    Result<List<Transaction>> GetPendingTransactions();
    Result<List<Transaction>>  ClearPendingTransactions();
}