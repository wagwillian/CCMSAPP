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
        [Required]
        [MaxLength(200)]
        public string eMail { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Disciplina { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Comentario { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]        
        public DateOnly Data { get; set; }
        [Required]
        [MaxLength(200)]
        public User User { get; set; } = new User(); // Navigation property to User


    }
}
