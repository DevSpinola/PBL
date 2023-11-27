using PBL.Calculos;
using System.ComponentModel.DataAnnotations;

namespace PBL.Models
{
    public class ProjetilModel
    {
        [Key]
        [Required]
        public int ProjetilId { get; set; }        
        public double AnguloGraus { get; set; }
        public double AnguloRad { get; set; }        
        public virtual ICollection<ColisaoModel> Colisoes { get; set; }
        public ProjetilModel()
        {
            
        }
        public ProjetilModel( double anguloGraus)
        {            
            AnguloGraus = anguloGraus;           
            AnguloRad = CalculoAvancado.ConverteAngulo(AnguloGraus);
        }
    }
}
