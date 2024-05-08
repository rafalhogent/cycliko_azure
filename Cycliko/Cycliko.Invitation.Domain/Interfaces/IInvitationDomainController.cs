namespace Cycliko.Invitation.Domain;

public interface IInvitationDomainController
{
    Task<string> CreateInvitationAsync(
        CreateInvitationRequestDto invitationDto,
        double energy,
        double distance);

    Task<MemoryStream> GetInvitationAsync(string invitationNr);
}
