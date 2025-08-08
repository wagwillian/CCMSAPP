namespace CCSMDataManager.Models
{
    public class RelatorioSalaDto
    {
        public int Id { get; set; }
        public string Turma { get; set; } = string.Empty;
        public string HorarioAula { get; set; } = string.Empty;
        public string eMail { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;  
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
