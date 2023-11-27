using System.ComponentModel.DataAnnotations;

namespace PBL.Models
{
    public class MeteoroModel
    {
        [Key]
        [Required]
        public int MeteoroId { get; set; }
        public int AlturaInicial { get; set; }
        public int VelocidadeMeteoro { get; set; }
        public int DistanciaHorizontal { get; set; }
        public virtual ICollection<ColisaoModel> Colisoes { get; set; }
        public MeteoroModel() { }

        public MeteoroModel(int h, int vm, int x)
        {
            AlturaInicial = h;
            VelocidadeMeteoro = vm;
            DistanciaHorizontal = x;
        }
    }
}
