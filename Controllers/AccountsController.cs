using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsPool _accountsPool;
    public AccountsController(IAccountsPool accountsPool)
    {
        _accountsPool = accountsPool;
    }

    [HttpGet]
    [Route("{address}/balance")]
    public IActionResult GetBalance(string address)
    {
        var result = _accountsPool.GetBalance(address);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{address}/transactions")]
    public IActionResult GetTransactions(string address)
    {
        var result = _accountsPool.GetTransactions(address);
        return Ok(result);
    }
    
}