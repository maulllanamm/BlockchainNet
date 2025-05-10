using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionFactory
{
    Transaction CreateRewardTransaction(string receiver, decimal amount);
}