
/* Integrantes do grupo:
	
	Bruna Fernandes Menezes 082220031
	Guilherme Spinola dos Santos 082220025
	James Mauch dos Santos Junior 082220029
	Luisa Jacob Poveda 082220006

*/
USE MASTER
GO
DROP DATABASE IF EXISTS PBL
GO
CREATE DATABASE PBL
GO
USE PBL
GO


/* Tabela Projétil  */

CREATE TABLE [dbo].[Projetil](
	[ProjetilId] [int] identity (1,1) NOT NULL Primary Key,	
	[AnguloGraus] [float] NOT NULL,
	[AnguloRad] [float] NOT NULL)


/*  Tabela Meteoro  */

CREATE TABLE [dbo].[Meteoro](
	[MeteoroId] [int] identity (1,1) NOT NULL Primary Key,
	[AlturaInicial] [int] NOT NULL,
	[VelocidadeMeteoro] [int] NOT NULL,
	[DistanciaHorizontal] [int] NOT NULL)



/*  Tabela Colisão  */

CREATE TABLE [dbo].[Colisao](
	[ColisaoId] [int] identity (1,1) NOT NULL Primary Key,
	[Colidiu] [bit] NULL,
	[VoParaColidir] [float] NULL,
	[AlturaColisao] [float] NULL,
	[TempoColisao] [float] NULL,
	[Movimento] [varchar](20) NULL,
	[ProjetilId] [int] NOT NULL,
	[MeteoroId] [int] NOT NULL,
	Foreign Key (projetilId) references Projetil (projetilId),
	Foreign Key (meteoroId) references Meteoro (meteoroId)
)

/*  Tabela Log  */

CREATE TABLE [dbo].[LogPBL](
	[LogId] [int] identity (1,1) NOT NULL,
	[Data]  [smalldatetime] NOT NULL,
	[Descricao] [varchar](100)
)

--Stored Procedures para a Tabela Projetil
GO
Create or alter procedure sp_CreateProjetil(@anguloGraus float, @anguloRad float, @idCriado int out ) as 
begin
     insert into Projetil(anguloGraus, anguloRad)
	 values (@anguloGraus, @anguloRad)
	 set @idCriado = SCOPE_IDENTITY()
end
GO
Create or alter procedure sp_ReadProjetil as 
begin
     select * from Projetil
end
GO

--Stored Procedures para a Tabela Meteoro

Create or alter procedure sp_CreateMeteoro(@alturaInicial int, @velocidadeMeteoro int, @distanciaHorizontal int, @idCriado int out ) as 
begin
     insert into Meteoro(alturaInicial, velocidadeMeteoro, DistanciaHorizontal)
	 values (@alturaInicial, @velocidadeMeteoro, @distanciaHorizontal)
	 set @idCriado = SCOPE_IDENTITY()
end
GO
Create or alter procedure sp_ReadMeteoro as 
begin
     select * from Meteoro
end
GO

--Stored Procedures para a Tabela Colisão

Create or alter procedure sp_CreateColisao(@velocidadeInicial float, @alturaColisao float, @tempoColisao float, @movimento varchar(20), @projetilID int, @meteoroID int, @idCriado int out) as 
begin
     insert into Colisao(VoParaColidir, alturaColisao, tempoColisao,Movimento, projetilID, meteoroID)
	 values (@velocidadeInicial, @alturaColisao, @tempoColisao,@movimento, @projetilID, @meteoroID)
	 set @idCriado = SCOPE_IDENTITY()
end
GO
Create or alter procedure sp_ReadColisao as 
begin
     select * from Colisao
end
GO
--Trigger para a tabela Log
create or alter trigger trg_Insert_Projetil_Log on Projetil
for insert as
	begin	 
	  declare @anguloGraus float

	  select @anguloGraus=anguloGraus from inserted

		insert into LogPBL ( data, descricao)
		values ( getdate(), concat('Inclusão de valores: ângulo = ', @anguloGraus))		
		return
	end
GO

--Trigger para evitar delete de dados da Tabela Projetil
create or alter trigger trg_Delete_Projetil on Projetil
for delete as
      begin
	      print('Não é possível deletar dados da tabela Projetil')
		  rollback tran
	  end
GO

--Trigger para não permitir delete de dados da tabela Meteoro
create or alter trigger trg_Delete_Meteoro on Meteoro
for delete as
      begin
	      print('Não é possível deletar dados da tabela Meteoro')
		  rollback tran
	  end
GO
--Trigger para o campo 'colidiu' da tabela Colisao
create or alter trigger trg_campocolidiu on Colisao
for insert as
    begin
		declare @velocidadeInicial float
	    declare @tempoColisao float
		declare @ProjetilID int
		declare @MeteoroID int
		
		select @velocidadeInicial=VoParaColidir, @tempoColisao= tempoColisao, @ProjetilID = projetilID, @MeteoroID = meteoroID from Colisao
	   
		if @velocidadeInicial is null
			begin
				update Colisao set colidiu = 0 where projetilID = @ProjetilID and meteoroID = @MeteoroID
			end
		else 
			begin
				update Colisao set colidiu = 1 where projetilID = @ProjetilID and meteoroID = @MeteoroID
			end	    
	end
	
