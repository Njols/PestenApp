CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NCHAR(100) NOT NULL, 
    [Email] NCHAR(100) NOT NULL, 
    [Password] NCHAR(100) NOT NULL
)
