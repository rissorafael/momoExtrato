using Dapper;
using DomoExtrato.Interfaces;
using DomoExtrato.Models;
using Npgsql;

namespace DomoExtrato.Infra.Data
{
    public class ExtratoRepository : IExtratoRepository
    {

        private IConfiguration _config;

        public ExtratoRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<Extrato>> BuscarExtratoPorPeriodoAsync(int periodoDias)
        {

            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(_config.GetConnectionString("MyPostgresConnection")))
            {

                string query = $@"SELECT 
                                                    Id,
                                                    Data,
                                                    TipoTransacao,
                                                    ValorMonetario
                                                  FROM Extrato
                                       WHERE Data BETWEEN CURRENT_DATE - INTERVAL '{periodoDias}' DAY AND CURRENT_DATE";

                var result = await npgsqlConnection.QueryAsync<Extrato>(query);

                return result;

            }

        }
    }
}

