namespace ChgCharityJamPrototype.DTO;

/// <summary>
/// Request object for creating a new team
/// </summary>
public class TeamResponse
{
	public string Name { get; set; }

	public string Workspace { get; set; }

	public int Balance { get; set; }

	public int UserCount { get; set; }

	//public FormFile Image { get; set; }
}
