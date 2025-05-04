using Google.Apis.Auth.OAuth2;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.Config;
using Google.Apis.Analytics.v3;
using Google.Apis.Services;

namespace PwMidiaCaptacaoLeads.Aplicacao.Servicos;

public class GoogleAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;

    public GoogleAuthService(string clientId, string clientSecret)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public GoogleAdsClient AuthenticateGoogleAds()
    {
        var config = new GoogleAdsConfig
        {
            OAuth2ClientId = _clientId,
            OAuth2ClientSecret = _clientSecret,
            OAuth2RefreshToken = "YOUR_REFRESH_TOKEN"
        };

        return new GoogleAdsClient(config);
    }



    public async Task<UserCredential> AuthenticateGoogleAnalyticsAsync()
    {
        return await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            },
            new[] { "https://www.googleapis.com/auth/analytics.readonly" },
            "user",
            CancellationToken.None
        );
    }


    public async Task GetAnalyticsDataAsync()
    {
        var credential = await AuthenticateGoogleAnalyticsAsync();
        var analyticsService = new AnalyticsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "PwMidiaCaptacaoLeads"
        });

        var request = analyticsService.Data.Ga.Get(
            "ga:SEU_VIEW_ID",
            "30daysAgo",
            "today",
            "ga:sessions,ga:pageviews,ga:goalCompletionsAll");
        var response = await request.ExecuteAsync();

        foreach (var row in response.Rows)
        {
            Console.WriteLine($"Sessões: {row[0]}, Visualizações de Página: {row[1]}, Conversões: {row[2]}");
        }
    }
}