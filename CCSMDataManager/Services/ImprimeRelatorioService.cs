
using CCSMDataManager.Data;
using CCSMDataManager.Entities;
using CCSMDataManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CCSMDataManager.Services
{
    public class ImprimeRelatoriosService : IImprimeRelatoriosService
    {
        private readonly CCMSAPPDbContext _context;

        public ImprimeRelatoriosService(CCMSAPPDbContext context)
        {
            _context = context;
        }


        public async Task<FileStream> OnPostExportarAsync(RelatorioImprimirDto relatorioImprimirDto)
        {
            // Ação de POST para exportar os dados filtrados
            var relatorios = await GetRelatorios(relatorioImprimirDto);


            // Gera o arquivo Excel
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatórios de Sala");

                // Cabeçalhos
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Turma";
                worksheet.Cells[1, 3].Value = "Data";
                worksheet.Cells[1, 4].Value = "Horário da Aula";
                worksheet.Cells[1, 5].Value = "Disciplina";
                worksheet.Cells[1, 6].Value = "Turno";
                worksheet.Cells[1, 7].Value = "Comentário";
                worksheet.Cells[1, 8].Value = "Barulho";

                // Preenche os dados
                for (int i = 0; i < relatorios.Count; i++)
                {
                    var rel = relatorios[i];
                    worksheet.Cells[i + 2, 1].Value = rel.Id;
                    worksheet.Cells[i + 2, 2].Value = rel.Turma;
                    worksheet.Cells[i + 2, 3].Value = rel.Data.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 4].Value = rel.HorarioAula;
                    worksheet.Cells[i + 2, 5].Value = rel.Disciplina;
                    worksheet.Cells[i + 2, 6].Value = rel.Turno;
                    worksheet.Cells[i + 2, 7].Value = rel.Comentario;
                    worksheet.Cells[i + 2, 8].Value = rel.Barulho ? "Sim" : "Não";
                }

                package.Save();
            }

            stream.Position = 0;
            string excelName = $"Relatorios_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            // Create a FileStream for writing
            using (FileStream fileStream = File.Open("output.txt", FileMode.Create, FileAccess.Write))
            {
                // Reset the position of the MemoryStream to the beginning
                stream.Position = 0;
                // Copy the data from the MemoryStream to the FileStream
                await stream.CopyToAsync(fileStream);
                return fileStream;
            }

        }

        private async Task<List<RelatorioSala>> GetRelatorios(RelatorioImprimirDto relatorioImprimirDto)
        {
            var query = _context.RelatorioSalas.AsQueryable();

            if (!string.IsNullOrEmpty(relatorioImprimirDto.Turma))
            {
                query = query.Where(r => r.Turma.Contains(relatorioImprimirDto.Turma));
            }

            if (relatorioImprimirDto.DataInicio != default(DateTime))
            {
                query = query.Where(r => r.Data >= relatorioImprimirDto.DataInicio);
            }

            if (relatorioImprimirDto.DataFim != default(DateTime))
            {
                // Adiciona 1 dia para incluir o dia de fim na busca
                query = query.Where(r => r.Data <= relatorioImprimirDto.DataFim.AddDays(1));
            }

            return await query.ToListAsync();
        }
    }
}
