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

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
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
    }
}
    
