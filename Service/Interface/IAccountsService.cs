using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IAccountsService
{
    Result<decimal> GetBalance(string address);
    Result<List<TransactionWithBlockInfo>> GetTransactions(string address);
}