using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainReaderService :  IBlockchainReader
{
    private readonly List<Block> _chain;
    private readonly IBlocksFactory _blocksFactory;
    
    public BlockchainReaderService(IBlocksFactory blocksFactory)
    {
        _blocksFactory = blocksFactory;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksFactory.CreateGenesisBlock();
            _chain.Add(genesis);
            BlockchainStorage.Save(_chain);    
        }
    }

    public Result<List<Block>> GetChain()
    {
        return Result<List<Block>>.Ok(_chain);
    }

    public Result<Block> GetLatestBlock()
    {
        return Result<Block>.Ok(_chain.Last());
    }
}