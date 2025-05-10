using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }
    
    [HttpGet]
    [Route("pending")]
    public IActionResult GetTransactions()
    {
        var result = _transactionsService.GetPendingTransactions();
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddTransaction(Transaction transaction)
    {
        var result = _transactionsService.AddTransaction(transaction);
        return Ok(result);
    }
    
    [HttpDelete]
    public IActionResult DeleteTransaction()
    {
        var result = _transactionsService.ClearPendingTransactions();
        return Ok(result);
    }
    
}