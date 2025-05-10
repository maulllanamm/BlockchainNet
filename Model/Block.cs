namespace BlockchainNet.Model;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public int nonce { get; set; } = 0;
}

public class NewBlock
{
    public int Index { get; set; }
    public List<Transaction> Transactions { get; set; }
}