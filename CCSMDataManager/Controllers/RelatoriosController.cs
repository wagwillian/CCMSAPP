using CCSMDataManager.Models;
using CCSMDataManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCSMDataManager.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;
        private readonly IImprimeRelatoriosService _imprimeRelatoriosService;

        public RelatoriosController(IRelatorioService relatorioService, IImprimeRelatoriosService imprimeRelatoriosService)
        {
            _relatorioService = relatorioService;
            _imprimeRelatoriosService = imprimeRelatoriosService;
        }

        [HttpPost]        
        public async Task<IActionResult> Post([FromBody] RelatorioSalaDto relatorioSalaDto)
        {
            var userId = GetUserIdFromClaims();
            await _relatorioService.AddRelatorioAsync(relatorioSalaDto, userId);
            return Ok();
        }
        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

        [HttpPost("/imprimir")]
        public async Task<IActionResult> Imprimir([FromBody] RelatorioImprimirDto relatorioImprimirDto)
        {
            var fileStream = await _imprimeRelatoriosService.OnPostExportarAsync(relatorioImprimirDto);
            fileStream.Position = 0; // Reset stream position
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "RelatoriosSala.xlsx";
            return File(fileStream, contentType, fileName);
        }
    }
}
    
