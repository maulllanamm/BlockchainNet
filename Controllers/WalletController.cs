using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/wallets")]
public class WalletController : ControllerBase
{
    private readonly IWalletsQuery _walletsQuery;
    private readonly IWalletsCommand _walletsCommand;
    public WalletController(IWalletsQuery walletsQuery, IWalletsCommand walletsCommand)
    {
        _walletsQuery = walletsQuery;
        _walletsCommand = walletsCommand;
    }

    [HttpGet]
    [Route("{address}/balance")]
    public IActionResult GetBalance([FromHeader] string address)
    {
        var result = _walletsQuery.GetBalance(address);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{address}/transactions")]
    public IActionResult GetTransactions([FromHeader(Name = "x-address")] string address)
    {
        var result = _walletsQuery.GetTransactions(address);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("keypair")]
    public IActionResult GenerateKeyPair()
    {
        var result = _walletsCommand.GenerateKeyPair();
        return Ok(result);
    }
    
    [HttpPost]
    [Route("address")]
    public IActionResult GenerateAddress([FromHeader(Name = "x-public-key")] string publicKey)
    {
        var result = _walletsCommand.GenerateAddress(publicKey);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("sign")]
    public IActionResult Sign([FromBody]BaseTransaction signTransactionRequest, [FromHeader(Name = "x-private-key")] string base64PrivateKey)
    {
        var result = _walletsCommand.GenerateSign(signTransactionRequest, base64PrivateKey);
        return Ok(result);
    }
    
}