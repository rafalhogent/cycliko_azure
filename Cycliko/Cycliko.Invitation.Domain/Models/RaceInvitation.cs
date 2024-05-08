using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cycliko.Invitation.Domain;

public class RaceInvitation : IDocument
{
    public string InvitationNr { get; set; } = Guid.NewGuid().ToString();
    public required string RiderName { get; set; }
    public required string RiderEmail { get; set; }
    public required string RiderAddress { get; set; }
    public required RaceModel RaceDetails { get; set; }

    public double ExpectedEnergyKJ { get; set; }
    public double RaceDistance { get; set; }

    public void Compose(IDocumentContainer container)
    {
        container.Page(p => p.Content().Padding(26).Element(ComposeContent));
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            var lines = new List<string>{
               $"Invitation Number: {InvitationNr}",
               $"Name: {RiderName}",
               $"Email: {RiderEmail}",
               $"Address: {RiderAddress}",
               $"Race: {RaceDetails.RaceName}",
               $"Distance: {RaceDistance} km",
               $"Location: {RaceDetails.Location}" ,
               $"{RaceDetails.Description}" ,
            };

            lines.ForEach(l => col.Item().Text(l));
        });
    }
}
