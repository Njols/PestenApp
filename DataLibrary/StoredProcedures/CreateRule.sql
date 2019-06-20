CREATE PROCEDURE [dbo].[CreateRule]
	@CardFace int,
	@CardSuit int = NULL,
	@RuleType varchar(max),
	@Amount int,
	@RuleSetId int
AS
	DECLARE @RuleId int
	INSERT INTO [Rule] (CardFace,CardSuit, RuleType, Amount)
		VALUES (@CardFace,@CardSuit, @RuleType, @Amount)

	SELECT @RuleId = SCOPE_IDENTITY()

	INSERT INTO [Rule_RuleSet] (RuleId, RuleSetId)
		VALUES (@RuleId,@RuleSetId)

RETURN 0
