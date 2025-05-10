using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlocksMiner
{
    void MineBlock(int difficulty, Block block);
}