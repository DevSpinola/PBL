using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PBL.Models;
using System.Data;

namespace PBL.Data
{
    public class PblContext : DbContext
    {
        public PblContext(DbContextOptions<PblContext> options): base(options) { }

        public DbSet<ColisaoModel> Colisao { get; set; }
        public DbSet<MeteoroModel> Meteoro { get; set; }
        public DbSet<ProjetilModel> Projetil { get; set; }
        public int InserirProjetil(ProjetilModel projetil)
        {
            var novoProjetilIDParameter = new SqlParameter
            {
                ParameterName = "@novoProjetilID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            Database.ExecuteSqlRaw("EXEC sp_CreateProjetil @anguloGraus, @anguloRad, @novoProjetilID OUTPUT",
                                    new SqlParameter("anguloGraus", projetil.AnguloGraus),
                                    new SqlParameter("anguloRad", projetil.AnguloRad),
                                    novoProjetilIDParameter);

            // Obtendo o valor do parâmetro de saída
            int novoProjetilID = (int)novoProjetilIDParameter.Value;

            return novoProjetilID;
        }
        public int InserirMeteoro(MeteoroModel meteoro)
        {
            var novoMeteoroIDParameter = new SqlParameter
            {
                ParameterName = "@novoMeteoroID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            Database.ExecuteSqlRaw("EXEC sp_CreateMeteoro @alturaInicial, @velocidadeMeteoro, @distanciaHorizontal, @novoMeteoroID OUTPUT",
                                    new SqlParameter("alturaInicial", meteoro.AlturaInicial),
                                    new SqlParameter("velocidadeMeteoro", meteoro.VelocidadeMeteoro),
                                    new SqlParameter("distanciaHorizontal", meteoro.DistanciaHorizontal),
                                    novoMeteoroIDParameter);

            // Obtendo o valor do parâmetro de saída
            int novoMeteoroID = (int)novoMeteoroIDParameter.Value;

            return novoMeteoroID;
        }
        public int InserirColisao(ColisaoModel colisao)
        {
            var novaColisaoIDParameter = new SqlParameter
            {
                ParameterName = "@novaColisaoID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            // Verifica se os valores são nulos e define os parâmetros em conformidade
            var velocidadeInicialParameter = colisao.VoParaColidir.HasValue
                ? new SqlParameter("velocidadeInicial", colisao.VoParaColidir)
                : new SqlParameter("velocidadeInicial", DBNull.Value);

            var alturaColisaoParameter = colisao.AlturaColisao.HasValue
                ? new SqlParameter("alturaColisao", colisao.AlturaColisao)
                : new SqlParameter("alturaColisao", DBNull.Value);

            var tempoColisaoParameter = colisao.TempoColisao.HasValue
                ? new SqlParameter("tempoColisao", colisao.TempoColisao)
                : new SqlParameter("tempoColisao", DBNull.Value);

            var movimentoParameter = !string.IsNullOrEmpty(colisao.Movimento)
                ? new SqlParameter("movimento", colisao.Movimento)
                : new SqlParameter("movimento", DBNull.Value);

            Database.ExecuteSqlRaw("EXEC sp_CreateColisao @velocidadeInicial, @alturaColisao, @tempoColisao, @movimento, @projetilID, @meteoroID, @novaColisaoID OUTPUT",
                                    velocidadeInicialParameter,
                                    alturaColisaoParameter,
                                    tempoColisaoParameter,
                                    movimentoParameter,
                                    new SqlParameter("projetilID", colisao.ProjetilId),
                                    new SqlParameter("meteoroID", colisao.MeteoroId),
                                    novaColisaoIDParameter);

            // Obtendo o valor do parâmetro de saída
            int novaColisaoID = (int)novaColisaoIDParameter.Value;

            return novaColisaoID;
        }


    }
}
