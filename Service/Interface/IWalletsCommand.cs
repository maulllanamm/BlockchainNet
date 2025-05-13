using BlockchainNet.Model;

namespace BlockchainNet.Service.Interface;

public interface IWalletsCommand
{
    Wallet GenerateKeyPair();
    string GenerateAddress(string publicKey);
    string GenerateSign(SignTransactionRequest signTransactionRequest, string base64PrivateKey);
    bool VerifySign(Transaction transaction);
}