namespace ChgCharityJamPrototype.Models.GameEngineModels;

public record Effect
{
	public Guid Id { get; init; } = Guid.Empty;
	public string CardId { get; init; } = string.Empty;
	public DateTimeOffset? ActivationTime { get; init; }
	public TimeSpan Duration { get; init; } = TimeSpan.Zero;
	public DateTimeOffset? EffectEnd { get; init; }
}
