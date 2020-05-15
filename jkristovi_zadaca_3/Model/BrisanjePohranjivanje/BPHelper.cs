using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Model.BrisanjePohranjivanje
{
    static class BPHelper
    {
        public static List<PohranjenoStanje> ListaPohranjenihStanja = new List<PohranjenoStanje>();
        public static List<Emisija> DohvatiListuSvihEmisijaUCompositeu()
        {
            List<Emisija> lista = new List<Emisija>();
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var program in tvKuca.GetCompositeRaspored().GetChildList())
            {
                foreach (var dan in program.GetChildList())
                {
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        if (em.GetRedniBroj() != -1)
                        {
                            // -1 je fiksno signal TV kuce
                            lista.Add(em);
                        }
                    }
                }
            }
            return lista.OrderBy(x=>x.GetRedniBroj()).ToList();
        }

        public static void IzbrisiEmisiju(int redniBroj)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            int indeksPrograma = 0;
            int indeksDana = 0;
            foreach (var program in tvKuca.GetCompositeRaspored().GetChildList())
            {
                foreach (var dan in program.GetChildList())
                {
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        if(em.GetRedniBroj() == redniBroj)
                        {
                            ZabiljeziStanje(em, indeksPrograma, indeksDana);
                            dan.GetChildList().Remove(emisija);
                            Console.WriteLine("Emisija uspješno izbrisana iz Composite-a!");
                            break;
                        }
                    }
                    indeksDana++;
                }
                indeksPrograma++;
            }
        }

        private static void ZabiljeziStanje(Emisija brisanaEm, int program, int dan)
        {
            PohranjenoStanje stanje = new PohranjenoStanje();
            stanje.SetRedniBroj(ListaPohranjenihStanja.Count + 1);
            stanje.SetVrijemePohrane(DateTime.Now);

            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            stanje.SetPocetniCvor(tvKuca.GetCompositeRaspored());

            stanje.SetOpis("Stanje prije brisanja emisije (Redni br: " + brisanaEm.GetRedniBroj() 
                + ") '" + brisanaEm.GetNaziv() + "' -> " +
                tvKuca.GetCompositeRaspored().GetChildList()[program].GetMojNaziv() + 
                " - " + IspisHelper.UnesiBrojDobijDan(dan+1));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Spremam novo stanje: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(stanje.GetOpis());
            ListaPohranjenihStanja.Add(stanje);
        }

        public static void VracanjeNaStanje(int indeks)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            CompositeRaspored cvorStanjaNaKojeSeVracam =
                NapraviKopijuCvoraStanja(ListaPohranjenihStanja[indeks]);
            tvKuca.Raspored = cvorStanjaNaKojeSeVracam;

            //dalje je ispis
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nTrenutno stanje je postavljeno na: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            IspisHelper.Brojac = 0;
            IRedakTablice redakTablice =
                new KratkiTekstDecorator(
                    new TekstualniDecorator(
                        new KratkiTekstDecorator(
                            new CjelobrojniDecorator(
                                new ConcreteRedak()))));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format, "\n" + new String('.', 109), 
                ListaPohranjenihStanja[indeks].GetOpis(),
                ListaPohranjenihStanja[indeks].GetVrijemePohrane().ToShortTimeString() + ":"
                + ListaPohranjenihStanja[indeks].GetVrijemePohrane().TimeOfDay.Seconds,
                ListaPohranjenihStanja[indeks].GetRedniBroj());
            Console.WriteLine(ispis);

        }

        private static CompositeRaspored NapraviKopijuCvoraStanja(PohranjenoStanje stanje)
        {
            CompositeRaspored noviPocetniCvor = new CompositeRaspored();
            int indeksPrograma = 0;
            foreach (var program in stanje.GetPocetniCvor().GetChildList())
            {
                CompositeRaspored noviProgram = new CompositeRaspored();
                noviProgram.SetMojNaziv(program.GetMojNaziv());
                noviPocetniCvor.AddChild(noviProgram);
                int indeksDana = 0;
                foreach (var dan in program.GetChildList())
                {
                    CompositeRaspored noviDan = new CompositeRaspored();
                    noviPocetniCvor.GetChildList()[indeksPrograma].AddChild(noviDan);
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        Emisija nova = TjedniPlanHelper.VratiNoviKopiraniObjekt(em);
                        nova.SetRedniBroj(em.GetRedniBroj());
                        nova.SetVrijemePrikazivanja(em.GetVrijemePrikazivanja());
                        noviPocetniCvor.GetChildList()[indeksPrograma]
                            .GetChildList()[indeksDana].AddChild(nova);
                    }
                    indeksDana++;
                }
                indeksPrograma++;
            }
            return noviPocetniCvor;
        }
    }
}
