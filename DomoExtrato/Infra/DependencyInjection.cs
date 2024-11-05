using DomoExtrato.Infra.Data;
using DomoExtrato.Interfaces;
using DomoExtrato.Service;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;


namespace DomoExtrato.Infra
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IExtratoRepository, ExtratoRepository>();
            services.AddScoped<IExtratoService, ExtratoService>();
            services.AddScoped<IPeriodosRepository, PeriodosRepository>();
            services.AddScoped<IPeriodosService, PeriodosService>();

            ConfigureLogging();

        }

        public static void ConfigureLogging()
        {
            var environment = "Development";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSkin(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        static ElasticsearchSinkOptions ConfigureElasticSkin(IConfigurationRoot configuration, string environment)
        {
            var teste = configuration["ElasticConfiguration:Uri"];

            var elasticsearchSinkOptions = new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = configuration["ElasticConfiguration:Index"],
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };

            return elasticsearchSinkOptions;
        }

    }
}
