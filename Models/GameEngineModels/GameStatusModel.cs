namespace ChgCharityJamPrototype.Models.GameEngineModels;

public record GameStatusModel
{
	public Team[] Teams { get; init; } = [];
	public TimeSpan Uptime { get; init; } = TimeSpan.Zero;
}
