CREATE TABLE [dbo].[Rule]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Card] NCHAR(10) NOT NULL, 
    [RuleType] NCHAR(10) NOT NULL, 
    [Amount] INT NULL
)
