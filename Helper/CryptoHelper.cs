using System.Security.Cryptography;
using System.Text;
using BlockchainNet.Model;

namespace BlockchainNet.Helper;

public class CryptoHelper : ICryptoHelper
{
    public KeyPair GenerateKeyPair()
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var privateKey = Convert.ToBase64String(ecdsa.ExportECPrivateKey());
        var publicKey = Convert.ToBase64String(ecdsa.ExportSubjectPublicKeyInfo());
        return new KeyPair
        {
            PrivateKey = privateKey,
            PublicKey = publicKey
        };
    }

    public string GenerateAddress(string publicKey)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(publicKey);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }

    public string Sign(SignTransactionRequest signTransactionRequest, string base64PrivateKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportECPrivateKey(Convert.FromBase64String(base64PrivateKey), out _);

        var rawData = new string($"{signTransactionRequest.Sender}-{signTransactionRequest.Receiver}-{signTransactionRequest.Amount}");
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var signature = ecdsa.SignHash(hash);
        return Convert.ToBase64String(signature);
    }

    public bool Verify(Transaction transaction)
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