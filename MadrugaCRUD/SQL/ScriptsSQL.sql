
CREATE TABLE [dbo].[tbCliente](
	[Id] [int]  NOT NULL IDENTITY(1, 1),
	[Nome] [varchar](max) NOT NULL,
	[CidadeId] [int]  NOT NULL,
	CONSTRAINT [PK_Cliente] PRIMARY KEY  (Id)
);

CREATE TABLE [dbo].[tbEstado] (
    [Id] [int]  NOT NULL IDENTITY(1, 1),
    [Nome] [varchar](max)  NOT NULL,
	[UF] [varchar](2)  NOT NULL,
    CONSTRAINT [PK_Estado] PRIMARY KEY  (Id)
);

CREATE TABLE [dbo].[tbCidade] (
    [Id] [int]  NOT NULL IDENTITY(1, 1),
    [Nome] [varchar](max)  NOT NULL,
	[EstadoId] [int]  NOT NULL,
    CONSTRAINT [PK_Cidade] PRIMARY KEY  (Id)
);


ALTER TABLE [dbo].[tbCliente]  WITH CHECK ADD  CONSTRAINT [FK_tbCliente_tbCidade] FOREIGN KEY([CidadeId])
REFERENCES [dbo].[tbCidade] ([Id])
GO

ALTER TABLE [dbo].[tbCidade]  WITH CHECK ADD  CONSTRAINT [FK_tbCidade_tbEstado] FOREIGN KEY([EstadoId])
REFERENCES [dbo].[tbEstado] ([Id])
GO

--PROCEDURES

--INCLUIR
CREATE PROCEDURE [dbo].[spIncluirEstado]              
@Nome varchar(max),  
@UF varchar(2)    
AS        
BEGIN
INSERT INTO [tbEstado] ([Nome],[UF]) VALUES (@Nome, @UF) 
END    
GO
CREATE PROCEDURE [dbo].[spIncluirCidade]              
@Nome varchar(max),  
@EstadoId int   
AS        
BEGIN
INSERT INTO [tbCidade] ([Nome],[EstadoId]) VALUES (@Nome, @EstadoId) 
END    
GO
CREATE PROCEDURE [dbo].[spIncluirCliente]              
@Nome varchar(max),  
@CidadeId int   
AS        
BEGIN
INSERT INTO [tbCliente] ([Nome],[CidadeId]) VALUES (@Nome, @CidadeId) 
END   
GO

--REMOVER
CREATE PROCEDURE [dbo].[spRemoverEstado]              
@Id int   
AS        
BEGIN
DELETE FROM [tbEstado] WHERE Id = @Id
END    
GO
CREATE PROCEDURE [dbo].[spRemoverCidade]              
@Id int 
AS        
BEGIN
DELETE FROM [tbCidade] WHERE Id = @Id
END    
GO
CREATE PROCEDURE [dbo].[spRemoverCliente]              
@Id int 
AS        
BEGIN
DELETE FROM [tbCliente] WHERE Id = @Id
END   
GO

--LISTAR
CREATE PROCEDURE [dbo].[spListarEstado]              
@Id int = null
AS        
BEGIN
SELECT * FROM [tbEstado] (NOLOCK) WHERE Id = @Id or @Id IS NULL order by nome
END    
GO
CREATE PROCEDURE [dbo].[spListarCidade]              
@Id int = null,
@EstadoId int = null
AS        
BEGIN
SELECT * FROM [tbCidade] (NOLOCK) WHERE (Id = @Id or @Id IS NULL) and (EstadoId = @EstadoId or @EstadoId IS NULL) order by nome
END    
GO
CREATE PROCEDURE [dbo].[spListarCliente]              
@Id int = null,
@CidadeId int = null
AS        
BEGIN
SELECT * FROM [tbCliente] (NOLOCK) WHERE (Id = @Id or @Id IS NULL) and (CidadeId = @CidadeId or @CidadeId IS NULL) order by nome
END   
GO

--ATUALIZAR
CREATE PROCEDURE [dbo].[spAtualizarEstado]              
@Nome varchar(max),  
@UF varchar(2),
@Id int 
AS        
BEGIN
UPDATE [tbEstado] SET [Nome] = @Nome, [UF] = @UF WHERE Id = @Id
END    
GO
CREATE PROCEDURE [dbo].[spAtualizarCidade]              
@Nome varchar(max),  
@EstadoId int,
@Id int 
AS        
BEGIN
UPDATE [tbCidade] SET [Nome] = @Nome, [EstadoId] = @EstadoId WHERE Id = @Id
END    
GO
CREATE PROCEDURE [dbo].[spAtualizarCliente]              
@Nome varchar(max),  
@CidadeId int,
@Id int 
AS        
BEGIN
UPDATE [tbCliente] SET [Nome] = @Nome, [CidadeId] = @CidadeId WHERE Id = @Id
END   
GO