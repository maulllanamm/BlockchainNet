using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface ITransactionsValidation
{
    Result<Transaction>  Validate(Transaction transaction);
}