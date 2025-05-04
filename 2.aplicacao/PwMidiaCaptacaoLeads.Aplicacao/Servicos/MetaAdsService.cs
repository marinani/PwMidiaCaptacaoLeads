namespace PwMidiaCaptacaoLeads.Aplicacao.Servicos;

public class MetaAdsService
{
    private readonly MetaAuthService _authService;

    public MetaAdsService(MetaAuthService authService)
    {
        _authService = authService;
    }

    public void GetAdAccounts()
    {
        var client = _authService.AuthenticateMetaAds();
        dynamic accounts = client.Get("/me/adaccounts");

        foreach (var account in accounts.data)
        {
            Console.WriteLine($"Conta de Anúncio: {account.name}");
        }
    }
}