using Joboard.Entities.Customer;
using Nest;

namespace Joboard.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(
            this IServiceCollection service, IConfiguration configuration
        )
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                                   .PrettyJson()
                                   .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            service.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            //settings.DefaultMappingFor<Job>(p =>
            //p.Ignore(x => x.Email)
            //  .Ignore(x => x.IsPremium));
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<User>(x => x.AutoMap()));
        }
    }
}
