/*----------------------------------------------------------------
DESCRIÇÃO	: Criacao da proc FI_SP_IncBeneficiario
MOTIVO		: 
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 03/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

CREATE PROC [dbo].[FI_SP_IncBeneficiario]
    @CPF           VARCHAR (14) ,
	@NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT	
AS
BEGIN
	INSERT INTO BENEFICIARIOS(CPF, NOME, IDCLIENTE) 
	VALUES (@CPF, @NOME, @IDCLIENTE)

	SELECT SCOPE_IDENTITY()
END
GO


