using BlockchainNet.Helper;
using BlockchainNet.Service.Implement;
using BlockchainNet.Service.Interface;

namespace BlockchainNet;

public static class ServiceCollectionExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IBlockchainMiner, BlockchainMinerService>();
        services.AddScoped<IBlockchainReader, BlockchainReaderService>();
            
        services.AddScoped<IBlocksFactory, BlocksService>();
        services.AddScoped<IBlocksHasher, BlocksService>();
        services.AddScoped<IBlocksMiner, BlocksService>();
        
        services.AddScoped<ITransactionsPool, TransactionsesService>();
        services.AddScoped<ITransactionsFactory, TransactionsesService>();
        services.AddScoped<ITransactionsValidation, TransactionsesService>();

        services.AddScoped<IAccountsPool, AccountsService>();
        services.AddScoped<ICryptoHelper, CryptoHelper>();
    }
}