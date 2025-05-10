using BlockchainNet.Service.Implement;
using BlockchainNet.Service.Interface;

namespace BlockchainNet;

public static class ServiceCollectionExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IBlockchainService, BlockchainService>();
        services.AddScoped<IBlockService, BlockService>();
    }
}