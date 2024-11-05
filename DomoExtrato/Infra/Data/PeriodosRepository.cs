using Dapper;
using DomoExtrato.Interfaces;
using DomoExtrato.Models;
using Npgsql;

namespace DomoExtrato.Infra.Data
{
    public class PeriodosRepository : IPeriodosRepository
    {
        private IConfiguration _config;

        public PeriodosRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<Periodos>> BuscarTodosPeriodosAsync()
        {

            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(_config.GetConnectionString("MyPostgresConnection")))
            {
                var result = await npgsqlConnection.QueryAsync<Periodos>(@"
                                                    SELECT 
                                                       Id
                                                      ,PeriodoDias
                                                    FROM Periodos
                                                      Where 1=1");
                return result;

            }
        }
    }
}
