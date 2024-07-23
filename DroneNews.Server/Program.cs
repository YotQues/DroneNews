
using DroneNews.Model.Config;
using DroneNews.CronJobs;
using DroneNews.CommandHandlers;
using DroneNews.QueryHandlers;
using System.Globalization;

namespace DroneNews.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureSerivces(builder.Services, builder.Configuration);

            var app = builder.Build();

            ConfigureMiddlewares(app);

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }


        private static void ConfigureSerivces(IServiceCollection services, IConfiguration config)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllers();
            #region SwaggerGen
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #endregion SwaggerGen

            #region Model
            services.AddDroneNewsContext(config);
            #endregion Model

            services.AddSignalR();
            services.AddNewsAPI(config.GetValue<string>("APIKEY_NewsAPI") ?? throw new Exception("Could not find 'APIKEY_NewsAPI' configuration"));

            services.AddQueryHandlers();
            services.AddCommandHandlers();

            services.AddCronJobs();

        }

        private static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
