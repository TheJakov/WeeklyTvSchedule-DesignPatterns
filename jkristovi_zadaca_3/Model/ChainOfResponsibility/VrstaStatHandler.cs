using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Decorator;

namespace jkristovi_zadaca_3.Model.ChainOfResponsibility
{
    public class VrstaStatHandler : AbstractStatisticHandler
    {
        public VrstaStatHandler(int opcija)
        {
            Opcija = opcija;
        }

        public override void Calculate(IComponentRaspored component)
        {
            List<string> listaNazivaVrsta = new List<string>();
            List<int> pratecaListaCounterVrsta = new List<int>();
            foreach (var emisija in component.GetChildList())
            {
                Emisija em = emisija as Emisija;
                string vrsta = IspisHelper.DohvatiNazivVrsteEmisije(em.GetVrsta());
                if (vrsta == "Nepoznata vrsta")
                {
                    //onda nista, ovo je za emisije koje predstavljaju TV signal
                }
                else if (!listaNazivaVrsta.Contains(vrsta))
                {
                    listaNazivaVrsta.Add(vrsta);
                    pratecaListaCounterVrsta.Add(1);
                }
                else
                {
                    pratecaListaCounterVrsta[listaNazivaVrsta.IndexOf(vrsta)] += 1;
                }
            }
            IspisiStatistiku(listaNazivaVrsta, pratecaListaCounterVrsta);
        }

        private static void IspisiStatistiku(List<string> listaVrsta, List<int> listaKolicina)
        {
            int ukEmisija = 0;
            foreach (var broj in listaKolicina)
            {
                ukEmisija += broj;
            }
            Console.WriteLine("\nOvaj dan sadrži ukupno " + ukEmisija + " emisija.\n");
            IspisHelper.PrikaziZagavljeTablicaStatVrsta();
            foreach (string naziv in listaVrsta)
            {
                IspisHelper.Brojac = 0;
                IRedakTablice redakTablice =
                        new KratkiTekstDecorator(
                            new CjelobrojniDecorator(
                                new TekstualniDecorator(
                                    new ConcreteRedak())));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, 
                    DobijPostotak(listaKolicina[listaVrsta.IndexOf(naziv)],ukEmisija)+"%",
                    listaKolicina[listaVrsta.IndexOf(naziv)], naziv);
                Console.WriteLine(ispis);
                Console.WriteLine(new String('.', ispis.Length));
            }
        }
        
    }
}
