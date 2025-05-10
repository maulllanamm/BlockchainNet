using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlocksService
{
    Block CreateGenesisBlock();
    Block CreateBlock(Block previousBlock, List<Transaction> transactions);
    string CalculateHash(Block block);
    void MineBlock(int difficulty, Block block);
}