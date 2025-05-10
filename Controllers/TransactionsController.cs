using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    [Route("pending")]
    public IActionResult GetTransactions()
    {
        var result = _transactionService.GetPendingTransactions();
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddTransaction(Transaction transaction)
    {
        var result = _transactionService.AddTransaction(transaction);
        return Ok(result);
    }
    
    [HttpDelete]
    public IActionResult DeleteTransaction()
    {
        var result = _transactionService.ClearPendingTransactions();
        return Ok(result);
    }
    
}