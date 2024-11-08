﻿using DomoExtrato.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomoExtrato.Controllers
{
    public class ExtratoController : Controller
    {
        private readonly IExtratoService _extratoService;
        private readonly IPeriodosService _periodosService;
        private readonly ILogger<ExtratoController> _logger;
        public ExtratoController(IExtratoService extratoService, IPeriodosService periodosService, ILogger<ExtratoController> logger)
        {
            _extratoService = extratoService;
            _periodosService = periodosService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ExtratoIndex()
        {
            var periodos = await _periodosService.BuscarTodosPeriodosAsync();
            ViewBag.Valores = new SelectList(periodos);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProcessarValor(int selectedValue)
        {
            var extrato = await _extratoService.BuscarExtratoPorPeriodoAsync(selectedValue);

            ViewBag.Extrato = extrato;
            return View("ResultadoExtrato");
        }

        [HttpPost]
        public IActionResult GerarPdf(string extrato)
        {
            var pdfBytes = _extratoService.GerarPdfDeObjeto(extrato);

            return File(pdfBytes, "application/pdf", "extrato.pdf");
        }
    }
}


