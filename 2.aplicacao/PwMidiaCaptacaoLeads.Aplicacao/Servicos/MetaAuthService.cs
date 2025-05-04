using Facebook;

namespace PwMidiaCaptacaoLeads.Aplicacao.Servicos;

public class MetaAuthService
{
    private readonly string _accessToken;

    public MetaAuthService(string accessToken)
    {
        _accessToken = accessToken;
    }

    public FacebookClient AuthenticateMetaAds()
    {
        return new FacebookClient(_accessToken);
    }
}