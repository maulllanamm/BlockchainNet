using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlocksHasher
{
    string CalculateHash(Block block);
}