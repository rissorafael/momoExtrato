
namespace DomoExtrato.Interfaces
{
    public interface IPeriodosService
    {
        Task<List<int>> BuscarTodosPeriodosAsync();
    }
}
