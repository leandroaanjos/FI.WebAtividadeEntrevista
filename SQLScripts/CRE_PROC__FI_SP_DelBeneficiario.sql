/*----------------------------------------------------------------
DESCRIÇÃO	: Criacao da proc FI_SP_DelBeneficiario
MOTIVO		: 
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 03/07/2024
*/---------------------------------------------------------------

USE [FI.WEBATIVIDADEENTREVISTA.MDF]
GO

CREATE PROC [dbo].[FI_SP_DelBeneficiario]
	@ID BIGINT
AS
BEGIN
	DELETE BENEFICIARIOS WHERE ID = @ID
END