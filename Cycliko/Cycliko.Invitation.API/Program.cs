using System.Text.Json;
using System.Text.Json.Serialization;
using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.Invitation.API;
using Cycliko.Invitation.API.Extensions;
using Cycliko.Invitation.Domain;
using Cycliko.Invitation.Storage;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EnergyQuoteClientOptions>(
    builder.Configuration.GetSection(
        key: nameof(EnergyQuoteClientOptions)));

builder.Services.Configure<InvitationRepoOptions>(
    builder.Configuration.GetSection(
        key: nameof(InvitationRepoOptions)));


builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IInvitationDomainController, InvitationDomainController>();
builder.Services.AddHttpClient();
builder.Services.AddCyclikoOpenApiDoc();
QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapGet("/", () => "Hello Cycliko Invitations!");

app.MapGet("/api/locations", async Task<object> (IHttpClientFactory httpClientFactory, [FromBody] LocationsRequestDto searchDto) =>
{
    using HttpClient client = httpClientFactory.CreateClient();

    var abc = $"https://geo.api.vlaanderen.be/geolocation/v1/Suggestion?q={searchDto.QueryText}";
    var response = await client.GetFromJsonAsync<object>(abc);

    return response;
});

app.MapPost("/api/invitations",
async Task<Results<Ok<GetInvitationResponseDto>, FileStreamHttpResult, NotFound<string>,  CreatedAtRoute<InvitationCreationResponseDto>>> (
    IInvitationDomainController domainController,
    [FromBody] CreateInvitationRequestDto invitationDto,
    IHttpClientFactory httpClientFactory,
    IOptions<EnergyQuoteClientOptions> quoteClientOptions
    ) =>
{
    using HttpClient client = httpClientFactory.CreateClient();

    var discoveryDoc = await client.GetDiscoveryDocumentAsync(quoteClientOptions.Value.DiscoveryDocURL);
    var token = await client.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
            Address = discoveryDoc.TokenEndpoint,
            ClientId = "internal-cycliko-invitations",
            ClientSecret = quoteClientOptions.Value.SecretTokenIntern,
            Scope = "cycliko.energyquote.api.READ"
        });

    client.SetBearerToken(token.AccessToken!);

    var options = new JsonSerializerOptions();
    options.PropertyNameCaseInsensitive = true;
    options.Converters.Add(new JsonStringEnumConverter());

    var connStr = $"{quoteClientOptions.Value.BaseUrl}/{invitationDto.QuoteId}";

    try
    {
        var quote = await client.GetFromJsonAsync<EnergyQuoteResponseDTO>(connStr, options);
        if (quote != null)
        {
            var invitationNumber = await domainController
                .CreateInvitationAsync(invitationDto, quote.EnergyKiloJoules, quote.RaceDistanceKm);

            var creationResponse = new InvitationCreationResponseDto
            {
                InvitationNr = invitationNumber,
            };

            return TypedResults.CreatedAtRoute(creationResponse, "GetInvitation", new { InvitationNr = creationResponse.InvitationNr });
        }
        else
        {
            return TypedResults.NotFound("Energy Quote not found");
        }
    }
    catch (HttpRequestException ex)
    {
        if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return TypedResults.NotFound("Energy Quote not found");
        }
        throw;
    }

});

app.MapGet("/api/invitations/{invitationNr}", async Task<Results<Ok<GetInvitationResponseDto>, FileStreamHttpResult, NotFound>> (
    IInvitationDomainController domain,
    [FromHeader(Name = "Accept")] string acceptHeader,
    [FromRoute] string invitationNr) =>
{

    if (string.Equals(acceptHeader, "text/json"))
    {
        var result = new GetInvitationResponseDto
        {
            InvitationNr = invitationNr
        };
        return TypedResults.Ok(result);
    }
    else if (string.Equals(acceptHeader, "application/octet-stream"))
    {
        var stream = await domain.GetInvitationAsync(invitationNr);
        return TypedResults.Stream(stream, "application/octet-stream", $"{invitationNr}.pdf");
    }

    return TypedResults.NotFound();

}).WithName("GetInvitation");

app.Run();
