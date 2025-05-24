using BlockchainNet.Helper;
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
        
        services.AddScoped<TransactionsService>(); // ✅ satu instance

        // Semua interface diarahkan ke instance yang sama
        services.AddScoped<ITransactionsCommand>(sp => sp.GetRequiredService<TransactionsService>());
        services.AddScoped<ITransactionsQuery>(sp => sp.GetRequiredService<TransactionsService>());
        services.AddScoped<ITransactionsValidation>(sp => sp.GetRequiredService<TransactionsService>());

        services.AddScoped<IWalletsQuery, WalletsService>();
        services.AddScoped<IWalletsCommand, WalletsService>();
        
        services.AddScoped<IHelperHash, HelperService>();
    }
}