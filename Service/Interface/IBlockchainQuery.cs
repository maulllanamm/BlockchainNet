using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainQuery
{
    Result<List<Block>> GetChain();
    Result<Block> GetLatestBlock();
}