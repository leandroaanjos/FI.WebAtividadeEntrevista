/*----------------------------------------------------------------
DESCRIÇÃO	: Criacao da proc FI_SP_AltBenefV2
MOTIVO		: 
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 03/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

CREATE PROC [dbo].[FI_SP_AltBenefV2]
    @CPF           VARCHAR (14)	,
	@NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT,    
	
	@Id           BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		CPF = @CPF,
		NOME = @NOME, 
		IDCLIENTE = @IDCLIENTE
	WHERE Id = @Id
END