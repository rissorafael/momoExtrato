using DomoExtrato.Models;

namespace DomoExtrato.Interfaces
{
    public interface IExtratoService
    {
        Task<IEnumerable<Extrato>> BuscarExtratoPorPeriodoAsync(int periodoDias);
        byte[] GerarPdfDeObjeto(string extrato);
    }
}
