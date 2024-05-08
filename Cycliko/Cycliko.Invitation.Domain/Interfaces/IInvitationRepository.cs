namespace Cycliko.Invitation.Domain;

public interface IInvitationRepository
{
    Task<MemoryStream> GetIvitationAsync(string trackingNumber);
    Task CreateInvitationAsync(RaceInvitation invitation);
}
