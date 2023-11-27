using System.ComponentModel.DataAnnotations;

namespace PBL.Data.Dtos
{
    public class ReadColisaoDto
    {
        public int ColisaoId { get; set; }
        public double? VoParaColidir { get; set; }
        public double? AlturaColisao { get; set; }
        public double? TempoColisao { get; set; }
        public string? Movimento { get; set; }
        [Required]
        public int ProjetilId { get; set; }
        [Required]
        public int MeteoroId { get; set; }
    }
}
