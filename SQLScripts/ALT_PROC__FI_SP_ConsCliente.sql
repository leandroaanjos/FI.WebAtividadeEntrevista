/*----------------------------------------------------------------
DESCRIÇÃO	: Alteracao da proc FI_SP_ConsCliente
MOTIVO		: Adicionado CPF de clientes
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 01/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

ALTER PROC [dbo].[FI_SP_ConsCliente]
	@ID BIGINT
AS
BEGIN
	IF(ISNULL(@ID,0) = 0)
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF, ID FROM CLIENTES WITH(NOLOCK)
	ELSE
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF, ID FROM CLIENTES WITH(NOLOCK) WHERE ID = @ID
END