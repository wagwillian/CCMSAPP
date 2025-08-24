using CCSMDataManager.Models;

namespace CCSMDataManager.Services
{
    public interface IImprimeRelatoriosService
    {
        Task<FileStream> OnPostExportarAsync(RelatorioImprimirDto relatorioImprimirDto);
    }
}