using BlockchainNet.Model;
using BlockchainNet.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainNet.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsCommand _transactionsCommand;
    private readonly ITransactionsQuery _transactionsQuery;

    public TransactionsController(ITransactionsCommand transactionsCommand, ITransactionsQuery transactionsQuery)
    {
        _transactionsCommand = transactionsCommand;
        _transactionsQuery = transactionsQuery;
    }
    
    [HttpGet]
    [Route("pending")]
    public IActionResult GetTransactions()
    {
        var result = _transactionsQuery.GetPendingTransactions();
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddTransaction(Transaction transaction)
    {
        var result = _transactionsCommand.AddTransaction(transaction);
        return result.Success
        ? Ok(result)
        : StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete]
    public IActionResult DeleteTransaction()
    {
        var result = _transactionsCommand.ClearPendingTransactions();
        return Ok(result);
    }
    
}