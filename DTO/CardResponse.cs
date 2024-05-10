namespace ChgCharityJamPrototype.DTO;

/// <summary>
/// Request object for creating a new team
/// </summary>
public class CardResponse
{
	public string Id { get; set; }

	public string Type { get; set; }

	public string Name { get; set; }

	public string Activation { get; set; }

	public int? Duration { get; set; }

	public string Stacking { get; set; }

	public double Price { get; set; }

	public int? Quantity { get; set; }

	public string TTSPrimaryDesc { get; set; }

	public string TTSSecondaryDesc { get; set; }
}
