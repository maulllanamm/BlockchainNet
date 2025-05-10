using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainReader
{
    Result<List<Block>> GetChain();
    Result<Block> GetLatestBlock();
}