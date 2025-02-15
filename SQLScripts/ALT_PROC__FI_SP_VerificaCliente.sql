/*----------------------------------------------------------------
DESCRIÇÃO	: Alteracao da proc FI_SP_VerificaCliente
MOTIVO		: Adicionado retorno de mais campos na consulta de cliente
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 01/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

ALTER PROC [dbo].[FI_SP_VerificaCliente]	
	@CPF VARCHAR(14)
AS
BEGIN
	SELECT * FROM CLIENTES WITH(NOLOCK) WHERE CPF = @CPF
END