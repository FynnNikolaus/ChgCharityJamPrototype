namespace ChgCharityJamPrototype.Models.GameEngineModels;

public record Team
{
	public string Id { get; init; } = string.Empty;
	public Effect[] Effects { get; init; } = [];
	public decimal Balance { get; init; }
	public string Workspace { get; init; } = string.Empty;

	public virtual bool Equals(Team? other)
	{
		if (other == null)
			return false;

		return Id == other.Id && Effects.OrderBy(x => x.Id).SequenceEqual(other.Effects.OrderBy(x => x.Id)) && Balance == other.Balance && Workspace == other.Workspace;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Id.GetHashCode(), Effects.GetHashCode(), Balance.GetHashCode(), Workspace.GetHashCode());
	}
}
