namespace ChgCharityJamPrototype.Models.GameEngineModels;

public record Team
{
	public string Id { get; init; } = string.Empty;
	public Effect[] Effects { get; init; } = [];
	public decimal Balance { get; init; }
	public string Workspace { get; init; } = string.Empty;
}
