namespace Cycliko.Invitation.Storage;

public class InvitationRepoOptions
{
    public required string BlobConnectionString { get; set; }
    public required string Container { get; set; }
}
