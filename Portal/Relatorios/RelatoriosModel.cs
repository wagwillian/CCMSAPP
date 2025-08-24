using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; // Certifique-se de ter a referência ao pacote EPPlus
using CCSMDataManager.Data;
using CCSMDataManager.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portal.Relatorios;



        public class RelatoriosModel : PageModel
        {
            private readonly CCMSAPPDbContext _context;

            public RelatoriosModel(CCMSAPPDbContext context)
            {
                _context = context;
            }

            [BindProperty(SupportsGet = true)]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Início")]
            public DateTime DataInicio { get; set; } = DateTime.Today;

            [BindProperty(SupportsGet = true)]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Fim")]
            public DateTime DataFim { get; set; } = DateTime.Today;

            [BindProperty(SupportsGet = true)]
            [Display(Name = "Turma")]
            public string Turma { get; set; } = string.Empty;

            public List<RelatorioSala> Relatorios { get; set; } = new List<RelatorioSala>();

            public async Task OnGetAsync()
            {
                // Na primeira carga, exibe relatórios do dia atual
                await CarregarRelatorios();
            }

            public async Task<IActionResult> OnPostFiltrarAsync()
            {
                // Ação de POST para filtrar com base nos parâmetros
                await CarregarRelatorios();
                return Page();
            }

            public async Task<IActionResult> OnPostExportarAsync()
            {
                // Ação de POST para exportar os dados filtrados
                await CarregarRelatorios();

                if (!Relatorios.Any())
                {
                    // Retorna à página com mensagem se não houver dados para exportar
                    TempData["Mensagem"] = "Nenhum relatório encontrado para exportar.";
                    return Page();
                }

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
                    for (int i = 0; i < Relatorios.Count; i++)
                    {
                        var rel = Relatorios[i];
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

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

            private async Task CarregarRelatorios()
            {
                var query = _context.RelatorioSalas.AsQueryable();

                if (!string.IsNullOrEmpty(Turma))
                {
                    query = query.Where(r => r.Turma.Contains(Turma));
                }

                if (DataInicio != default(DateTime))
                {
                    query = query.Where(r => r.Data >= DataInicio);
                }

                if (DataFim != default(DateTime))
                {
                    // Adiciona 1 dia para incluir o dia de fim na busca
                    query = query.Where(r => r.Data <= DataFim.AddDays(1));
                }

                Relatorios = await query.ToListAsync();
            }
}
    



