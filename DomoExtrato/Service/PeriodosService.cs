using DomoExtrato.Infra.Data;
using DomoExtrato.Interfaces;
using DomoExtrato.Models;

namespace DomoExtrato.Service
{
    public class PeriodosService : IPeriodosService
    {
        private readonly IPeriodosRepository _periodosRepository;
        private readonly ILogger<PeriodosService> _logger;

        public PeriodosService(IPeriodosRepository periodosRepository)
        {
            _periodosRepository = periodosRepository;
        }
        public async Task<List<int>> BuscarTodosPeriodosAsync()
        {
            try
            {
                var periodos = await _periodosRepository.BuscarTodosPeriodosAsync();
                var periodoDias = periodos.Select(c => c.PeriodoDias).ToList();
               
                return periodoDias;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[PeriodosService - BuscarTodosPeriodosAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }
    }
}
