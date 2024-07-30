using EstherTherapyMvcAuth.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(EstherTherapyMvcAuth.ConfigureDb))]

namespace EstherTherapyMvcAuth
{
    // Example Data Model
    public class MyTable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ConfigureDb : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureServices((context, services) => {
                services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                    context.Configuration.GetConnectionString("DefaultConnection")
                    ?? ":memory:",
                    SqliteDialect.Provider));
            })
            .ConfigureAppHost(afterConfigure:appHost => {
                appHost.ScriptContext.ScriptMethods.Add(new DbScriptsAsync());

                // Create non-existing Table and add Seed Data Example
                using var db = appHost.Resolve<IDbConnectionFactory>().Open();
                if (db.CreateTableIfNotExists<MyTable>())
                {
                    db.Insert(new MyTable { Name = "Seed Data for new MyTable" });
                }

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
                        ModifiedBy = "daniel",
                    });
                }
            });
    }
}
