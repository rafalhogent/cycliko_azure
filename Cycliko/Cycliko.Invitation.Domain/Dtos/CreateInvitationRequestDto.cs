namespace Cycliko.Invitation.Domain;

public class CreateInvitationRequestDto
{
    public required string QuoteId { get; set; }
    public required string RiderName { get; set; }
    public required string RiderEmail { get; set; }
    public required string RiderAddress { get; set; }
    public required string RaceName { get; set; }
    public string? RaceDescription { get; set; } = "";
    public required string Location { get; set; }

}