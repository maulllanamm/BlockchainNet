using BlockchainNet.Model;

namespace BlockchainNet.Helper;

public interface ICryptoHelper
{
    KeyPair GenerateKeyPair();
    string GenerateAddress(string publicKey);
    string Sign(SignTransactionRequest signTransactionRequest, string base64PrivateKey);
    bool Verify(Transaction transaction);
    
}