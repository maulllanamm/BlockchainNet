using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsQuery
{
    Result<List<Transaction>> GetPendingTransactions();
}