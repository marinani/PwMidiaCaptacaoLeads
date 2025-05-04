using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PwMidiaCaptacaoLeads.Dados;

public class PwMidiaLeadsDbContexto : DbContext
{
    public PwMidiaLeadsDbContexto(DbContextOptions<PwMidiaLeadsDbContexto> options)
         : base(options)
    {
    }
}
