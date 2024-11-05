using DomoExtrato.Interfaces;
using DomoExtrato.Models;
using DomoExtrato.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DomoExtratoTest
{
    public class ExtratoServiceTest
    {
        private readonly Mock<IExtratoRepository> _mockExtratoRepository;
        private readonly Mock<ILogger<ExtratoService>> _mockLogger;
        private readonly ExtratoService _extratoService;

        // Não deu tempo de validar
        [Fact]
        public async Task BuscarExtratoPorPeriodoAsync_DeveRetornarExtratos()
        {
            var periodoDias = 30;
            var expectedExtratos = new List<Extrato>
    {
        new Extrato { Id = 1, Data = DateTime.Now, TipoTransacao = "PIX", ValorMonetario = 100.0m },
        new Extrato { Id = 2, Data = DateTime.Now.AddDays(-1), TipoTransacao = "TED", ValorMonetario = 50.0m }
    };

            _mockExtratoRepository
                .Setup(repo => repo.BuscarExtratoPorPeriodoAsync(periodoDias))
                .ReturnsAsync(expectedExtratos);


            var result = await _extratoService.BuscarExtratoPorPeriodoAsync(periodoDias);


            Assert.NotNull(result);
            Assert.Equal(expectedExtratos.Count, result.Count());  // Verifica o número de elementos

            foreach (var expectedExtrato in expectedExtratos)
            {
                var correspondingResult = result.FirstOrDefault(r => r.Id == expectedExtrato.Id);

                Assert.NotNull(correspondingResult);

                Assert.Equal(expectedExtrato.Id, correspondingResult.Id);
                Assert.Equal(expectedExtrato.TipoTransacao, correspondingResult.TipoTransacao);

                Assert.True(Math.Abs((expectedExtrato.Data - correspondingResult.Data).TotalSeconds) < 1);
            }
        }
    }
}