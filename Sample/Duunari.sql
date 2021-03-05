CREATE TABLE [dbo].[Duunari]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EtuNimi] NVARCHAR(50) NOT NULL, 
    [SukuNimi] NVARCHAR(50) NOT NULL, 
    [Palkka] FLOAT NOT NULL, 
    [TyoTunnit] INT NULL 
)
