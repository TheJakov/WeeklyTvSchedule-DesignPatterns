using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Model.ChainOfResponsibility
{
    public class ProfitStatHandler : AbstractStatisticHandler
    {
        public ProfitStatHandler(int opcija)
        {
            Opcija = opcija;
        }

        public override void Calculate(IComponentRaspored component)
        {
            int brojEmisija = 0;
            int brojProfitabilnihEmisija = 0;
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var emisija in component.GetChildList())
            {
                int vrsta = emisija.GetVrsta();
                VrstaEmisije vrstaEm = tvKuca.ListaVrstaEmisija.Find(x => x.GetId() == vrsta);
                if (vrstaEm != null)
                {
                    brojEmisija++;
                    if (vrstaEm.GetMozeImatReklame())
                        brojProfitabilnihEmisija++;
                }
            }
            IspisiStatistiku(brojEmisija, brojProfitabilnihEmisija);
        }

        private static void IspisiStatistiku(int ukEmisija, int profitEmisija)
        {
            Console.WriteLine("\nOvaj dan sadrži ukupno " + ukEmisija + " emisija.\n");
            IspisHelper.PrikaziZagavljeTablicaStatVrsta();

            IspisHelper.Brojac = 0;
            IRedakTablice redakTablice =
                    new KratkiTekstDecorator(
                        new CjelobrojniDecorator(
                            new TekstualniDecorator(
                                new ConcreteRedak())));
            string format = redakTablice.NapraviRedak();

            string ispis = String.Format(format,
                DobijPostotak(profitEmisija, ukEmisija) + "%", profitEmisija, "PROFITABILNE");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('.', ispis.Length));

            string ispis2 = String.Format(format,
                DobijPostotak((ukEmisija - profitEmisija), ukEmisija) + "%",
                (ukEmisija - profitEmisija), "NEPROFITABILNE");
            Console.WriteLine(ispis2);
            Console.WriteLine(new String('.', ispis.Length));
        }
    }
}
