using DomoExtrato.Models;

namespace DomoExtrato.Interfaces
{
    public interface IExtratoRepository
    {
        Task<IEnumerable<Extrato>> BuscarExtratoPorPeriodoAsync(int periodoDias);
    }
}
