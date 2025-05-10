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
        return Ok(_blockchainService.GetChain());
    }
    
    [HttpGet]
    [Route("verify")]
    public IActionResult VerifyChain()
    {
        return Ok(_blockchainService.VerifyChain());
    }
    
    [HttpPost]
    public IActionResult Add(NewBlock newBlock)
    {
        _blockchainService.AddBlock(newBlock);
        return Ok();
    }
    
}