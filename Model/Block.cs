namespace BlockchainNet.Model;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
}

public class NewBlock
{
    public int Index { get; set; }
    public List<Transaction> Transactions { get; set; }
}