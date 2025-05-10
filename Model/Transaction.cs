namespace BlockchainNet.Model;

public class Transaction
{
    public string Sender { get; set; }   
    public string Receiver { get; set; }   
    public decimal Amount { get; set; }   
}

public class TransactionWithBlockInfo
{
    public Transaction Transaction { get; set; }
    public string BlockHash { get; set; }
    public DateTime Timestamp { get; set; }
}