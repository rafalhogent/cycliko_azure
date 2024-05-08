namespace Cycliko.Invitation.Domain;

public class RaceModel
{
    public string Id { get; set; } = new Guid().ToString();
    public required string RaceName { get; set; }
    public string Description { get; set; } = "";
    public required string Location { get; set; }
}