using DomoExtrato.Models;

namespace DomoExtrato.Interfaces
{
    public interface IPeriodosRepository
    {
        Task<IEnumerable<Periodos>> BuscarTodosPeriodosAsync();
    }
}
