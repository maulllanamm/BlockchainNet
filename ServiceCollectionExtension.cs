using BlockchainNet.Service.Implement;
using BlockchainNet.Service.Interface;

namespace BlockchainNet;

public static class ServiceCollectionExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IBlockchainService, BlockchainService>();
        services.AddScoped<IBlocksService, BlocksesService>();
        services.AddScoped<ITransactionsService, TransactionsService>();
        services.AddScoped<IAccountsService, AccountsService>();
    }
}