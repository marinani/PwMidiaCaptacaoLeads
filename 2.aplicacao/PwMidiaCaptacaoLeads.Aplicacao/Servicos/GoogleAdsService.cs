using Google.Ads.GoogleAds;

namespace PwMidiaCaptacaoLeads.Aplicacao.Servicos;

public class GoogleAdsService
{
    private readonly GoogleAuthService _authService;

    public GoogleAdsService(GoogleAuthService authService)
    {
        _authService = authService;
    }

    public void GetCampaigns()
    {
        var client = _authService.AuthenticateGoogleAds();
        var campaignService = client.GetService(Services.V18.GoogleAdsService);

        // Exemplo de busca de campanhas
        var query = "SELECT campaign.id, campaign.name FROM campaign";
        var response = campaignService.Search("SEU_CUSTOMER_ID", query);

        foreach (var row in response)
        {
            Console.WriteLine($"Campanha: {row.Campaign.Name}");
        }
    }


    public void GetCampaignPerformance()
    {
        var client = _authService.AuthenticateGoogleAds();
        var campaignService = client.GetService(Services.V18.GoogleAdsService);

        var query = @"
        SELECT
            campaign.id,
            campaign.name,
            metrics.clicks,
            metrics.impressions,
            metrics.conversions
        FROM
            campaign
        WHERE
            segments.date DURING LAST_30_DAYS";

        var response = campaignService.Search("SEU_CUSTOMER_ID", query);

        foreach (var row in response)
        {
            Console.WriteLine($"Campanha: {row.Campaign.Name}, Cliques: {row.Metrics.Clicks}, Conversões: {row.Metrics.Conversions}");
        }
    }
}