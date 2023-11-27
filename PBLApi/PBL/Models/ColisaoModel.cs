using PBL.Models;
using System.ComponentModel.DataAnnotations;

public class ColisaoModel
{
    [Key]
    [Required]
    public int ColisaoId { get; set; }
    public double? VoParaColidir { get; set; }
    public double? AlturaColisao { get; set; }
    public double? TempoColisao { get; set; }
    public string? Movimento { get; set; }
    [Required]
    public int ProjetilId { get; set; }
    [Required]
    public int MeteoroId { get; set; }
    public virtual ProjetilModel Projetil { get; set; }
    public virtual MeteoroModel Meteoro { get; set; }

    public ColisaoModel()
    {
        // Construtor sem parâmetros
    }

    public ColisaoModel(double? y, double? t, double? vo, string? mov, int projetilId, int meteoroId)
    {
        AlturaColisao = y;
        TempoColisao = t;
        VoParaColidir = vo;
        Movimento = mov;
        ProjetilId = projetilId;
        MeteoroId = meteoroId;
    }
}
