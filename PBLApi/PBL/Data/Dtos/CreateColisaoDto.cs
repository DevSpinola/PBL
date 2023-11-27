using System.ComponentModel.DataAnnotations;

namespace PBL.Data.Dtos
{
    public class CreateColisaoDto
    {
        [Required]
        public int ProjetilId { get; set; }
        [Required]
        public int MeteoroId { get; set; }
    }
}
