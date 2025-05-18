using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/blocks")]
public class BlockchainController : ControllerBase
{
    private readonly IBlockchainMiner _blockchainMiner;
    private readonly IBlockchainQuery _blockchainQuery;

    public BlockchainController(IBlockchainMiner blockchainMiner, IBlockchainQuery blockchainQuery)
    {
        _blockchainMiner = blockchainMiner;
        _blockchainQuery = blockchainQuery;
    }
    
    [HttpGet]
    public IActionResult GetChain()
    {
        var result = _blockchainQuery.GetChain();
        return Ok(result);
    }    
    
    [HttpGet]
    [Route("latest")]
    public IActionResult GetLatestBlock()
    {
        var result = _blockchainQuery.GetLatestBlock();
        return Ok(result);
    }    
    
    [HttpPost]
    [Route("mine")]
    public IActionResult Mine([FromHeader(Name = "x-miner-address")] string minerAddress)
    {
        var result = _blockchainMiner.Mine(minerAddress);
        return Ok(result);
    }
    
}