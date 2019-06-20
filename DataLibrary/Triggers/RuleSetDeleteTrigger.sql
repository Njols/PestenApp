CREATE TRIGGER [RuleSetDeleteTrigger]
	ON [dbo].[RuleSet]
	INSTEAD OF DELETE
	AS
	BEGIN
		SET NOCOUNT ON
		DELETE FROM [Rule_RuleSet] WHERE [RuleSetId] = (SELECT [Id] FROM deleted)
		Delete FROM [Rule] WHERE Id NOT IN (SELECT RuleId from [Rule_RuleSet])
		DELETE FROM [AdditionalRule_RuleSet] WHERE [RuleSetId] = (SELECT [Id] FROM deleted)
		DELETE FROM [RuleSet] WHERE Id = (SELECT Id FROM deleted)
	END
