using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IBlockchainService
{
    List<Block> GetChain();
    Block GetLatestBlock();
    void Mine(string minerAddress);
    bool VerifyChain();
}