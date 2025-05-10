using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionService
{
    void AddTransaction(Transaction transaction);
    Result<List<Transaction>> GetPendingTransactions();
    void ClearPendingTransactions();
    Transaction CreateRewardTransaction(string miner, decimal rewardAmount);
}