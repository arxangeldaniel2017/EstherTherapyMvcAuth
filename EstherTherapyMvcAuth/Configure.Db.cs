using EstherTherapyMvcAuth.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(EstherTherapyMvcAuth.ConfigureDb))]

namespace EstherTherapyMvcAuth;

public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder

        .ConfigureServices((context, services) => {
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                Environment.GetEnvironmentVariable("DATABASE_CONNECTION"), MySqlDialect.Provider));
        })
        .ConfigureAppHost(appHost => {
            // Enable built-in Database Admin UI at /admin-ui/database
            //appHost.Plugins.Add(new AdminDatabaseFeature());

            using var db = appHost.Resolve<IDbConnectionFactory>().Open();
            if (db.CreateTableIfNotExists<Booking>())
            {
                // Seed data
                db.Insert(new Booking
                {
                    Name = "Test",
                    Cost = 123,
                    RoomNumber = 321,
                    RoomType = RoomType.Queen,
                    Notes = "Testing more",
                    BookingStartDate = new DateTime(2022, 1, 1),
                    BookingEndDate = new DateTime(2022, 1, 5),
                    CreatedBy = "daniel",
                    //CreatedDate = DateTime.Now,
                    ModifiedBy = "daniel",
                });
            }
        });
}
