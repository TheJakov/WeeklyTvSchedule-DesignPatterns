using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.EmisijaBuilder
{
    class EmisijaBuildDirector
    {
        private IEmisijaBuilder builder;

        public EmisijaBuildDirector(IEmisijaBuilder builder)
        {
            this.builder = builder;
        }

        public Emisija Construct(
            int id, string naziv, int trajanje, int vrsta, List<KeyValuePair<int,int>> lista)
        {
            return builder.SetId(id)
                          .SetNaziv(naziv)
                          .SetTrajanje(trajanje)
                          .SetVrsta(vrsta)
                          .SetListaOsobaUloga(lista)
                          .Build();
        }
    }
}
