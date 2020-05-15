using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.TvProgramBuilder
{
    /// <summary>
    /// The builder abstraction.
    /// </summary>
    interface ITvProgramBuilder
    {
        TvProgram Build();
        ITvProgramBuilder SetId(int id);
        ITvProgramBuilder SetNaziv(string naziv);
        ITvProgramBuilder SetPocetakPrikazivanja(DateTime pocetak);
        ITvProgramBuilder SetKrajPrikazivanja(DateTime kraj);

        ITvProgramBuilder SetNazivDatoteke(string nazivDatoteke);

        ITvProgramBuilder SetVremenskiPlan(List<VremenskiPlan> plan);

        ITvProgramBuilder SetTjedan();
    }
}
