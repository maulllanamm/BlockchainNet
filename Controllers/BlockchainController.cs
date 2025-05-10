using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/blockchain")]
public class BlockchainController : ControllerBase
{
    private readonly IBlockchainService _blockchainService;

    public BlockchainController(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }
    
    [HttpGet]
    public IActionResult GetChain()
    {
        var result = _blockchainService.GetChain();
        return Ok(result);
    }
    
    [HttpPost]
    [Route("mine")]
    public IActionResult Mine(string minerAddress)
    {
        var result = _blockchainService.Mine(minerAddress);
        return Ok(result);
    }
    
}