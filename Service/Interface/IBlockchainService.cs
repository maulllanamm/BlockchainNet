using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainService
{
    Result<List<Block>> GetChain();
    Result<Block> GetLatestBlock();
    Result<Block> Mine(string minerAddress);
    Result<decimal> GetBalanceOfAddress(string address);
}