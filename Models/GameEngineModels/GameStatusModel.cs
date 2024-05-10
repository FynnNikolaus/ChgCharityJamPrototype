namespace ChgCharityJamPrototype.Models.GameEngineModels;

public record GameStatusModel
{
	public Team[] Teams { get; init; } = [];
	public TimeSpan Uptime { get; init; } = TimeSpan.Zero;

	public virtual bool Equals(GameStatusModel? other)
	{
		if (other == null)
			return false;

		return Teams.OrderBy(x => x.Id).SequenceEqual(other.Teams.OrderBy(x => x.Id));
	}

	public override int GetHashCode()
	{
		return Teams.GetHashCode();
	}
}
