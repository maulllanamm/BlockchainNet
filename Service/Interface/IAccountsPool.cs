using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IAccountsPool
{
    Result<decimal> GetBalance(string address);
    Result<List<TransactionWithBlockInfo>> GetTransactions(string address);
}