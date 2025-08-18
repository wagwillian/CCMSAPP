using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCSMDataManager.Entities;

namespace Portal.Authentication.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Turma { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Aula { get; set; } = string.Empty;
        [Required]
        public bool Barulho { get; set; }
        [Required]
        [MaxLength(200)]
        public string Disciplina { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Comentario { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public DateOnly Data { get; set; }       
      
    }
}
