using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainService
{
    List<Block> GetChain();
    Block GetLatestBlock();
    void AddBlock(NewBlock newBlock);
    bool VerifyChain();
}