CREATE TABLE [dbo].[Rule]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CardFace] INT NOT NULL,
	[CardSuit] INT NULL,
    [RuleType] INT NOT NULL, 
    [Amount] INT NULL
)
