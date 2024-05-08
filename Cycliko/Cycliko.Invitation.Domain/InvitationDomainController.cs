namespace Cycliko.Invitation.Domain;

public class InvitationDomainController : IInvitationDomainController
{
    private readonly IInvitationRepository _repo;

    public InvitationDomainController(IInvitationRepository repo)
    {
        _repo = repo;
    }

    public async Task<string> CreateInvitationAsync(
        CreateInvitationRequestDto invitationDto,
        double energy,
        double distance)
    {
        RaceInvitation invitation = new RaceInvitation
        {
            RiderName = invitationDto.RiderName,
            RiderEmail = invitationDto.RiderEmail,
            RiderAddress = invitationDto.RiderAddress,
            RaceDetails = new RaceModel
            {
                RaceName = invitationDto.RaceName,
                Location = invitationDto.Location,
            },
            ExpectedEnergyKJ = energy,
            RaceDistance = distance

        };
        await _repo.CreateInvitationAsync(invitation);
        return invitation.InvitationNr;
    }

    public async Task<MemoryStream> GetInvitationAsync(string invitationNr)
    {
        var stream = await _repo.GetIvitationAsync(invitationNr);
        stream.Position = 0;
        return stream;
    }
}
