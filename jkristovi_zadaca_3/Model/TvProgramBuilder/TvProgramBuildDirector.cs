using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.TvProgramBuilder
{
    class TvProgramBuildDirector
    {
        private ITvProgramBuilder builder;

        public TvProgramBuildDirector(ITvProgramBuilder builder)
        {
            this.builder = builder;
        }

        public TvProgram Construct(
            int id, string naziv, DateTime pocetak, DateTime kraj, string nazivDatoteke)
        {
            return builder.SetId(id)
                          .SetNaziv(naziv)
                          .SetPocetakPrikazivanja(pocetak)
                          .SetKrajPrikazivanja(kraj)
                          .SetNazivDatoteke(nazivDatoteke)
                          .SetTjedan()
                          .Build();
        }
    }
}
