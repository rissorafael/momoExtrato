using DinkToPdf;
using DinkToPdf.Contracts;
using DomoExtrato.Interfaces;
using DomoExtrato.Models;
using Newtonsoft.Json;
using System.Text;

namespace DomoExtrato.Service
{
    public class ExtratoService : IExtratoService
    {
        private readonly IExtratoRepository _extratoRepository;
        private readonly ILogger<ExtratoService> _logger;
        private readonly IConverter _converter;

        public ExtratoService(IExtratoRepository extratoRepository, IConverter converter, ILogger<ExtratoService> logger)
        {
            _extratoRepository = extratoRepository;
            _converter = converter;
            _logger = logger;
        }

        public async Task<IEnumerable<Extrato>> BuscarExtratoPorPeriodoAsync(int periodoDias)
        {
            try
            {
                var extrato = await _extratoRepository.BuscarExtratoPorPeriodoAsync(periodoDias);
                return extrato;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[BuscarExtratoPorPeriodoAsync - BuscarExtratoPorPeriodo] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }

        public byte[] GerarPdfDeObjeto(string extrato)
        {
            var extratoList = JsonConvert.DeserializeObject<List<Extrato>>(extrato);

            var html = GerarHtmlDoRelatorio(extratoList);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color, Orientation = Orientation.Portrait, PaperSize = PaperKind.A4 },
                Objects = { new ObjectSettings() { HtmlContent = html, WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            return _converter.Convert(doc);
        }

        private string GerarHtmlDoRelatorio(List<Extrato> relatorio)
        {
            var sb = new StringBuilder();


            sb.AppendLine("<html><body>");
            sb.AppendLine("<h1>Relatorio Extratos</h1>");
            sb.AppendLine("<table border='1'>");


            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Data</th>");
            sb.AppendLine("<th>Valor Monetario</th>");
            sb.AppendLine("<th>Tipo Transacao</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");


            sb.AppendLine("<tbody>");
            foreach (var item in relatorio)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{item.Data}</td>");
                sb.AppendLine($"<td>{item.ValorMonetario:C}</td>");
                sb.AppendLine($"<td>{item.TipoTransacao}</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");


            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}