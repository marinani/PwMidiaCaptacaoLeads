using Microsoft.AspNetCore.Mvc;
using PwMidiaCaptacaoLeads.Aplicacao.Servicos;

namespace PwMidiaCaptacaoLeads.Adm.Controllers
{
    public class IntegracoesController : Controller
    {

        private readonly GoogleAdsService _googleAdsService;
        private readonly MetaAdsService _metaAdsService;

        public IntegracoesController(GoogleAdsService googleAdsService, MetaAdsService metaAdsService)
        {
            _googleAdsService = googleAdsService;
            _metaAdsService = metaAdsService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("google-ads/campaigns")]
        public IActionResult GetGoogleAdsCampaigns()
        {
            _googleAdsService.GetCampaigns();
            return Ok("Campanhas do Google Ads obtidas com sucesso.");
        }

        [HttpGet("meta-ads/accounts")]
        public IActionResult GetMetaAdAccounts()
        {
            _metaAdsService.GetAdAccounts();
            return Ok("Contas de anúncios do Meta Ads obtidas com sucesso.");
        }
    }
}
