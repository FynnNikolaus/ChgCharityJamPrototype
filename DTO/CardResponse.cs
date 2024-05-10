namespace ChgCharityJamPrototype.DTO;

/// <summary>
/// Request object for creating a new team
/// </summary>
public class CardResponse
{
	public string Id { get; set; }

	public string Name { get; set; }

	public string Type { get; set; }

	public string Activation { get; set; }

	public int Duration { get; set; }

	public int Price { get; set; }

	public int Quantity { get; set; }

	public string Description { get; set; }
}
