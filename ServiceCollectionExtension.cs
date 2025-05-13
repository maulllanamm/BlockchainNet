using BlockchainNet.Service.Implement;
using BlockchainNet.Service.Interface;

namespace BlockchainNet;

public static class ServiceCollectionExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IBlockchainMiner, BlockchainMinerService>();
        services.AddScoped<IBlockchainQuery, BlockchainQueryService>();
            
        services.AddScoped<IBlocksCommand, BlocksService>();
        services.AddScoped<IBlocksHasher, BlocksService>();
        services.AddScoped<IBlocksMiner, BlocksService>();
        
        services.AddScoped<ITransactionsCommand, TransactionsService>();
        services.AddScoped<ITransactionsQuery, TransactionsService>();
        services.AddScoped<ITransactionsValidation, TransactionsService>();

        services.AddScoped<IWalletsQuery, WalletsService>();
        services.AddScoped<IWalletsCommand, WalletsService>();
    }
}