 /*----------------------------------------------------------------
DESCRIÇÃO	: Alteracao da tabela BENEFICIARIOS
MOTIVO		: Alterado campo de CPF para armazenar mais caracteres
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 03/07/2024
*/---------------------------------------------------------------

IF EXISTS (
	SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.BENEFICIARIOS') AND name = 'CPF'
)
BEGIN
    ALTER TABLE dbo.BENEFICIARIOS
	ALTER COLUMN CPF varchar(14) NOT NULL;
END
GO