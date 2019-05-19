CREATE TABLE [dbo].[RuleSet]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Name] NCHAR(10) NOT NULL, 
    CONSTRAINT [FK_RuleSet_ToTable] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
