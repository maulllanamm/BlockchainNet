namespace BlockchainNet.Model;

public class Transaction : BaseTransaction
{
    public string PublicKey { get; set; }   // base64 ECDSA public key
    public string Signature { get; set; }   // base64 ECDSA signature
}
public class SignTransactionRequest : BaseTransaction
{
    public string PublicKey { get; set; }   // base64 ECDSA public key
}

public class TransactionWithBlockInfo
{
    public Transaction Transaction { get; set; }
    public string BlockHash { get; set; }
    public DateTime Timestamp { get; set; }
}


