using CCSMDataManager.Models;

namespace CCSMDataManager.Services
{
    public interface IRelatorioService
    {
        Task AddRelatorioAsync(RelatorioSalaDto relatorioDto, Guid userId);
    }
}