using BlockchainNet.Helper;
using BlockchainNet.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/helpers")]
public class HelpersController : ControllerBase
{
    private readonly ICryptoHelper _cryptoHelper;

    public HelpersController(ICryptoHelper cryptoHelper)
    {
        _cryptoHelper = cryptoHelper;
    }
    
    [HttpGet]
    [Route("generate/keypair")]
    public IActionResult GenerateKeyPair()
    {
        var result = _cryptoHelper.GenerateKeyPair();
        return Ok(result);
    }
    
    [HttpGet]
    [Route("generate/address")]
    public IActionResult GenerateAddress(string publicKey)
    {
        var result = _cryptoHelper.GenerateAddress(publicKey);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("sign")]
    public IActionResult Sign(SignTransactionRequest signTransactionRequest, string base64PrivateKey)
    {
        var result = _cryptoHelper.Sign(signTransactionRequest, base64PrivateKey);
        return Ok(result);
    }
    
}