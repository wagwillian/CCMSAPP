using CCSMDataManager.Data;
using CCSMDataManager.Entities;
using CCSMDataManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CCSMDataManager.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly CCMSAPPDbContext _dbContext;

        public RelatorioService(CCMSAPPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRelatorioAsync(RelatorioSalaDto relatorioDto, Guid userId)
        {
            var relatorio = new RelatorioSala
            {                
                Turma = relatorioDto.Turma,
                HorarioAula = relatorioDto.HorarioAula,
                UserId = userId,
                Turno = relatorioDto.Turno,
                Barulho = relatorioDto.Barulho,
                Comentario = relatorioDto.Comentario,
                Data = relatorioDto.Data
            };

            _dbContext.Set<RelatorioSala>().Add(relatorio);
            await _dbContext.SaveChangesAsync();
        }
    }

    
}
