using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsPool _transactionsPool;

    public TransactionsController(ITransactionsPool transactionsPool)
    {
        _transactionsPool = transactionsPool;
    }
    
    [HttpGet]
    [Route("pending")]
    public IActionResult GetTransactions()
    {
        var result = _transactionsPool.GetPendingTransactions();
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddTransaction(Transaction transaction)
    {
        var result = _transactionsPool.AddTransaction(transaction);
        return result.Success
        ? Ok(result)
        : StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete]
    public IActionResult DeleteTransaction()
    {
        var result = _transactionsPool.ClearPendingTransactions();
        return Ok(result);
    }
    
}