CREATE TABLE [dbo].[AdditionalRule_RuleSet]
(
	[RuleSetId] INT NOT NULL PRIMARY KEY,
	[AdditionalRuleId] INT NOT NULL, 
    CONSTRAINT [FK_AdditionalRule_RuleSet_ToTable] FOREIGN KEY (RuleSetId) REFERENCES RuleSet(Id),
	CONSTRAINT [FK_AdditionalRule_Ruleset_ToTable2] FOREIGN KEY (AdditionalRuleId) REFERENCES AdditionalRule(Id)
)
