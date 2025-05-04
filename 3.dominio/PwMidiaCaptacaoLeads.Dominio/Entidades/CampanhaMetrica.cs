using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwMidiaCaptacaoLeads.Dominio.Entidades
{
    public class CampanhaMetrica
    {
        public int Id { get; set; }
        public long CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public long Impressions { get; set; }
        public long Clicks { get; set; }
        public double Cost { get; set; }
        public DateTime DataColeta { get; set; }
    }
}
