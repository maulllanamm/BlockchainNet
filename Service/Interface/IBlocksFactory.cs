using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlocksFactory
{
    Block CreateGenesisBlock();
    Block CreateBlock(Block previousBlock, List<Transaction> transactions);
}