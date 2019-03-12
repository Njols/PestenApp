CREATE TABLE [dbo].[Rule_RuleSet]
(
	[RuleSetId] INT NOT NULL PRIMARY KEY, 
    [RuleId] INT NOT NULL, 
    CONSTRAINT [FK_Rule_RuleSet_ToTable] FOREIGN KEY (RuleSetId) REFERENCES RuleSet(Id),
	CONSTRAINT [FK_RUle_RuleSet_ToTable2] FOREIGN KEY (RuleId) REFERENCES [Rule](Id)
)
