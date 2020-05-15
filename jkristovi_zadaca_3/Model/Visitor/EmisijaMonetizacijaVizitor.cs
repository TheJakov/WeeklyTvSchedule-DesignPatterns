using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Visitor
{
    public class EmisijaMonetizacijaVizitor : IEmisijaVisitor
    {
        public int Visit(Emisija emisija)
        {
            int profitabilnihMinuta = 0;
            int vrsta = emisija.GetVrsta();
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            VrstaEmisije vrstaEm = tvKuca.ListaVrstaEmisija.Find(x => x.GetId() == vrsta);
            if (vrstaEm != null)
            {
                if (vrstaEm.GetMozeImatReklame())
                    profitabilnihMinuta = vrstaEm.GetMaxTrajanjeReklama();
            }
            return profitabilnihMinuta;
        }
    }
}
