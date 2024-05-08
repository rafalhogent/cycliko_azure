using Cycliko.Invitation.Domain;
using Azure.Storage.Blobs;
using QuestPDF.Fluent;
using Microsoft.Extensions.Options;

namespace Cycliko.Invitation.Storage;

public class InvitationRepository : IInvitationRepository
{
    private readonly  IOptions<InvitationRepoOptions> _options;
    public InvitationRepository(IOptions<InvitationRepoOptions> options)
    {
        _options = options;
    }

    public async Task CreateInvitationAsync(RaceInvitation invitation)
    {
        var blobName = $"{invitation.InvitationNr}.pdf";

        var serviceClient = GetBlobServiceClient();
        var containerClient = serviceClient.GetBlobContainerClient(_options.Value.Container);
        var blobClient = containerClient.GetBlobClient(blobName);

        var stream = new MemoryStream();
        invitation.GeneratePdf(stream);
        stream.Position = 0;

        await blobClient.UploadAsync(stream);
        stream.Close();
    }

    public async Task<MemoryStream> GetIvitationAsync(string invitationNr)
    {
        var blobName = $"{invitationNr}.pdf";

        var serviceClient = GetBlobServiceClient();
        var containerClient = serviceClient.GetBlobContainerClient(_options.Value.Container);
        var blobClient = containerClient.GetBlobClient(blobName);

        var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream);

        return stream;
    }

    private BlobServiceClient GetBlobServiceClient()
    {
        var connectionString = _options.Value.BlobConnectionString;
        BlobServiceClient client = new BlobServiceClient(connectionString);
        return client;
    }
}
