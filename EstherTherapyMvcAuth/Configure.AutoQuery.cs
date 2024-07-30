using ServiceStack;

[assembly: HostingStartup(typeof(EstherTherapyMvcAuth.ConfigureAutoQuery))]

namespace EstherTherapyMvcAuth;

public class ConfigureAutoQuery : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddPlugin(new AutoQueryFeature {
                MaxLimit = 1000,
                //IncludeTotal = true,
            });
        });
}
