using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    static class TjedniPlanHelper
    {
        public static int RedniBrojDodaneEmisije = 0;

        private static void DodjeliRedniBroj(Emisija emisija)
        {
            RedniBrojDodaneEmisije++;
            emisija.SetRedniBroj(RedniBrojDodaneEmisije);

        }
        public static void PopunjavanjeTjednogRasporeda()
        {
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem stvaranje vremenskog rasporeda TV programa.");
            try
            {
                foreach (TvProgram program in TVKuca.ListaTvPrograma)
                {
                    PopuniTjedniPlan(program);
                }
            }
            catch
            {
                Console.WriteLine("Greška kod stvaranja vremenskog rasporeda TV programa." +
                    "\nGasim program.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("\nZavršeno stvaranje vremenskog rasporeda TV programa.");
        }

        public static void PopuniTjedniPlan(TvProgram program)
        {
            List<VremenskiPlan> listaZadaniPocetakDani;
            List<VremenskiPlan> listaEmisijaViseDana;
            List<VremenskiPlan> listaEmisijaBezIcega;
            listaZadaniPocetakDani = DohvatiEmisijeZadPocetIDani(program.GetVremenskiPlan());
            listaEmisijaViseDana = DohvatiEmisijeJedanIliViseDana(program.GetVremenskiPlan());
            listaEmisijaBezIcega = DohvatiEmisijeBezIcega(program.GetVremenskiPlan());
            DodajEmisijeZadaniPocetak(program, listaZadaniPocetakDani);
            DodajEmisijeJedanIliViseDana(program, listaEmisijaViseDana);
            DodajEmisijeBezIcega(program, listaEmisijaBezIcega);
            DodajEmitiranjeSingnalaTVKucePocetak(program);
            DodajEmitiranjeSingnalaTVKuceKraj(program);
            SortirajSveEmisijeUDanu(program);
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            tvKuca.SetCompositeRaspored();
        }

        private static List<VremenskiPlan> DohvatiEmisijeZadPocetIDani(List<VremenskiPlan> plan)
        {
            List<VremenskiPlan> izlaznaLista = new List<VremenskiPlan>();

            foreach (VremenskiPlan item in plan)
            {
                if ((item.GetPocetak() != DateTime.MinValue) && 
                    (item.GetListaDanaEmitiranja().Count >= 1))
                {
                    izlaznaLista.Add(item);
                }
            }
            return izlaznaLista;
        }

        private static List<VremenskiPlan> DohvatiEmisijeJedanIliViseDana(List<VremenskiPlan> plan)
        {
            List<VremenskiPlan> izlaznaLista = new List<VremenskiPlan>();

            foreach (VremenskiPlan item in plan)
            {
                if ((item.GetPocetak() == DateTime.MinValue) && 
                    (item.GetListaDanaEmitiranja().Count >= 1))
                {
                    izlaznaLista.Add(item);
                }
            }
            return izlaznaLista;
        }

        private static List<VremenskiPlan> DohvatiEmisijeBezIcega(List<VremenskiPlan> plan)
        {
            List<VremenskiPlan> izlaznaLista = new List<VremenskiPlan>();

            foreach (VremenskiPlan item in plan)
            {
                if ((item.GetPocetak() == DateTime.MinValue) && 
                    (item.GetListaDanaEmitiranja().Count == 0))
                {
                    izlaznaLista.Add(item);
                }
            }
            return izlaznaLista;
        }

        private static void DodajEmisijeZadaniPocetak(
            TvProgram program, List<VremenskiPlan> listaZadaniPocetakDani)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
            foreach (VremenskiPlan plan in listaZadaniPocetakDani)
            {
                foreach (int brojDana in plan.GetListaDanaEmitiranja())
                {
                    Emisija trazena  = tvKuca.ListaEmisija.Find
                        (x => x.GetId() == plan.GetEmisijaId());
                    if (trazena == null)
                    {
                        Console.WriteLine(" - Emisija sa ID: "+ plan.GetEmisijaId()+
                            " ne postoji! -> PRESKAČEM");
                        break;
                    }
                    Emisija emisijaZaDodat = VratiNoviKopiraniObjekt(trazena);
                    emisijaZaDodat.SetVrijemePrikazivanja(plan.GetPocetak());
                    DodajNoveOsobeUlogeEmisije(emisijaZaDodat, plan);
                    if (ProvjeriMogucnostDodavanja
                        (emisijaZaDodat, program.GetTjedan()[brojDana - 1], program))
                    {
                        DodjeliRedniBroj(emisijaZaDodat);
                        program.GetTjedan()[(brojDana-1)].AddChild(emisijaZaDodat);
                        subject.AddObserver(emisijaZaDodat);
                    }
                    else
                    {
                        Console.WriteLine(" - "+brojDana+".dan - Emisiju: "+emisijaZaDodat.GetId()
                        +" vrijeme pocetka: "+emisijaZaDodat.GetVrijemePrikazivanja().TimeOfDay
                        +", "+emisijaZaDodat.GetNaziv()+" - trajanje: "+
                        emisijaZaDodat.GetTrajanje() + " nije bilo moguće dodati u raspored.");
                    }
                }
            }
        }

        private static void DodajEmisijeJedanIliViseDana(
            TvProgram program, List<VremenskiPlan> listaEmisijeViseDana)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (VremenskiPlan plan in listaEmisijeViseDana)
            {
                foreach (int brojDana in plan.GetListaDanaEmitiranja())
                {
                    bool uspioDodati = false;
                    Emisija trazena=tvKuca.ListaEmisija.Find(x=>x.GetId() == plan.GetEmisijaId());
                    if (trazena == null)
                    {
                        IspisHelper.EmisijaNePostoji(plan.GetEmisijaId());
                        break;
                    }
                    if (program.GetTjedan()[brojDana - 1].GetChildList().Count == 0)
                    {
                        DodajEmisijuNaPocetakPrograma(program, trazena, plan, brojDana);
                        uspioDodati = true;
                    }
                    else if (program.GetTjedan()[brojDana - 1].GetChildList().Count == 1)
                    {
                        if (UspioDodatPrijePrve(program, trazena, plan, brojDana))
                            uspioDodati = true;
                        else if (UspioDodatNakonZadnje(program, trazena, plan, brojDana))
                            uspioDodati = true;
                    }
                    else
                    {
                        if (UspioDodatPrijePrve(program, trazena, plan, brojDana))
                            uspioDodati = true;
                        else if (UspioDodatiIzmeduDvije(program, trazena, plan, brojDana))
                            uspioDodati = true;
                        else if (UspioDodatNakonZadnje(program, trazena, plan, brojDana))
                            uspioDodati = true;
                    }

                    if (!uspioDodati)
                    {
                        IspisHelper.EmisijaNemoguceDodati(
                            plan.GetEmisijaId(), brojDana, program.GetNaziv());
                    }
                }
            }
        }

        private static void DodajEmisijeBezIcega(
            TvProgram program, List<VremenskiPlan> listaEmisijeBezIcega)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            foreach (VremenskiPlan plan in listaEmisijeBezIcega)
            {
                Emisija trazena = tvKuca.ListaEmisija.Find(x => x.GetId() == plan.GetEmisijaId());
                if (trazena == null)
                {
                    IspisHelper.EmisijaNePostoji(plan.GetEmisijaId());
                    break;
                }
                bool uspioDodati = false;
                for (int i = 0; i < program.GetTjedan().Count; i++)
                {
                    if (program.GetTjedan()[i].GetChildList().Count == 0)
                    {
                        DodajEmisijuNaPocetakPrograma(program, trazena, plan, (i+1));
                        uspioDodati = true;
                        break;
                    }
                    else if (program.GetTjedan()[i].GetChildList().Count == 1)
                    {
                        if (UspioDodatPrijePrve(program, trazena, plan, (i + 1)))
                        {
                            uspioDodati = true;
                            break;
                        }                           
                        else if (UspioDodatNakonZadnje(program, trazena, plan, (i + 1)))
                        {
                            uspioDodati = true;
                            break;
                        }
                    }
                    else
                    {
                        if (UspioDodatPrijePrve(program, trazena, plan, (i + 1)))
                        {
                            uspioDodati = true;
                            break;
                        }
                        else if (UspioDodatiIzmeduDvije(program, trazena, plan, (i + 1)))
                        {
                            uspioDodati = true;
                            break;
                        }
                        else if (UspioDodatNakonZadnje(program, trazena, plan, (i + 1)))
                        {
                            uspioDodati = true;
                            break;
                        }
                    }
                }
                if (!uspioDodati)
                    IspisHelper.EmisijaNemoguceDodati(plan.GetEmisijaId(), program.GetNaziv());
            }
        }

        private static bool ProvjeriMogucnostDodavanja(
            Emisija emisija, IComponentRaspored dan, TvProgram program)
        {
            bool moguceDodati = true;
            if(program.GetPocetakPrikazivanja().TimeOfDay > 
                emisija.GetVrijemePrikazivanja().TimeOfDay)
            {
                moguceDodati = false;
            }
            if(PostojiEmisijaKojaPocinjeUNavedenoVrijeme(dan, emisija.GetVrijemePrikazivanja()))
            {
                moguceDodati = false;
            }
            if(PostojiEmisijaKojaSeEmitiraUNavedenoVrijeme(
                dan, emisija.GetVrijemePrikazivanja(),emisija.GetTrajanje()))
            {
                moguceDodati = false;
            }
            if(PrelaziPonocaXkrajPrograma(emisija, program))
            {
                moguceDodati = false;
            }
            return moguceDodati;
        }
        private static bool PostojiEmisijaKojaPocinjeUNavedenoVrijeme(
            IComponentRaspored dan, DateTime vrijeme)
        {
            bool postoji = false;
            foreach (var item in dan.GetChildList())
            {
                if (item.GetVrijemePrikazivanja().TimeOfDay == vrijeme.TimeOfDay)
                {
                    postoji = true;
                }
            }
            return postoji;
        }
        private static bool PostojiEmisijaKojaSeEmitiraUNavedenoVrijeme(
            IComponentRaspored dan, DateTime vrijeme, int trajanje)
        {
            bool postoji = false;
            foreach (var item in dan.GetChildList())
            {
                DateTime calcTimePocetak = new DateTime();
                calcTimePocetak = item.GetVrijemePrikazivanja();

                DateTime calcTimeKraj = new DateTime();
                calcTimeKraj = item.GetVrijemePrikazivanja();
                calcTimeKraj.AddMinutes(item.GetTrajanje());

                if ((calcTimePocetak.TimeOfDay < vrijeme.TimeOfDay) && 
                    (calcTimeKraj.TimeOfDay > vrijeme.TimeOfDay))
                {
                    postoji = true;
                }
                else if ((calcTimePocetak.TimeOfDay > vrijeme.TimeOfDay) &&
                         (calcTimeKraj.TimeOfDay < vrijeme.AddMinutes(trajanje).TimeOfDay))
                {
                    postoji = true;
                }
            }
            return postoji;
        }
        private static DateTime DohvatiVrijemeGdjeEmisijuMoguceUbacitiIzmeduDvijeEmisije(
            IComponentRaspored dan, int trajanje)
        {
            DateTime vratiDatum = DateTime.MinValue;
            List<IComponentRaspored> sortiranDan = dan.GetChildList().OrderBy
                (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>();            
            for (int i = 0; i < sortiranDan.Count-1; i++)
            {
                DateTime calcItemKraj = new DateTime();
                calcItemKraj = sortiranDan[i].GetVrijemePrikazivanja()
                    .AddMinutes(sortiranDan[i].GetTrajanje());

                DateTime vrijemePocetka = new DateTime();
                vrijemePocetka = calcItemKraj;

                DateTime calcNextItemPocetak = new DateTime();
                calcNextItemPocetak = sortiranDan[i+1].GetVrijemePrikazivanja();

                DateTime emisijaTrajeDo = new DateTime();
                emisijaTrajeDo = vrijemePocetka.AddMinutes(trajanje);
                if (emisijaTrajeDo.TimeOfDay <= calcNextItemPocetak.TimeOfDay)
                {
                    vratiDatum = vrijemePocetka;
                    return vratiDatum;
                }
            }
            return vratiDatum;
        }

        private static DateTime DohvatiVrijemeEmisijuMoguceUbacitiPrijePrveEmisije(
            IComponentRaspored dan, int trajanje, TvProgram program)
        {
            DateTime vratiDatum = DateTime.MinValue;
            List<IComponentRaspored> sortiranDan = dan.GetChildList().OrderBy
                (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>();

            DateTime pocetakPrveEmisije = new DateTime();
            pocetakPrveEmisije = sortiranDan[0].GetVrijemePrikazivanja();

            DateTime pocetakPrograma = new DateTime();
            pocetakPrograma = program.GetPocetakPrikazivanja();

            if(pocetakPrograma.AddMinutes(trajanje) <= pocetakPrveEmisije)
            {
                vratiDatum = pocetakPrograma;
                return vratiDatum;
            }
            return vratiDatum;
        }

        private static DateTime DohvatiVrijemeEmisijuMoguceUbacitiNakonZadnjeEmisije(
            IComponentRaspored dan, int trajanje, TvProgram program)
        {
            DateTime vratiDatum = DateTime.MinValue;
            List<IComponentRaspored> sortiranDan = dan.GetChildList().OrderBy
                (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>();

            DateTime krajZadnjeEmisije = new DateTime();
            krajZadnjeEmisije = sortiranDan[sortiranDan.Count-1].GetVrijemePrikazivanja()
                .AddMinutes(sortiranDan[sortiranDan.Count - 1].GetTrajanje());

            DateTime krajPrograma = new DateTime();
            krajPrograma = program.GetKrajPrikazivanja();

            if(krajPrograma == DateTime.MinValue)
            {
                if (!PrelaziPonoca(krajPrograma, trajanje))
                {
                    vratiDatum = krajZadnjeEmisije;
                    return vratiDatum;
                }
            }
            else
            {
                if (krajZadnjeEmisije.AddMinutes(trajanje) <= krajPrograma)
                {
                    vratiDatum = krajZadnjeEmisije;
                    return vratiDatum;
                }
            }
            return vratiDatum;
        }

        /// <summary>
        /// Ovo je DEPRICATED od 30-11
        /// Ovo mi ne pomaze, ovo ima referencu na listu OU prave
        /// </summary>
        /// <param name="prva"></param>
        /// <param name="druga"></param>
        private static void DodajAtributeDrugeEmisijePrvoj(Emisija prva, Emisija druga)
        {
            prva.SetId(druga.GetId());
            prva.SetNaziv(druga.GetNaziv());
            prva.SetVrsta(druga.GetVrsta());
            prva.SetTrajanje(druga.GetTrajanje());
            prva.SetListaOsobaUloga(druga.GetListaOsobaUloga());
        }
        /// <summary>
        /// Ovo je nova, klonira ko bog
        /// </summary>
        /// <param name="origigi">Originalna emisija</param>
        public static Emisija VratiNoviKopiraniObjekt(Emisija origigi)
        {
            Emisija nova = new Emisija();

            int id = origigi.GetId();
            string naziv = origigi.GetNaziv();
            int vrstaId = origigi.GetVrsta();
            int trajanje = origigi.GetTrajanje();
            List<KeyValuePair<int, int>> lista = new List<KeyValuePair<int, int>>();
            foreach (var item in origigi.GetListaOsobaUloga())
            {
                lista.Add(item);
            }

            nova.SetId(id);
            nova.SetNaziv(naziv);
            nova.SetVrsta(vrstaId);
            nova.SetTrajanje(trajanje);
            nova.SetListaOsobaUloga(lista);
            return nova;
        }

        private static void DodajNoveOsobeUlogeEmisije(Emisija emisija, VremenskiPlan plan)
        {
            if (plan.GetListaOsobaUloga().Count > 0)
            {
                foreach (var item in plan.GetListaOsobaUloga())
                {
                    if (!emisija.GetListaOsobaUloga().Contains(item))
                    {
                        emisija.GetListaOsobaUloga().Add(item);
                    }
                }
            }
        }

        private static bool PrelaziPonocaXkrajPrograma(Emisija emisija, TvProgram program)
        {
            DateTime datum;
            if (program.GetKrajPrikazivanja() == DateTime.MinValue)
            {
                datum = new DateTime(
                    emisija.GetVrijemePrikazivanja().Year, emisija.GetVrijemePrikazivanja().Month,
                    emisija.GetVrijemePrikazivanja().AddDays(1).Day, 0, 0, 0);
            }
            else
            {
                datum = new DateTime(
                    emisija.GetVrijemePrikazivanja().Year, emisija.GetVrijemePrikazivanja().Month,
                    emisija.GetVrijemePrikazivanja().AddDays(0).Day, 
                    program.GetKrajPrikazivanja().Hour, program.GetKrajPrikazivanja().Minute, 0);
            }
            TimeSpan timespan = datum - emisija.GetVrijemePrikazivanja()
                .AddMinutes(emisija.GetTrajanje());
            if (timespan.TotalMinutes < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool PrelaziPonoca(DateTime unosDateTime, int trajanje)
        {
            DateTime datum = new DateTime(
                unosDateTime.Year, unosDateTime.Month, unosDateTime.Day, 0, 0, 0);
            datum.AddDays(1);
            TimeSpan timespan = datum - unosDateTime.AddMinutes(trajanje);
            if (timespan.TotalMinutes < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void DodajEmisijuNaPocetakPrograma(TvProgram program, Emisija orig, 
            VremenskiPlan plan, int brojDana)
        {
            Emisija emisijaZaDodat = VratiNoviKopiraniObjekt(orig);
            DodajNoveOsobeUlogeEmisije(emisijaZaDodat, plan);
            emisijaZaDodat.SetVrijemePrikazivanja(program.GetPocetakPrikazivanja());
            DodjeliRedniBroj(emisijaZaDodat);
            program.GetTjedan()[(brojDana - 1)].AddChild(emisijaZaDodat);

            SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
            subject.AddObserver(emisijaZaDodat);
        }
        private static bool UspioDodatPrijePrve(TvProgram program, Emisija orig, 
            VremenskiPlan plan, int brojDana)
        {
            Emisija emisijaZaDodat = VratiNoviKopiraniObjekt(orig);
            DodajNoveOsobeUlogeEmisije(emisijaZaDodat, plan);
            emisijaZaDodat.SetVrijemePrikazivanja(
                DohvatiVrijemeEmisijuMoguceUbacitiPrijePrveEmisije(
                  program.GetTjedan()[brojDana - 1], emisijaZaDodat.GetTrajanje(), program));
            if (emisijaZaDodat.GetVrijemePrikazivanja().TimeOfDay !=
                DateTime.MinValue.TimeOfDay)
            {
                DodjeliRedniBroj(emisijaZaDodat);
                program.GetTjedan()[(brojDana - 1)].AddChild(emisijaZaDodat);
                SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
                subject.AddObserver(emisijaZaDodat);
                return true;
            }
            return false;
        }

        private static bool UspioDodatNakonZadnje(TvProgram program, Emisija orig, 
            VremenskiPlan plan, int brojDana)
        {
            Emisija emisijaZaDodat = VratiNoviKopiraniObjekt(orig);
            DodajNoveOsobeUlogeEmisije(emisijaZaDodat, plan);
            emisijaZaDodat.SetVrijemePrikazivanja(
                DohvatiVrijemeEmisijuMoguceUbacitiNakonZadnjeEmisije(
                  program.GetTjedan()[brojDana - 1], emisijaZaDodat.GetTrajanje(), program));
            if (emisijaZaDodat.GetVrijemePrikazivanja().TimeOfDay !=
               DateTime.MinValue.TimeOfDay)
            {
                DodjeliRedniBroj(emisijaZaDodat);
                program.GetTjedan()[(brojDana - 1)].AddChild(emisijaZaDodat);
                SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
                subject.AddObserver(emisijaZaDodat);
                return true;
            }
            return false;
        }

        private static bool UspioDodatiIzmeduDvije(TvProgram program, Emisija orig,
            VremenskiPlan plan, int brojDana)
        {
            Emisija emisijaZaDodat = VratiNoviKopiraniObjekt(orig);
            DodajNoveOsobeUlogeEmisije(emisijaZaDodat, plan);
            emisijaZaDodat.SetVrijemePrikazivanja(
                DohvatiVrijemeGdjeEmisijuMoguceUbacitiIzmeduDvijeEmisije(
                    program.GetTjedan()[brojDana - 1], emisijaZaDodat.GetTrajanje()));
            if (emisijaZaDodat.GetVrijemePrikazivanja().TimeOfDay !=
                DateTime.MinValue.TimeOfDay)
            {
                DodjeliRedniBroj(emisijaZaDodat);
                program.GetTjedan()[(brojDana - 1)].AddChild(emisijaZaDodat);
                SubjectOsobaUlogaSingleton subject = SubjectOsobaUlogaSingleton.GetInstance();
                subject.AddObserver(emisijaZaDodat);
                return true;
            }
            return false;
        }

        private static void DodajEmitiranjeSingnalaTVKucePocetak(TvProgram program)
        {
            List<DateTime> listaPocetakaPrvihEmisija = PronadiPocetkePrvihEmisija(program);
            for (int i = 0; i < program.GetTjedan().Count; i++)
            {
                Emisija emisijaZaDodat = new Emisija();
                emisijaZaDodat.SetId(1234);
                emisijaZaDodat.SetNaziv("SIGNAL S IDENTITETOM TV KUCE");
                emisijaZaDodat.SetListaOsobaUloga(new List<KeyValuePair<int, int>>());
                emisijaZaDodat.SetVrijemePrikazivanja(
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
                    );
                TimeSpan timespan = 
                    listaPocetakaPrvihEmisija[i] - emisijaZaDodat.GetVrijemePrikazivanja();
                emisijaZaDodat.SetTrajanje(int.Parse(timespan.TotalMinutes.ToString()));
                program.GetTjedan()[i].GetChildList().Add(emisijaZaDodat);
            }
        }

        private static List<DateTime> PronadiPocetkePrvihEmisija(TvProgram program)
        {
            List<DateTime> listaPocetakaPrvihEmisija = new List<DateTime>();
            for (int i = 0; i < program.GetTjedan().Count; i++)
            {
                var sortiranDan = program.GetTjedan()[i].GetChildList().OrderBy
                    (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>();
                if (sortiranDan.Count == 0)
                    listaPocetakaPrvihEmisija.Add(program.GetPocetakPrikazivanja());
                else
                    listaPocetakaPrvihEmisija.Add(sortiranDan[0].GetVrijemePrikazivanja());
            }
            return listaPocetakaPrvihEmisija;
        }
        private static void DodajEmitiranjeSingnalaTVKuceKraj(TvProgram program)
        {
            List<DateTime> listaKrajevaZadnjihEmisija = PronadiKrajeveZadnjihEmisija(program);
            for (int i = 0; i < program.GetTjedan().Count; i++)
            {
                Emisija emisijaZaDodat = new Emisija();
                emisijaZaDodat.SetId(1234);
                emisijaZaDodat.SetNaziv("SIGNAL S IDENTITETOM TV KUCE");
                emisijaZaDodat.SetListaOsobaUloga(new List<KeyValuePair<int, int>>());
                emisijaZaDodat.SetVrijemePrikazivanja(listaKrajevaZadnjihEmisija[i]);
                DateTime sutraPocetka = new DateTime(
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 0, 0, 0);
                TimeSpan timespan = sutraPocetka - emisijaZaDodat.GetVrijemePrikazivanja();
                emisijaZaDodat.SetTrajanje(int.Parse(timespan.TotalMinutes.ToString()));
                program.GetTjedan()[i].GetChildList().Add(emisijaZaDodat);
            }
        }
        private static List<DateTime> PronadiKrajeveZadnjihEmisija(TvProgram program)
        {
            List<DateTime> listaKrajevaZadnjihEmisija = new List<DateTime>();
            for (int i = 0; i < program.GetTjedan().Count; i++)
            {
                var sortiranDan = program.GetTjedan()[i].GetChildList().OrderBy
                    (o => o.GetVrijemePrikazivanja()).ToList<IComponentRaspored>();
                if (sortiranDan.Count == 0)
                    listaKrajevaZadnjihEmisija.Add(program.GetKrajPrikazivanja());
                else
                {
                    listaKrajevaZadnjihEmisija.Add(
                    sortiranDan[sortiranDan.Count - 1].GetVrijemePrikazivanja()
                    .AddMinutes(sortiranDan[sortiranDan.Count - 1].GetTrajanje()));
                }
            }
            return listaKrajevaZadnjihEmisija;
        }

        private static void SortirajSveEmisijeUDanu(TvProgram program)
        {
            foreach (var dan in program.GetTjedan())
            {
                dan.GetChildList().Sort((a, b) =>
                DateTime.Compare(a.GetVrijemePrikazivanja(), b.GetVrijemePrikazivanja()));
            }
        }
    }
}
