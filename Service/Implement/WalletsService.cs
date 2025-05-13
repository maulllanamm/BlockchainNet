using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Helper;
using BlockchainNet.Model;
using BlockchainNet.Service.Interface;

namespace BlockchainNet.Service.Implement;

public class WalletsService : IWalletsQuery ,IWalletsCommand
{
    private readonly IBlockchainQuery _blockchainQuery;
    private readonly IHelperHash _helperHash;

    public WalletsService(IBlockchainQuery blockchainQuery, IHelperHash helperHash)
    {
        _blockchainQuery = blockchainQuery;
        _helperHash = helperHash;
    }


    public Result<decimal> GetBalance(string address)
    {
        decimal balance = 0;
        var chainResult = _blockchainQuery.GetChain();
        foreach (var block in chainResult.Data)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.Sender == address)
                {
                    balance -= transaction.Amount;
                }

                if (transaction.Receiver == address)
                {
                    balance += transaction.Amount;
                }
            }
        }
        return Result<decimal>.Ok(balance);
    }

    public Result<List<TransactionWithBlockInfo>> GetTransactions(string address)
    {
        var chainResult = _blockchainQuery.GetChain();
        var transactions = chainResult
            .Data
            .SelectMany(t => t.Transactions.Select(x => new TransactionWithBlockInfo
            {
                Transaction = x,
                Timestamp = t.Timestamp,
                BlockHash = t.Hash
            }))
            .Where(tx => tx.Transaction.Sender == address || tx.Transaction.Receiver == address)
            .ToList();
        return Result<List<TransactionWithBlockInfo>>.Ok(transactions);
    }

    public Wallet GenerateKeyPair()
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var privateKey = Convert.ToBase64String(ecdsa.ExportECPrivateKey());
        var publicKey = Convert.ToBase64String(ecdsa.ExportSubjectPublicKeyInfo());
        return new Wallet
        {
            PrivateKey = privateKey,
            PublicKey = publicKey
        };
    }

    public string GenerateAddress(string publicKey)
    {
        return _helperHash.GenerateHash(publicKey);
    }

    public string GenerateSign(SignTransactionRequest signTransactionRequest, string base64PrivateKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportECPrivateKey(Convert.FromBase64String(base64PrivateKey), out _);

        var rawData = new string($"{signTransactionRequest.Sender}-{signTransactionRequest.Receiver}-{signTransactionRequest.Amount}");
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var signature = ecdsa.SignHash(hash);
        return Convert.ToBase64String(signature);
    }

    public bool VerifySign(Transaction transaction)
    {
        if (transaction.PublicKey == "SYSTEM" || transaction.Signature == "SYSTEM")
        {
            return true;
        }

        if (string.IsNullOrEmpty(transaction.Signature))
        {
            return false;
        }
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(transaction.PublicKey), out _);
        
        var rawTransaction = new string($"{transaction.Sender}-{transaction.Receiver}-{transaction.Amount}");
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(rawTransaction));
        var signature = Convert.FromBase64String(transaction.Signature);
        return ecdsa.VerifyHash(hash, signature);
    }
}