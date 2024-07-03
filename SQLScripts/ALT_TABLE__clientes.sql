 /*----------------------------------------------------------------
DESCRIÇÃO	: Alteracao da tabela CLIENTES
MOTIVO		: Armazenar CPF de clientes
RESPONSÁVEL	: Leandro Dos Anjos
DATA		: 01/07/2024
*/---------------------------------------------------------------

IF NOT EXISTS (
	SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.CLIENTES') AND name = 'CPF'
)
BEGIN
    ALTER TABLE dbo.CLIENTES
	ADD CPF varchar(14) NOT NULL;
END
GO