namespace BlockchainNet.Model;

public class BaseTransaction
{
    public string Sender { get; set; }   
    public string Receiver { get; set; }   
    public decimal Amount { get; set; }  
}
