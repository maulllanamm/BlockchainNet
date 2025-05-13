using System.Security.Cryptography;
using System.Text;

namespace BlockchainNet.Helper;

public class HelperService : IHelperHash
{
    public string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }
}