using Marvin.Cache.Headers;

namespace DenemeTakipAPI.API
{
    public static class ServiceRegistration
    {
        public static void AddPresentationServices(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expirationOptions =>
            {
                expirationOptions.MaxAge = 300;
                expirationOptions.CacheLocation = CacheLocation.Public;
            },
            validationOptions =>
            {
                validationOptions.MustRevalidate = true;
            });
            services.AddResponseCaching();
        }
    }
}
