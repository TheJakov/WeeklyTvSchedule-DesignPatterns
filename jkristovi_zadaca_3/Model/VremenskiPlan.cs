using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    public class VremenskiPlan
    {
        private int IdEmisija;
        private List<int> ListaDanaEmitiranja;
        private DateTime Pocetak;
        private List<KeyValuePair<int, int>> ListaOsobaUloga;

        public void SetEmisijaId(int id)
        {
            IdEmisija = id;
        }
        public int GetEmisijaId()
        {
            return IdEmisija;
        }

        public void SetListaDanaEmitiranja(List<int> listaDana)
        {
            ListaDanaEmitiranja = listaDana;
        }
        public List<int> GetListaDanaEmitiranja()
        {
            return ListaDanaEmitiranja;
        }

        public void SetPocetak(DateTime pocetak)
        {
            Pocetak = pocetak;
        }
        public DateTime GetPocetak()
        {
            return Pocetak;
        }

        public void SetListaOsobaUloga(List<KeyValuePair<int,int>> lista)
        {
            ListaOsobaUloga = lista;
        }
        public List<KeyValuePair<int,int>> GetListaOsobaUloga()
        {
            return ListaOsobaUloga;
        }
    }
}
