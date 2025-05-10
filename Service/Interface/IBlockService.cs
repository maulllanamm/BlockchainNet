using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockService
{
    Block CreateGenesisBlock();
    string CalculateHash(Block block);
    bool MineBlock(int difficulty, Block block);
}