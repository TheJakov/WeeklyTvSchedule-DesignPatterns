using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.EmisijaBuilder
{
    class EmisijaConcreteBuilder : IEmisijaBuilder
    {
        private Emisija emisija;

        public EmisijaConcreteBuilder()
        {
            emisija = new Emisija();
        }

        public Emisija Build()
        {
            return emisija;
        }

        public IEmisijaBuilder SetId(int id)
        {
            emisija.SetId(id);
            return this;
        }

        public IEmisijaBuilder SetNaziv(string naziv)
        {
            emisija.SetNaziv(naziv);
            return this;
        }

        public IEmisijaBuilder SetTrajanje(int trajanje)
        {
            emisija.SetTrajanje(trajanje);
            return this;
        }

        public IEmisijaBuilder SetVrsta(int vrsta)
        {
            emisija.SetVrsta(vrsta);
            return this;
        }

        public IEmisijaBuilder SetListaOsobaUloga(List<KeyValuePair<int,int>> lista)
        {
            emisija.SetListaOsobaUloga(lista);
            return this;
        }
    }
}
