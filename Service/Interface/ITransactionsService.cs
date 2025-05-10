using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsService
{
    Result<Transaction>  AddTransaction(Transaction transaction);
    Result<List<Transaction>> GetPendingTransactions();
    Result<List<Transaction>>  ClearPendingTransactions();
    Transaction CreateRewardTransaction(string miner, decimal rewardAmount);
}