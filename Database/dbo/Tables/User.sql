CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NCHAR(10) NOT NULL, 
    [Email] NCHAR(10) NOT NULL, 
    [Password] NCHAR(10) NOT NULL
)
