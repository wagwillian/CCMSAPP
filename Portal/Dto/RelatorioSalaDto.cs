namespace Portal.Dto
{
    public class RelatorioSalaDto
    {
        public int Id { get; set; }
        public string Turma { get; set; } = string.Empty;
        public string Turno { get; set; }
        public bool Barulho { get; set; }
        public string HorarioAula { get; set; } = string.Empty;
        public string eMail { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public DateTimeOffset Data { get; set; } = DateTimeOffset.Now;
    }
}
