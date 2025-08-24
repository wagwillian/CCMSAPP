using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace CCSMDataManager.Entities
{
    public class RelatorioSala
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Turma { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string HorarioAula { get; set; } = string.Empty;    
        [MaxLength(200)]
        public string Disciplina { get; set; } = string.Empty;        
        [MaxLength(500)]
        public string Comentario { get; set; } = string.Empty;        
        [MaxLength(50)]        
        public DateTimeOffset Data { get; set; }
        [MaxLength(50)]
        public String Turno { get; set; }       
        
        public bool Barulho { get; set; }
        public Guid UserId { get; set; }
        
        public User User { get; set; } = new User(); // Navigation property to User


    }
}
