CREATE DATABASE dbteste
GO

USE dbteste
GO

CREATE TABLE Cliente (
	ClienteId BIGINT NOT NULL IDENTITY (1,1),
	Cliente VARCHAR(200) NOT NULL,
	TipoCliente VARCHAR(40) NOT NULL,
	NomeContato VARCHAR(200) NOT NULL,
	TelefoneContato VARCHAR(15) NOT NULL,
	Cidade VARCHAR(100) NOT NULL,
	Bairro VARCHAR(100) NOT NULL,
	Logradouro VARCHAR(200) NOT NULL,
	DataCadastro DATE,
	DataAtualizacao DATE,
	CONSTRAINT Pk_ClienteId PRIMARY KEY (ClienteId)
)
GO

DECLARE @contador INT
DECLARE @fone INT
DECLARE @data DATE

SET @contador = 1
SET @fone = 1
SET @data = '2015-03-10'

WHILE (@contador <= 5000)
BEGIN
	INSERT INTO Cliente(
		Cliente, TipoCliente, NomeContato, TelefoneContato, Cidade, Bairro, Logradouro, DataCadastro, DataAtualizacao
	) VALUES (
		CONCAT('Teste', @contador), 
		CONCAT('TipoCliente', @contador), 
		CONCAT('NomeContato', @contador), 
		REPLICATE(@fone, 4) + '-' + REPLICATE(@fone, 4), 
		CONCAT('Cidade', @contador), 
		CONCAT('Bairro', @contador), 
		CONCAT('Rua Teste', @contador),
		@data, 
		DATEADD(DAY, 34, @data)
	)

	SET @contador = @contador + 1
	SET @fone = @fone + 1
	SET @data = DATEADD(DAY, 1, @data)

	IF @fone = 10
		SET @fone = 1
END
GO

CREATE PROCEDURE SP_ListarClientes
(
	@start int,
	@length int,
	@search nvarchar(MAX)
)
AS
BEGIN
	DECLARE @campos nvarchar(MAX)
	DECLARE @script nvarchar(MAX)

	SET @search = '%' + @search + '%'
	SET @campos = '@start int,@length int,@search nvarchar(MAX)'
	SET @script = 
	'SELECT
		ClienteId, 
		Cliente, 
		TipoCliente, 
		NomeContato, 
		TelefoneContato, 
		Cidade, 
		Bairro, 
		Logradouro,
		FORMAT(DataCadastro, ''dd/MM/yyyy'') AS DataCadastro, 
		FORMAT(DataAtualizacao, ''dd/MM/yyyy'') AS DataAtualizacao
		FROM 
		(SELECT 
				ROW_NUMBER() OVER (ORDER BY ClienteId ASC)  AS Row,
				ClienteId, 
				Cliente, 
				TipoCliente, 
				NomeContato, 
				TelefoneContato, 
				Cidade, 
				Bairro, 
				Logradouro, 
				DataCadastro, 
				DataAtualizacao
			FROM Cliente
			WHERE 
				(
					ClienteId LIKE @search OR Cliente LIKE @search 
					OR TipoCliente LIKE @search OR NomeContato LIKE @search 
					OR TelefoneContato LIKE @search OR Cidade LIKE @search
					OR Bairro LIKE @search OR Logradouro LIKE @search 
					OR FORMAT(DataCadastro, ''dd/MM/yyyy'') LIKE @search 
					OR FORMAT(DataAtualizacao, ''dd/MM/yyyy'') LIKE @search 
				)
		) AS ClienteWithRow
	WHERE Row > @start AND Row <= (@start + @length)
	ORDER BY ClienteId ASC'

	EXECUTE SP_EXECUTESQL @script, @campos, @start, @length, @search
END
GO

CREATE PROCEDURE SP_GetTotalRecords
(
	@search nvarchar(MAX)
)
AS
BEGIN
	SET @search = '%' + @search + '%'

	SELECT COUNT(1) AS TotalRecords 
	FROM Cliente
	WHERE (
		ClienteId LIKE @search OR Cliente LIKE @search 
		OR TipoCliente LIKE @search OR NomeContato LIKE @search 
		OR TelefoneContato LIKE @search OR Cidade LIKE @search
		OR Bairro LIKE @search OR Logradouro LIKE @search 
		OR FORMAT(DataCadastro, 'dd/MM/yyyy') LIKE @search 
		OR FORMAT(DataAtualizacao, 'dd/MM/yyyy') LIKE @search 
	)
END
GO

CREATE PROCEDURE SP_AdicionarCliente
(
	@Cliente nvarchar(200), 
	@TipoCliente nvarchar(40), 
	@NomeContato nvarchar(200), 
	@TelefoneContato nvarchar(15), 
	@Cidade nvarchar(100), 
	@Bairro nvarchar(100), 
	@Logradouro nvarchar(200),
	@DataCadastro nvarchar(20)
)
AS
BEGIN
	INSERT INTO Cliente (Cliente, TipoCliente, NomeContato, TelefoneContato, Cidade, Bairro, Logradouro, DataCadastro)
	VALUES (@Cliente, @TipoCliente, @NomeContato, @TelefoneContato, @Cidade, @Bairro, @Logradouro, @DataCadastro)
END
GO

CREATE PROCEDURE SP_ObterCliente
(
	@ClienteId bigint
) 
AS
BEGIN
	SELECT * FROM Cliente WHERE ClienteId = @ClienteId
END
GO

CREATE PROCEDURE SP_AtualizarCliente
(
	@ClienteId bigint,
	@Cliente nvarchar(200), 
	@TipoCliente nvarchar(40), 
	@NomeContato nvarchar(200), 
	@TelefoneContato nvarchar(15), 
	@Cidade nvarchar(100), 
	@Bairro nvarchar(100), 
	@Logradouro nvarchar(200),
	@DataAtualizacao nvarchar(20)
)
AS
BEGIN
	UPDATE Cliente 
	SET 
		Cliente = @Cliente, 
		TipoCliente = @TipoCliente, 
		NomeContato = @NomeContato, 
		TelefoneContato = @TelefoneContato, 
		Cidade = @Cidade, 
		Bairro = @Bairro, 
		Logradouro = @Logradouro, 
		DataAtualizacao = @DataAtualizacao
	WHERE ClienteId = @ClienteId
END
GO

CREATE PROCEDURE SP_ExcluirCliente
(
	@ClienteId bigint
)
AS
BEGIN
	DELETE FROM Cliente WHERE ClienteId = @ClienteId
END
GO
