using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using BlockchainNet.Storage;

namespace BlockchainNet.Service.Implement;

public class BlockchainQueryService :  IBlockchainQuery
{
    private readonly List<Block> _chain;
    private readonly IBlocksCommand _blocksCommand;
    
    public BlockchainQueryService(IBlocksCommand blocksCommand)
    {
        _blocksCommand = blocksCommand;
        _chain = BlockchainStorage.Load() ?? new List<Block>();
        if (!_chain.Any())
        {
            var genesis = _blocksCommand.CreateGenesisBlock();
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