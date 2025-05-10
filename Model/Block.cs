namespace BlockchainNet.Model;

public class Block
{
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public int nonce { get; set; } = 0;
}
