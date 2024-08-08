using Funq;
using ServiceStack;
using EstherTherapyMvcAuth.ServiceInterface;

[assembly: HostingStartup(typeof(EstherTherapyMvcAuth.AppHost))]

namespace EstherTherapyMvcAuth;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("EstherTherapyMvcAuth", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
                UseSameSiteCookies = true,
#if DEBUG                
                AdminAuthSecret = "secretoff", // Enable Admin Access with ?authsecret=secretoff
#endif
        });
    }
}
