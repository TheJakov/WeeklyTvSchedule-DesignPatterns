using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.EmisijaBuilder
{
    /// <summary>
    /// The builder abstraction.
    /// </summary>
    interface IEmisijaBuilder
    {
        Emisija Build();
        IEmisijaBuilder SetId(int id);
        IEmisijaBuilder SetNaziv(string naziv);
        IEmisijaBuilder SetTrajanje(int trajanje);
        IEmisijaBuilder SetVrsta(int vrsta);
        IEmisijaBuilder SetListaOsobaUloga(List<KeyValuePair<int,int>> lista);

    }
}
