using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionService
{
    void AddTransaction(Transaction transaction);
    List<Transaction> GetPendingTransactions();
    void ClearPendingTransactions();
    Transaction CreateRewardTransaction(string miner, decimal rewardAmount);
}