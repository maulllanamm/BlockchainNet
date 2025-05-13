using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlocksCommand
{
    Block CreateGenesisBlock();
    Block CreateBlock(Block previousBlock, List<Transaction> transactions);
}