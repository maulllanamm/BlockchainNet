using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    public IActionResult GetTransactions()
    {
        var result = _transactionService.GetPendingTransactions();
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddTransaction(Transaction transaction)
    {
        _transactionService.AddTransaction(transaction);
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteTransaction()
    {
        _transactionService.ClearPendingTransactions();
        return Ok();
    }
    
    
}