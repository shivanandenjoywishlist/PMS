using PMS_BAL.IService.Amazon;
using PMS_BAL.IService.Common;
using PMS_BAL.IService.Flipkart;
using PMS_BAL.IService.Inventory;
using PMS_BAL.IService.Login;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Processor;
using PMS_BAL.IService.Product;
using PMS_BAL.Service.Common;
using PMS_BAL.Service.Inventory;
using PMS_BAL.Service.Login;
using PMS_BAL.Service.Order;
using PMS_BAL.Service.Product;
using PMS_DAL.IRepositories.Common;
using PMS_DAL.IRepositories.Login;
using PMS_DAL.IRepositories.Order;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_DAL.Repositories.Common;
using PMS_DAL.Repositories.Login;
using PMS_DAL.Repositories.Order;
using PMS_DAL.Repositories.Product;
using PMS_DAL.Repositories.ProviderSync;

public static class BuildUnityContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        
        // Register services
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped(typeof(IInventoryService<>), typeof(InventoryService<>));
        services.AddScoped<IInventoryServiceFactory,InventoryServiceFactory>();
        services.AddScoped<IRepositoryBaseFactory, RepositoryBaseFactory>();

        // Register repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ILoginRepositories, LoginRepositories>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IAmazonRepository, AmazonRepository>();
        services.AddScoped<IFlipKartRepository, FlipKartRepository>();


        // Register other services if needed
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<Amazon>();
        services.AddScoped<Flipkart>();
        services.AddScoped<IBaseService, BaseService>();

        return services;
       
    }
}
