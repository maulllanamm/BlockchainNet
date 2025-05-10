using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountsService;
    public AccountsController(IAccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    [HttpGet]
    [Route("{address}/balance")]
    public IActionResult GetBalance(string address)
    {
        var result = _accountsService.GetBalance(address);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{address}/transactions")]
    public IActionResult GetTransactions(string address)
    {
        var result = _accountsService.GetTransactions(address);
        return Ok(result);
    }
    
}