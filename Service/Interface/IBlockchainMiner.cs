using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainMiner
{
    Result<Block> Mine(string minerAddress);
}