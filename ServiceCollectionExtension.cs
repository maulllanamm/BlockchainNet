using BlockchainNet.Service.Implement;
using BlockchainNet.Service.Interface;

namespace BlockchainNet;

public static class ServiceCollectionExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IBlockchainMiner, BlockchainService>();
        services.AddScoped<IBlockchainReader, BlockchainService>();
            
        services.AddScoped<IBlocksFactory, BlocksService>();
        services.AddScoped<IBlocksHasher, BlocksService>();
        services.AddScoped<IBlocksMiner, BlocksService>();
        
        services.AddScoped<ITransactionsPool, TransactionsService>();
        services.AddScoped<ITransactionFactory, TransactionsService>();

        services.AddScoped<IAccountsPool, AccountsService>();
    }
}