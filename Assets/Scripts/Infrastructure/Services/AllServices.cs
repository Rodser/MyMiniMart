namespace Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance =new AllServices());

        public void RegisterSingle<TService>(TService inmlementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = inmlementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }
        
        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}