/*----------------------------------------------------------------
DESCRIÇÃO	: Criacao da proc FI_SP_ConsBeneficiariosPorTitular
MOTIVO		: 
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 03/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

CREATE PROC [dbo].[FI_SP_ConsBeneficiariosPorTitular]
	@IDCLIENTE BIGINT
AS
BEGIN
	SELECT ID, CPF, NOME, IDCLIENTE FROM BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE
END