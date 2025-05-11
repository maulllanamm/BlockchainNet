using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsFactory
{
    Transaction CreateRewardTransaction(string receiver, decimal amount);
}