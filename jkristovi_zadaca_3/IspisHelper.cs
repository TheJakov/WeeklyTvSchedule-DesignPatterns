using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Decorator;
using jkristovi_zadaca_3.Observer;
using jkristovi_zadaca_3.Visitor;
using jkristovi_zadaca_3.Model.BrisanjePohranjivanje;
using System;
using System.Collections.Generic;
using System.Linq;
using jkristovi_zadaca_3.Model.ChainOfResponsibility;

namespace jkristovi_zadaca_3
{
    static class IspisHelper
    {
        public static int Brojac;
        public static void PrikazGlavnogIzbornika()
        {
            bool izlazak = false;
            while (!izlazak)
            {
                IspisZaglavljaGlavnogIzbornika();
                Console.Write("Moj odabir: ");
                string odabir = Console.ReadLine();

                if (int.TryParse(odabir, out int izbor))
                {
                    if (izbor == 1)
                        OdabirProgramaZaIspis(1);
                    else if (izbor == 2)
                        OdabirProgramaZaIspis(2);
                    else if (izbor == 3)
                        OdabirVrsteEmisijeZaIspis();
                    else if (izbor == 4)
                        OdabirOsobeDostupneURasporedu();
                    else if (izbor == 5)
                        OdabirEmisijeZaBrisanje();
                    else if (izbor == 6)
                        OdabirStanjaZaIspisPodataka();
                    else if (izbor == 7)
                        OdabirStanjaZaVracanje();
                    else if (izbor == 8)
                        OdabirProgramaZaVlastituFunkcionalnost();
                    else if (izbor == 9)
                        izlazak = true;
                    else
                        Console.WriteLine("Ta opcija ne ne postoji!");
                }
                else
                {
                    Console.WriteLine("Ne ispravan unos!");
                }
            }
            Console.WriteLine("\nIzlazak iz program.");
            Console.ReadLine(); //pause
            Environment.Exit(0);
        }

        private static void IspisZaglavljaGlavnogIzbornika()
        {
            Console.WriteLine("\n\n -/- IZBORNIK -/- ");
            Console.WriteLine("(1) Ispis vremenskog plana za odabrani program i dan u tjednu");
            Console.WriteLine("(2) Ispis potencijalnih prihoda " +
                "za odabrani program i dan u tjednu");
            Console.WriteLine("(3) Ispis željene vrste emisije po svim programima i danima");
            Console.WriteLine("(4) Odabir osobe, i zamjena postojeće uloge novom");
            Console.WriteLine("(5) Obriši emisiju temeljem njenog jednoznačnog rednog broja");
            Console.WriteLine("(6) Ispis broja pohranjivanja i podataka svakog pohranjivaja");
            Console.WriteLine("(7) Vraćanje podataka po rednome broju pohranjivanja podataka");
            Console.WriteLine("(8) VLASTITA - Ispis statistike dana ovisno o opciji");
            Console.WriteLine("(9) Izlazak");
        }

        public static string DohvatiOsobeUloge(List<KeyValuePair<int, int>> lista)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            string izlazniString = "";
            if (lista.Count > 0)
            {
                int brojacRedova = 0;
                foreach (var item in lista)
                {
                    Osoba osoba = tvKuca.ListaOsoba.Find(x => x.GetId() == item.Key);
                    Uloga uloga = tvKuca.ListaUloga.Find(x => x.GetId() == item.Value);
                    if (osoba == null || uloga == null)
                    {
                        break;
                    }
                    else
                    {
                        brojacRedova++;
                        if (tvKuca.ListaOsoba.Contains(osoba) && tvKuca.ListaUloga.Contains(uloga))
                        {
                            if (brojacRedova == 1)
                                izlazniString += osoba.GetImePrezime() + " - " + uloga.GetOpis();
                            else
                            {
                                string par = osoba.GetImePrezime() + " - " + uloga.GetOpis();
                                izlazniString += "\n" + new String(' ', 82) +
                                    String.Format("{0,-39}", par);
                            }
                        }
                    }
                }
            } 
            else
            {
                izlazniString = "Nema sudionika";
            }
            return izlazniString;
        }

        public static string DohvatiNazivVrsteEmisije(int idVrsta)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            VrstaEmisije vrsta = tvKuca.ListaVrstaEmisija.Find(x => x.GetId() == idVrsta);
            string naziv = "";
            if (vrsta == null)
            {
                naziv = "Nepoznata vrsta";
                return naziv;
            }            
            naziv = vrsta.GetNaziv();
            return naziv;

        }

        public static void OdabirProgramaZaIspis(int tip)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nOdaberite program: \n");
            for (int i = 0; i < tvKuca.GetCompositeRaspored().GetChildList().Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                    tvKuca.GetCompositeRaspored().GetChildList()[i].GetMojNaziv());
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > tvKuca.GetCompositeRaspored().GetChildList().Count || odabir <= 0)
                {
                    Console.WriteLine("Taj program ne postoji !");
                }
                else
                {
                    if (tip == 1)
                        OdabirDanaProgramaZaIspis(
                            tvKuca.GetCompositeRaspored().GetChildList()[odabir - 1]);
                    else if (tip == 2)
                        OdabirDanaProgramaZaIspisPrihoda(
                            tvKuca.GetCompositeRaspored().GetChildList()[odabir - 1]);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirDanaProgramaZaIspis(IComponentRaspored program)
        {
            Console.WriteLine("\nOdaberite dan: \n");
            for (int i = 1; i < 8; i++)
            {
                Console.WriteLine("(" + i + ") " + UnesiBrojDobijDan(i));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir < 1 || odabir > 7)
                {
                    Console.WriteLine("Taj dan ne postoji !");
                }
                else
                {
                    IspisiVremenskiPlanDana(odabir, program);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void IspisiVremenskiPlanDana(int dan, IComponentRaspored program)
        {
            Console.WriteLine("\n" + UnesiBrojDobijDan(dan) + " - vremenski plan: \n");
            PrikaziZaglavljeDnevnogRasporeda();
            program.GetChildList()[dan - 1].PrikaziPodatke();
        }

        public static string UnesiBrojDobijDan(int broj)
        {
            string izlaz;
            if (broj == 1)
                izlaz = "Ponedeljak";
            else if (broj == 2)
                izlaz = "Utorak";
            else if (broj == 3)
                izlaz = "Srijeda";
            else if (broj == 4)
                izlaz = "Cetvrtak";
            else if (broj == 5)
                izlaz = "Petak";
            else if (broj == 6)
                izlaz = "Subota";
            else
                izlaz = "Nedjelja";
            return izlaz;
        }

        private static void OdabirDanaProgramaZaIspisPrihoda(IComponentRaspored program)
        {
            Console.WriteLine("\nOdaberite dan: \n");
            for (int i = 1; i < 8; i++)
            {
                Console.WriteLine("(" + i + ") " + UnesiBrojDobijDan(i));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir < 1 || odabir > 7)
                {
                    Console.WriteLine("Taj dan ne postoji !");
                }
                else
                {
                    IspisPrihodaZaDan(odabir, program);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void IspisPrihodaZaDan(int dan, IComponentRaspored program)
        {
            Console.WriteLine("\n" + UnesiBrojDobijDan(dan) + " - analiza prihoda: \n");
            PrikaziZaglavljePotencijalnihPrihoda();
       
            int brojProfEmisija = DohvatiBrojProfitabilnihEmisija(dan, program);
                int ukMinuta = DohvatiUkBrojMinutaReklama(dan, program);
                IspisHelper.Brojac = 0;
                IRedakTablice redakTablice =
                    new TekstualniDecorator(
                        new TekstualniDecorator(
                            new ConcreteRedak()));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, ukMinuta.ToString(),
                    brojProfEmisija.ToString());
                Console.WriteLine(ispis);
        }

        private static int DohvatiBrojProfitabilnihEmisija(int dan, IComponentRaspored program)
        {
            int brojEmisija = 0;
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var emisija in program.GetChildList()[dan - 1].GetChildList())
            {
                int vrsta = emisija.GetVrsta();
                VrstaEmisije vrstaEm = tvKuca.ListaVrstaEmisija.Find(x => x.GetId() == vrsta);
                if (vrstaEm != null)
                {
                    if (vrstaEm.GetMozeImatReklame())
                        brojEmisija++;
                }
            }
            return brojEmisija;
        }

        private static int DohvatiUkBrojMinutaReklama(
            int dan, IComponentRaspored program)
        {
            int brojMaksMinuta = 0;
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            IEmisijaVisitor visitor = new EmisijaMonetizacijaVizitor();
            foreach (var emisija in program.GetChildList()[dan - 1].GetChildList())
            {
                Emisija em = emisija as Emisija;
                brojMaksMinuta += em.Accept(visitor);
            }
            return brojMaksMinuta;
        }

        private static void OdabirOsobeDostupneURasporedu()
        {
            List<int> listaIdOsoba = DohvatiDistinctOsobeRasporeda();
            Console.WriteLine("\nOdaberite osobu kojoj želite mijenjati ulogu: \n");
            for (int i = 0; i < listaIdOsoba.Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                    DohvatiImeOsobePoId(listaIdOsoba[i]));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > listaIdOsoba.Count || odabir <= 0)
                {
                    Console.WriteLine("Ta osoba ne postoji !");
                }
                else
                {
                    OdabirUlogeDostupneZaOsobuURasporedu(listaIdOsoba[odabir-1]);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirUlogeDostupneZaOsobuURasporedu(int osobaId)
        {
            List<int> listaIdUloga = DohvatiDistinctUlogeZaNekuOsobu(osobaId);
            Console.WriteLine("\nOdabrana osoba: " + DohvatiImeOsobePoId(osobaId));
            Console.WriteLine("Odaberite dostupnu ulogu koju želite mijenjati: \n");
            for (int i = 0; i < listaIdUloga.Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                   DohvatiNazivUlogePoId(listaIdUloga[i]));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > listaIdUloga.Count || odabir <= 0)
                {
                    Console.WriteLine("Ta uloga ne postoji !");
                }
                else
                {
                    OdabirNoveUlogeZaOsobuURasporedu(osobaId, listaIdUloga[odabir - 1]);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirNoveUlogeZaOsobuURasporedu(int osobaId, int ulogaId)
        {
            List<int> listaIdSvihUloga = DohvatiSveIdUlogaIzTvKuce();
            listaIdSvihUloga.Remove(ulogaId); //Micem staru
            Console.WriteLine("\nOdabrana osoba: " + DohvatiImeOsobePoId(osobaId));
            Console.WriteLine("Odabrana uloga za zamjenu: " + DohvatiNazivUlogePoId(ulogaId));
            Console.WriteLine("Odaberite neku postojeću ulogu koju želite postaviti: \n");
            for (int i = 0; i < listaIdSvihUloga.Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                   DohvatiNazivUlogePoId(listaIdSvihUloga[i]));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > listaIdSvihUloga.Count || odabir <= 0)
                {
                    Console.WriteLine("Ta uloga ne postoji !");
                }
                else
                {
                    SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
                    subject.SetOsobaUlogaStaro(new KeyValuePair<int, int>(osobaId, ulogaId));
                    subject.SetOsobaUlogaNovo(new
                        KeyValuePair<int, int>(osobaId, listaIdSvihUloga[odabir - 1]));
                    Console.WriteLine("\nUspjesno zamjenjena uloga '" 
                        + DohvatiNazivUlogePoId(ulogaId)
                        + "' sa ulogom '" + DohvatiNazivUlogePoId(listaIdSvihUloga[odabir - 1])
                        + "' kod osobe - " + DohvatiImeOsobePoId(osobaId));
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirEmisijeZaBrisanje()
        {
            List<Emisija> listaSveEmisije = BPHelper.DohvatiListuSvihEmisijaUCompositeu();
            Console.WriteLine("Odaberite redni broj emisije koju želite izbrisati: \n");
            PrikaziZaglavljeRedniBrojNazivEmisije();
            for (int i = 0; i < listaSveEmisije.Count; i++)
            {
                IspisHelper.Brojac = 0;
                IRedakTablice redakTablice =
                    new KratkiTekstDecorator(
                        new TekstualniDecorator(
                            new CjelobrojniDecorator(
                                new ConcreteRedak())));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, "\n" + new String('.', 52),
                    listaSveEmisije[i].GetNaziv(), listaSveEmisije[i].GetRedniBroj());
                Console.WriteLine(ispis);
            }
            Console.Write("\nMoj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > listaSveEmisije.Count || odabir <= 0)
                {
                    Console.WriteLine("Odabrani redni broj emisije ne postoji !");
                }
                else
                {
                    BPHelper.IzbrisiEmisiju(odabir);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirStanjaZaIspisPodataka()
        {
            Console.WriteLine("\nPostoji ukupno "+BPHelper.ListaPohranjenihStanja.Count+
                " pohranjenih stanja.");
            Console.WriteLine("Odaberite redni broj stanja čije podatke želite pregledati: \n");
            PrikaziZaglavljeTablicaSvihStanja();
            foreach (var stanje in BPHelper.ListaPohranjenihStanja)
            {
                Brojac = 0;
                IRedakTablice redakTablice =
                    new KratkiTekstDecorator(
                        new TekstualniDecorator(
                            new KratkiTekstDecorator(
                                new CjelobrojniDecorator(
                                    new ConcreteRedak()))));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, "\n" + new String('.', 109), stanje.GetOpis(), 
                    stanje.GetVrijemePohrane().ToShortTimeString() + ":"
                    + stanje.GetVrijemePohrane().TimeOfDay.Seconds, stanje.GetRedniBroj());
                Console.WriteLine(ispis);
            }
            Console.Write("\nMoj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > BPHelper.ListaPohranjenihStanja.Count || odabir <= 0)
                {
                    Console.WriteLine("Odabrani redni broj pohranjivanja podataka ne postoji !");
                }
                else
                {
                    IspisPodatakaPohranjenogStanja(odabir-1);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirStanjaZaVracanje()
        {
            Console.WriteLine("\nPostoji ukupno " + BPHelper.ListaPohranjenihStanja.Count +
                " pohranjenih stanja.");
            Console.WriteLine("Odaberite redni broj stanja na koje se zelite vratiti: \n");
            PrikaziZaglavljeTablicaSvihStanja();
            foreach (var stanje in BPHelper.ListaPohranjenihStanja)
            {
                Brojac = 0;
                IRedakTablice redakTablice =
                    new KratkiTekstDecorator(
                        new TekstualniDecorator(
                            new KratkiTekstDecorator(
                                new CjelobrojniDecorator(
                                    new ConcreteRedak()))));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, "\n" + new String('.', 109), stanje.GetOpis(),
                    stanje.GetVrijemePohrane().ToShortTimeString() + ":"
                    + stanje.GetVrijemePohrane().TimeOfDay.Seconds, stanje.GetRedniBroj());
                Console.WriteLine(ispis);
            }
            Console.Write("\nMoj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > BPHelper.ListaPohranjenihStanja.Count || odabir <= 0)
                {
                    Console.WriteLine("Odabrani redni broj pohranjivanja podataka ne postoji !");
                }
                else
                {
                    BPHelper.VracanjeNaStanje(odabir - 1);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void IspisPodatakaPohranjenogStanja(int redbrStanja)
        {
            CompositeRaspored cvor = BPHelper.ListaPohranjenihStanja[redbrStanja].GetPocetniCvor();
            foreach (var program in cvor.GetChildList())
            {
                int brojac = 1;
                foreach (var dan in program.GetChildList())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n" + program.GetMojNaziv() + 
                        " - " + UnesiBrojDobijDan(brojac));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    PrikaziZaglavljeDnevnogRasporeda();
                    dan.PrikaziPodatke();
                    brojac++;
                }
            }
        }

        private static void OdabirProgramaZaVlastituFunkcionalnost()
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nOdaberite program: \n");
            for (int i = 0; i < tvKuca.GetCompositeRaspored().GetChildList().Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                    tvKuca.GetCompositeRaspored().GetChildList()[i].GetMojNaziv());
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > tvKuca.ListaTvPrograma.Count || odabir <= 0)
                {
                    Console.WriteLine("Taj program ne postoji !");
                }
                else
                {
                    OdabirDanaZaVlastituFunkcionalonost(
                        tvKuca.GetCompositeRaspored().GetChildList()[odabir-1]
                    );
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirDanaZaVlastituFunkcionalonost(IComponentRaspored program)
        {
            Console.WriteLine("\nOdaberite dan: \n");
            for (int i = 1; i < 8; i++)
            {
                Console.WriteLine("(" + i + ") " + UnesiBrojDobijDan(i));
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir < 1 || odabir > 7)
                {
                    Console.WriteLine("Taj dan ne postoji !");
                }
                else
                {
                    OdabirOpcijeZaVlastituFunkcionalnost(odabir, program);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void OdabirOpcijeZaVlastituFunkcionalnost
            (int dan, IComponentRaspored program)
        {
            Console.WriteLine("\nOdaberite opciju: \n");
            Console.WriteLine("(1) Udio pojedine vrste emisije u ovome danu");
            Console.WriteLine("(2) Udio profitablinih emisija u ovome danu");
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir < 1 || odabir > 2)
                {
                    Console.WriteLine("Ta opcija ne postoji !");
                }
                else
                {
                    //Stvaranje lanca
                    AbstractStatisticHandler handler =
                        new VrstaStatHandler(AbstractStatisticHandler.VRSTA).SetNext(
                            new ProfitStatHandler(AbstractStatisticHandler.PROFIT));
                    handler.DoWork(odabir, program.GetChildList()[dan - 1]);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static List<int> DohvatiDistinctOsobeRasporeda()
        {
            List<int> listaIDOsoba = new List<int>();
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var program in tvKuca.GetCompositeRaspored().GetChildList())
            {
                foreach (var dan in program.GetChildList())
                {
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        if(em != null)
                        {
                            foreach (var par in em.GetListaOsobaUloga())
                            {
                                if (!listaIDOsoba.Contains(par.Key))
                                    listaIDOsoba.Add(par.Key);
                            }
                        }
                    }                   
                }
            }
            return listaIDOsoba;
        }

        private static List<int> DohvatiDistinctUlogeZaNekuOsobu(int idOsoba)
        {
            List<int> listaIDUloga = new List<int>();
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var program in tvKuca.GetCompositeRaspored().GetChildList())
            {
                foreach (var dan in program.GetChildList())
                {
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        if (em != null)
                        {
                            foreach (var par in em.GetListaOsobaUloga())
                            {
                                if (par.Key == idOsoba && !listaIDUloga.Contains(par.Value))
                                    listaIDUloga.Add(par.Value);
                            }
                        }
                    }
                }
            }
            return listaIDUloga;
        }

        private static List<int> DohvatiSveIdUlogaIzTvKuce()
        {
            List<int> listaIDUloga = new List<int>();
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (var uloga in tvKuca.ListaUloga)
            {
                listaIDUloga.Add(uloga.GetId());
            }
            return listaIDUloga;
        }

        private static string DohvatiImeOsobePoId(int id)
        {
            string izlaznoIme = "Nepoznato";
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Osoba osoba = tvKuca.ListaOsoba.Find(x => x.GetId() == id);
            if(osoba != null)
            {
                izlaznoIme = osoba.GetImePrezime();
            }
            return izlaznoIme;
        }

        private static string DohvatiNazivUlogePoId(int id)
        {
            string izlaznaUloga = "Nepoznato";
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Uloga uloga = tvKuca.ListaUloga.Find(x => x.GetId() == id);
            if (uloga != null)
            {
                izlaznaUloga = uloga.GetOpis();
            }
            return izlaznaUloga;
        }

        public static void OdabirProgramaZaIspisStatistike()
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nOdaberite program: \n");
            for (int i = 0; i < tvKuca.GetCompositeRaspored().GetChildList().Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                    tvKuca.GetCompositeRaspored().GetChildList()[i].GetMojNaziv());
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > tvKuca.ListaTvPrograma.Count || odabir <= 0)
                {
                    Console.WriteLine("Taj program ne postoji !");
                }
                else
                {
                    IspisStatistikeZaProgram(tvKuca.ListaTvPrograma[odabir - 1]);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }

        private static void IspisStatistikeZaProgram(TvProgram program)
        {
            Console.WriteLine(program.GetNaziv() + " - Prikaz statistike\n");
            for (int i = 0; i < program.GetTjedan().Count; i++)
            {
                Console.WriteLine(UnesiBrojDobijDan(i + 1) + " - statiska: ");

                //dvije emisije su mi uvijek zapravo signal TV kuce
                Console.WriteLine("\t - Broj emisija: " +
                    (program.GetTjedan()[i].GetChildList().Count - 2));

                IspisPodatakaEmitiranjaEmisija(program, i);
                IspisPodatakaEmitiranjaTvSignala(program, i);
                IspisPodatakaSlobodnogVremena(program, i);
            }
        }

        private static int DohvatiBrojMinutaEmitiranjaEmisija(List<IComponentRaspored> danSortiran)
        {
            int ukupnoMinuta = 0;
            for (int i = 1; i < danSortiran.Count - 1; i++)
            {
                ukupnoMinuta += danSortiran[i].GetTrajanje();
            }
            return ukupnoMinuta;
        }

        private static int DohvatiBrojMinutaEmitSignalaTvKuce(List<IComponentRaspored> danSortiran)
        {
            //Prva i zadnja emisija je uvijek emitirani signal TV kuce
            int ukupnoMinuta = danSortiran[0].GetTrajanje() +
                danSortiran[danSortiran.Count - 1].GetTrajanje();
            return ukupnoMinuta;
        }

        private static int DohvatiBrojMinutaSlobodnogVremena(int minEmisije, int minSignalTvKuce)
        {
            int ukupnoMinuta = 1440 - (minEmisije + minSignalTvKuce);
            return ukupnoMinuta;
        }

        private static double IzracunajUdioMinutaUDanu(int brojMinuta)
        {
            double izlaz = (double)brojMinuta / (double)1440;
            return Math.Round(izlaz * 100, 2);
        }

        private static void IspisPodatakaEmitiranjaEmisija(TvProgram program, int i)
        {
            Console.WriteLine("\t - Emisije se emitiraju ukupno " +
                    DohvatiBrojMinutaEmitiranjaEmisija(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>())
                    + " minuta, time prekrivajuci oko " +
                    IzracunajUdioMinutaUDanu(DohvatiBrojMinutaEmitiranjaEmisija(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>()))
                    + "% dnevnog vremena.");
        }

        private static void IspisPodatakaEmitiranjaTvSignala(TvProgram program, int i)
        {
            Console.WriteLine("\t - Signal TV kuce se emitira ukupno " +
                    DohvatiBrojMinutaEmitSignalaTvKuce(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>()) +
                    " minuta, time prekrivajuci oko " +
                    IzracunajUdioMinutaUDanu(DohvatiBrojMinutaEmitSignalaTvKuce(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>()))
                    + "% dnevnog vremena.");
        }

        private static void IspisPodatakaSlobodnogVremena(TvProgram program, int i)
        {
            Console.WriteLine("\t - Preostalo slobodno vrijeme iznosi ukupno " +
                    DohvatiBrojMinutaSlobodnogVremena
                    (
                    DohvatiBrojMinutaEmitiranjaEmisija(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>()),
                    DohvatiBrojMinutaEmitSignalaTvKuce(
                        program.GetTjedan()[i].GetChildList().OrderBy
                        (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>())
                    )
                    + " minuta, time prekrivajuci oko " +
                    IzracunajUdioMinutaUDanu
                    (
                        DohvatiBrojMinutaSlobodnogVremena
                        (
                        DohvatiBrojMinutaEmitiranjaEmisija(
                            program.GetTjedan()[i].GetChildList().OrderBy
                            (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>()),
                        DohvatiBrojMinutaEmitSignalaTvKuce(
                            program.GetTjedan()[i].GetChildList().OrderBy
                            (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>())
                        )
                    )
                    + "% dnevnog vremena.");
        }

        private static void OdabirVrsteEmisijeZaIspis()
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nOdaberite vrstu emisije: \n");
            for (int i = 0; i < tvKuca.ListaVrstaEmisija.Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " +
                    tvKuca.ListaVrstaEmisija[i].GetNaziv());
            }
            Console.Write("Moj odabir: ");
            string korisnickiUnos = Console.ReadLine();
            if (int.TryParse(korisnickiUnos, out int odabir))
            {
                if (odabir > tvKuca.ListaVrstaEmisija.Count || odabir <= 0)
                {
                    Console.WriteLine("Taj vrsta ne postoji !");
                }
                else
                {
                    IspisSveEmisijeSaVrstom(odabir - 1);
                }
            }
            else
            {
                Console.WriteLine("Ne ispravan unos!");
            }
        }
        
        private static void IspisSveEmisijeSaVrstom(int indeks)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            int idVrsta = tvKuca.ListaEmisija[indeks].GetId();
            Console.WriteLine("\nVrsta emisije: "+tvKuca.ListaVrstaEmisija[indeks].GetNaziv()
                +" - sva pojavljivanja u rasporedu: \n");
            PrikaziZaglavljeDnevnogRasporeda();
            tvKuca.GetCompositeRaspored().PrikaziPodatkeVrsta(idVrsta);
        }

        public static void IspisGreskeRedak(int brojRetka, string redak)
        {
            Console.WriteLine(" - {0}. redak nije ispravno zadan podatak. PRESKAČEM. --> {1}",
                brojRetka, redak);
        }

        public static void EmisijaNemoguceDodati(int emId,  int dan, string naziv)
        {
            Console.WriteLine(" - Emisiju sa ID: " + emId +
                            " nije bilo moguce dodati u kod " + dan +
                            ". dana u raspored programa: " + naziv);
        }
        public static void EmisijaNemoguceDodati(int emId, string naziv)
        {
            Console.WriteLine(" - Emisiju sa ID: " + emId +
                            " nije bilo moguce dodati u nigdje " +
                            "u raspored programa: " + naziv);
        }

        public static void EmisijaNePostoji(int emId)
        {
            Console.WriteLine(" - Emisija sa ID: " + emId + " ne postoji! -> PRESKAČEM");
        }

        public static void PrikaziZaglavljeDnevnogRasporeda()
        {
            Brojac = 0;
            IRedakTablice redakTablice =
                new KratkiTekstDecorator(
                    new KratkiTekstDecorator(
                        new CjelobrojniDecorator(
                            new CjelobrojniDecorator(
                                new CjelobrojniDecorator(
                                    new TekstualniDecorator(
                                        new TekstualniDecorator(
                                            new TekstualniDecorator(
                                                new ConcreteRedak()))))))));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format,
                "KRAJ", "POČETAK", "TRAJANJE", "ID EMISIJE", "REDNI BR.",
                "SUDIONICI", "VRSTA EMISIJE", "NAZIV EMISIJE");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('_',ispis.Length));
        }

        private static void PrikaziZaglavljePotencijalnihPrihoda()
        {
            Brojac = 0;
            IRedakTablice redakTablice =
                    new TekstualniDecorator(
                        new TekstualniDecorator(
                            new ConcreteRedak()));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format,"UK. BR. PROFITABILNIH MINUTA", 
                "UK. BR. PROFITABILNIH EMISIJA");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('_', ispis.Length));
        }

        private static void PrikaziZaglavljeRedniBrojNazivEmisije()
        {
            Brojac = 0;
            IRedakTablice redakTablice =
                    new TekstualniDecorator(
                        new CjelobrojniDecorator(
                            new ConcreteRedak()));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format, "NAZIV", "REDNI BROJ");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('_', ispis.Length));
        }

        private static void PrikaziZaglavljeTablicaSvihStanja()
        {
            Brojac = 0;
            IRedakTablice redakTablice =
                    new TekstualniDecorator(
                        new KratkiTekstDecorator(
                            new CjelobrojniDecorator(
                                new ConcreteRedak())));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format, "OPIS", "VRIJEME", "REDNI BROJ");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('_', 109));
        }

        public static void PrikaziZagavljeTablicaStatVrsta()
        {
            Brojac = 0;
            IRedakTablice redakTablice =
                    new KratkiTekstDecorator(
                        new CjelobrojniDecorator(
                            new TekstualniDecorator(
                                new ConcreteRedak())));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format, "POSTOTAK", "KOLIČINA", "NAZIV VRSTE");
            Console.WriteLine(ispis);
            Console.WriteLine(new String('_', ispis.Length));
        }
    }
}
