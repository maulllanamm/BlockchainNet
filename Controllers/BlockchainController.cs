using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/blocks")]
public class BlockchainController : ControllerBase
{
    private readonly IBlockchainMiner _blockchainMiner;
    private readonly IBlockchainReader _blockchainReader;

    public BlockchainController(IBlockchainMiner blockchainMiner, IBlockchainReader blockchainReader)
    {
        _blockchainMiner = blockchainMiner;
        _blockchainReader = blockchainReader;
    }
    
    [HttpGet]
    public IActionResult GetChain()
    {
        var result = _blockchainReader.GetChain();
        return Ok(result);
    }    
    
    [HttpGet]
    [Route("latest")]
    public IActionResult GetLatestBlock()
    {
        var result = _blockchainReader.GetLatestBlock();
        return Ok(result);
    }    
    
    [HttpPost]
    [Route("mine")]
    public IActionResult Mine(string minerAddress)
    {
        var result = _blockchainMiner.Mine(minerAddress);
        return Ok(result);
    }
    
}