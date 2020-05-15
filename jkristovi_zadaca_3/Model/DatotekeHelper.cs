using System;
using System.Collections.Generic;
using System.Globalization;
using jkristovi_zadaca_3.TvProgramBuilder;
using jkristovi_zadaca_3.EmisijaBuilder;
using System.Text.RegularExpressions;

namespace jkristovi_zadaca_3
{
    static class DatotekeHelper
    {
        private static string timeFormat = "HH:mm";
        private static char LINE_SPLIT = '\n';
        private static char ATTR_SPLIT = ';';
        private static string ARG_REGEX = @"^(([a-zA-Z\d\s-_#\\:.!]{1,250})(\.txt))$";

        public static void ProvjeraUlaznihParametara(string[] args)
        {
            if (ProvjeriUlazneParametreNovo(args, ARG_REGEX))
            {
                Console.WriteLine("\nUlazni parametri su prosli validaciju. KREĆEM S RADOM.");
            }
            else
            {
                Console.WriteLine("\nUlazni parametri nisu zadani u ispravnom obliku." +
                    "\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekeTvKuce(string[] args)
        {
            string putanja = SaznajPutanjuDatTvKuce(args);
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke TV kuće.");
            string tvKucaDatotekaText = "";
            try
            {
                tvKucaDatotekaText = System.IO.File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke TV kuće.\nProvjerite da li se "+
                    "datoteka nalazi na navedenoj putanji ("+putanja+").\nIzlazak iz programa.");
                Console.ReadLine();//pause
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke TV kuće." +
                "\nZapočinjem učitavanje podataka datoteke TV kuće.");
            string[] nizRedakaTvKuca = PrepoznajRetkeIzDatoteke(tvKucaDatotekaText, LINE_SPLIT);
            TVKuca.ListaTvPrograma = DohvatiIspravneTvPrograme(nizRedakaTvKuca, ATTR_SPLIT);
            TVKuca.SetMyFolderLocation(DohvatiLokacijuMjestaDatoteke(putanja));
            if (TVKuca.ListaTvPrograma.Count > 0)
            {
                Console.WriteLine("Završeno učitavanje podataka datoteke TV kuće. " +
                    "(Ispravnih zapisa: " + TVKuca.ListaTvPrograma.Count + ")");
            }
            else
            {
                Console.WriteLine("Datoteka TV kuće ne sadrži niti jedan ispravan zapis " +
                    "za TV program.\nGasim program.");
                Console.ReadLine(); //pause
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekeOsoba(string[] args)
        {
            string putanja = SaznajPutanjuDatOsoba(args);
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke osoba.");
            string osobeDatotekaText = "";
            try
            {
                osobeDatotekaText = System.IO.File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke osoba.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji ("+putanja+").\nIzlazak iz programa.");
                Console.ReadLine();//pause
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke osoba." +
                "\nZapočinjem učitavanje podataka datoteke osoba.");
            string[] nizRedakaOsobe = PrepoznajRetkeIzDatoteke(osobeDatotekaText, LINE_SPLIT);
            TVKuca.ListaOsoba = DohvatiIspravneOsobe(nizRedakaOsobe, ATTR_SPLIT);
            if (TVKuca.ListaOsoba.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke osoba. " +
                    "(Ispravnih zapisa: " + TVKuca.ListaOsoba.Count + ")");
            else
            {
                Console.WriteLine("Datoteka osoba ne sadrži niti jedan ispravan zapis za osobu."+
                    "\nGasim program.");
                Console.ReadLine(); //pause
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekeUloga(string[] args)
        {
            string putanja = SaznajPutanjuDatUloga(args);
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke uloga.");
            string ulogeDatotekaText = "";
            try
            {
                ulogeDatotekaText = System.IO.File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke uloga.\nProvjerite da li se "+
                    "datoteka nalazi na navedenoj putanji ("+putanja+").\nIzlazak iz programa.");
                Console.ReadLine();//pause
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke uloga.\n" +
                "Započinjem učitavanje podataka datoteke uloga.");
            string[] nizRedakaUloge = PrepoznajRetkeIzDatoteke(ulogeDatotekaText, LINE_SPLIT);
            TVKuca.ListaUloga = DohvatiIspravneUloge(nizRedakaUloge, ATTR_SPLIT);
            if (TVKuca.ListaUloga.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke uloga. " +
                    "(Ispravnih zapisa: " + TVKuca.ListaUloga.Count + ")");
            else
            {
                Console.WriteLine("Datoteka uloga ne sadrži niti jedan ispravan zapis za ulogu."+  
                    "\nGasim program.");
                Console.ReadLine(); //pause
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekeVrsteEmisija(string[] args)
        {
            string putanja = SaznajPutanjuDatVrsteEmisija(args);
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke vrste emisija.");
            string vrsteEmisijaDatotekaText = "";
            try
            {
                vrsteEmisijaDatotekaText = System.IO.File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke vrsta emisija.\nProvjerite "+
                "nalazi li se datoteka na navedenoj putanji ("+putanja+").\nIzlazak iz programa.");
                Console.ReadLine();//pause
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke vrste emisija.\n" +
                "Započinjem učitavanje podataka datoteke vrste emisija.");
            string[] nizRedakaVrste=PrepoznajRetkeIzDatoteke(vrsteEmisijaDatotekaText,LINE_SPLIT);
            TVKuca.ListaVrstaEmisija=DohvatiIspravneVrsteEmisija(nizRedakaVrste, ATTR_SPLIT);
            if (TVKuca.ListaVrstaEmisija.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke vrsta emisija. " +
                    "(Ispravnih zapisa: " + TVKuca.ListaVrstaEmisija.Count + ")");
            else
            {
                Console.WriteLine("Datoteka vrsta emisija ne sadrži niti jedan ispravan " +
                    "zapis za vrstu emisije.\nGasim program.");
                Console.ReadLine(); //pause
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekeEmisija(string[]args)
        {
            string putanja = SaznajPutanjuDatEmisija(args);
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke emisija.");
            string emisijeDatotekaText = "";
            try
            {
                emisijeDatotekaText = System.IO.File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke emisija.\nProvjerite da li se "+
                    "datoteka nalazi na navedenoj putanji ("+putanja+").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke emisija.\n" +
                "Započinjem učitavanje podataka datoteke emisija.");
            string[] nizRedakaEmisije=PrepoznajRetkeIzDatoteke(emisijeDatotekaText, LINE_SPLIT);
            TVKuca.ListaEmisija = DohvatiIspravneEmisije(nizRedakaEmisije, ATTR_SPLIT);
            if (TVKuca.ListaEmisija.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke emisija. " +
                    "(Ispravnih zapisa: " + TVKuca.ListaEmisija.Count + ")");
            else
            {
                Console.WriteLine("Datoteka emisija ne sadrži niti jedan ispravan zapis " +
                    "za emisiju.\nGasim program.");
                Console.ReadLine(); //pause
                Environment.Exit(0);
            }
        }

        public static void UcitajPodatkeDatotekaTvPrograma()
        {
            TvKucaSingleton TVKuca = TvKucaSingleton.GetTvKucaInstance();
            Console.WriteLine("\nZapočinjem čitanje i učitavanje podataka datoteka " +
                "dostupnih TV programa.");
            int brojTvPrograma = TVKuca.ListaTvPrograma.Count;
            foreach (TvProgram program in TVKuca.ListaTvPrograma)
            {
                string tempDatotekaText = "";
                try
                {
                    tempDatotekaText = System.IO.File.ReadAllText(TVKuca.GetMyFolderLocation() + 
                        "\\" + program.GetNazivDatoteke());
                    string[] tempNizRedaka=PrepoznajRetkeIzDatoteke(tempDatotekaText, LINE_SPLIT);
                    program.SetVremenskiPlan(DohvatiVremenskiPlan(tempNizRedaka, ATTR_SPLIT));
                }
                catch
                {
                    brojTvPrograma--;
                    Console.WriteLine("Doslo je do pogreške kod čitanja datoteke \"{0}\" TV "+
                        "programa.\nProvjerite da li se datoteka nalazi u navedenom folderu {1}."+
                        " PRESKACEM.",program.GetNazivDatoteke(),TVKuca.GetMyFolderLocation());
                }
            }
            if (brojTvPrograma == 0)
            {
                Console.WriteLine("Ne postoji ispravna datoteka TV programa.\nGasim program.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje i učitavanje podataka datoteka dostupnih " +
                "TV programa.\n\nSummary:");
            foreach (TvProgram program in TVKuca.ListaTvPrograma) //Ispis summary
            {
                Console.WriteLine("Vremenski plan za program: " + program.GetNaziv() + 
                    ", sadrži sljedeći broj stavki: " + program.GetVremenskiPlan().Count);
            }
        }

        /// <summary>
        /// Metoda koja služi za razbijanje teksta datoteke u niz redaka
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća niz redaka teksta</returns>
        public static string[] PrepoznajRetkeIzDatoteke(string input, char splitter)
        {
            string[] izlaznaLista = input.Split(splitter);
            for (int i = 0; i < izlaznaLista.Length; i++)
            {
                izlaznaLista[i].Trim();
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda služi za filtriranje ispravnih zapisa TV programa iz niza redaka tv programa
        /// </summary>
        /// <param name="nizRedaka">Input niz</param>
        /// /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća listu ispravnih zapisa Tv Programa</returns>
        public static List<TvProgram> DohvatiIspravneTvPrograme(string[] nizRedaka, char splitter)
        {
            List<TvProgram> izlaznaLista = new List<TvProgram>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;
                else if (!IspravanTekstualniPodatak(privremeniObjekt[1]))
                    brojGresaka++;
                else if (!IspravanNazivDatotekeTvPrograma(privremeniObjekt[4]))
                    brojGresaka++;

                if (brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    izlaznaLista.Add(IzradiTvprogram(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja provjerava da li privremeni objekt sadrzi ispravan broj atributa
        /// </summary>
        /// <param name="inputObjekt">Objekt</param>
        /// <param name="broj">Ispravan broj atributa</param>
        /// <returns></returns>
        private static bool ProvjeriBrojAtributa(string[] inputObjekt, int broj)
        {
            if (inputObjekt.Length == broj)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda koja pretvara format vremena u 24-satni format, odnosno u HH:mm
        /// </summary>
        /// <param name="vrijeme">Ulazni parametar vremena</param>
        /// <returns>Vraća empty string ukoliko je whitespace, empty ili se 
        /// pojavljuje nedozvoljeni znak</returns>
        private static string VrijemeFormatirajPremaHHmm(string vrijeme)
        {
            string ulaznoVrijeme = vrijeme.Trim();
            if (ulaznoVrijeme.Length == 0)
            {
                return "";
            }
            else
            {
                char[] nizZnakova = ulaznoVrijeme.ToCharArray();
                bool pronadenUljez = false;
                for (int i = 0; i < nizZnakova.Length; i++) //provjera ispravnosti podatka vremena
                {
                    if (nizZnakova[i] == '0' || nizZnakova[i] == '1' || nizZnakova[i] == '2' || 
                        nizZnakova[i] == '3' || nizZnakova[i] == '4' || nizZnakova[i] == '5' || 
                        nizZnakova[i] == '6' || nizZnakova[i] == '7' || nizZnakova[i] == '8' || 
                        nizZnakova[i] == '9'
                        || nizZnakova[i] == ':') 
                    { 
                    }
                    else
                    {
                        pronadenUljez = true;
                    }
                } //kraj provjere ispravnosti
                if (pronadenUljez)
                {
                    return "";
                }
                else
                {   //formatiranje
                    string[] lista = ulaznoVrijeme.Split(':');
                    if (lista[0].Length == 1) //Ako je vrijeme u formatu H:mm
                    {
                        lista[0] = "0" + lista[0]; //Dodaj vodecu nulu satima, postaje HH:mm
                    }
                    string izlaznoVrijeme = lista[0] + ":" + lista[1];
                    return izlaznoVrijeme;
                }
            }
        }

        /// <summary>
        /// Metoda koja služi za filtriranje ispravnih zapisa o osobama iz niza redaka osoba
        /// </summary>
        /// <param name="nizRedaka">Input niz</param>
        /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća listu ispravnih zapisa osoba</returns>
        public static List<Osoba> DohvatiIspravneOsobe(string[] nizRedaka, char splitter)
        {
            List<Osoba> izlaznaLista = new List<Osoba>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;
                else if (!IspravanTekstualniPodatak(privremeniObjekt[1]))
                    brojGresaka++;

                if(brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    izlaznaLista.Add(IzradiOsobu(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za filtriranje ispravnih zapisa o osobama iz niza redaka osoba
        /// </summary>
        /// <param name="nizRedaka">Input niz</param>
        /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća listu ispravnih zapisa uloga</returns>
        public static List<Uloga> DohvatiIspravneUloge(string[] nizRedaka, char splitter)
        {
            List<Uloga> izlaznaLista = new List<Uloga>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;
                else if (!IspravanTekstualniPodatak(privremeniObjekt[1]))
                    brojGresaka++;

                if (brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    izlaznaLista.Add(IzradiUlogu(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za filtriranje ispravnih zapisa o emisijama iz niza redaka emisija
        /// </summary>
        /// <param name="nizRedaka">Input niz</param>
        /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća listu ispravnih zapisa emisija</returns>
        public static List<Emisija> DohvatiIspravneEmisije(string[] nizRedaka, char splitter)
        {
            List<Emisija> izlaznaLista = new List<Emisija>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;
                else if (!IspravanTekstualniPodatak(privremeniObjekt[1]))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[2])) //ID vrste
                    brojGresaka++;
                else if (!PostojiVrsta(privremeniObjekt[2]))
                    brojGresaka++;
                else if (!IspravnoTrajanjeEmisije(privremeniObjekt[3]))
                    brojGresaka++;

                if (brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    izlaznaLista.Add(IzradiEmisiju(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        private static bool PostojiVrsta(string idVrsta)
        {
            TvKucaSingleton tvKuca = TvKucaSingleton.GetTvKucaInstance();
            int intVrsta = int.Parse(idVrsta.Trim());
            VrstaEmisije vrsta = tvKuca.ListaVrstaEmisija.Find(x => x.GetId() == intVrsta);
            if (vrsta == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metoda služi za filtriranje ispravnih zapisa vremenskih planova pojedinog TV programa
        /// </summary>
        /// <param name="nizRedaka">Input niz</param>
        /// <param name="splitter">Splitter char</param>
        /// <returns>Vraća listu ispravnog vremenskog plana</returns>
        public static List<VremenskiPlan> DohvatiVremenskiPlan(string[] nizRedaka, char splitter)
        {
            List<VremenskiPlan> vremenskiPlanLista = new List<VremenskiPlan>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (privremeniObjekt.Length < 2)
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;

                if (brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    vremenskiPlanLista.Add(IzradiVremenskiPlan(privremeniObjekt));
                }
            }
            return vremenskiPlanLista;
        }

        public static List<VrstaEmisije> DohvatiIspravneVrsteEmisija(
            string[] nizRedaka, char splitter)
        {
            List<VrstaEmisije> vrsteEmisijeLista = new List<VrstaEmisije>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                    brojGresaka++;
                else if (!IspravanId(privremeniObjekt[0]))
                    brojGresaka++;
                else if (!IspravanTekstualniPodatak(privremeniObjekt[1]))
                    brojGresaka++;
                else if (!IspravanPodatakMozeImatReklame(privremeniObjekt[2]))
                    brojGresaka++;
                else if (!IspravnoMaxTrajanjeReklama(privremeniObjekt[3]))
                    brojGresaka++;

                if (brojGresaka != 0)
                    IspisHelper.IspisGreskeRedak(i, nizRedaka[i]);
                else
                {
                    vrsteEmisijeLista.Add(IzradiVrstuEmisije(privremeniObjekt));
                }
            }
            return vrsteEmisijeLista;
        }

        private static bool IspravanId(string podatak)
        {
            if (!int.TryParse(podatak.Trim(), out int tempId))
                return false;
            else
                return true;
        }

        private static bool IspravanTekstualniPodatak(string podatak)
        {
            if (podatak.Trim().Length == 0)
                return false;
            else
                return true;
        }

        private static bool IspravnoTrajanjeEmisije(string podatak)
        {
            if (!int.TryParse(podatak.Trim(), out int tempTrajanje) || (tempTrajanje <= 0))
                return false;
            else
                return true;
        }

        private static bool IspravanPodatakMozeImatReklame(string podatak)
        {
            if (!int.TryParse(podatak.Trim(), out int tempVrijednost))
                return false;
            else
            {
                if (tempVrijednost == 0 || tempVrijednost == 1)
                    return true;
                else
                    return false;
            }      
        }

        private static bool IspravnoMaxTrajanjeReklama(string podatak)
        {
            if (!int.TryParse(podatak.Trim(), out int tempVrijednost))
                return false;
            else
            {
                if (tempVrijednost < 0)
                    return false;
                else
                    return true;
            }
        }

        private static bool IspravanNazivDatotekeTvPrograma(string podatak)
        {
            if (podatak.Trim().Length == 0)
                return false;
            else if (!podatak.Trim().EndsWith(".txt"))
                return false;
            else if ((podatak.Trim().Length <= 4) && (podatak.Trim().EndsWith(".txt")))
                return false;
            else
                return true;
        }

        private static bool DohvatiBooelan(int vrijednost)
        {
            if (vrijednost == 0)
                return false;
            else
                return true;
        }

        private static VrstaEmisije IzradiVrstuEmisije(string[] redak)
        {
            VrstaEmisije tempVrsta = new VrstaEmisije();
            tempVrsta.SetId(int.Parse(redak[0].Trim()));
            tempVrsta.SetNaziv(redak[1].Trim());
            tempVrsta.SetMozeImatReklame(DohvatiBooelan(int.Parse(redak[2].Trim())));
            tempVrsta.SetMaxTrajanjeReklama(int.Parse(redak[3].Trim()));
            return tempVrsta;
        }

        private static Uloga IzradiUlogu(string[] redak)
        {
            Uloga tempUloga = new Uloga();
            tempUloga.SetId(int.Parse(redak[0].Trim()));
            tempUloga.SetOpis(redak[1].Trim());
            return tempUloga;
        }

        private static Osoba IzradiOsobu(string[] redak)
        {
            Osoba tempOsoba = new Osoba();
            tempOsoba.SetId(int.Parse(redak[0].Trim()));
            tempOsoba.SetImePrezime(redak[1].Trim());
            return tempOsoba;
        }

        private static TvProgram IzradiTvprogram(string[] redak)
        {
            int tempId = int.Parse(redak[0].Trim());
            string tempNaziv = redak[1].Trim();
            //FORMATIRANJE VREMENA
            string formatiranoPocetak = VrijemeFormatirajPremaHHmm(redak[2]);
            string formatiranoKraj = VrijemeFormatirajPremaHHmm(redak[3]);

            DateTime? helper1 = DateTime.TryParseExact(formatiranoPocetak, timeFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempPocetak)
                ? tempPocetak
                : (DateTime?)null;
            DateTime? helper2 = DateTime.TryParseExact(formatiranoKraj, timeFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempKraj)
                ? tempKraj
                : (DateTime?)null;
            string tempNazDat = redak[4].Trim();
            ITvProgramBuilder builder = new TvProgramConcreteBuilder();
            TvProgramBuildDirector director = new TvProgramBuildDirector(builder);
            return director.Construct(tempId, tempNaziv, tempPocetak, tempKraj, tempNazDat);

        }

        private static VremenskiPlan IzradiVremenskiPlan(string[] redak)
        {
            VremenskiPlan plan = new VremenskiPlan();
            plan.SetEmisijaId(int.Parse(redak[0].Trim()));

            List<int> tempListaDanaEmitiranja;
            if (redak.Length >= 2)
                tempListaDanaEmitiranja = DohvatiListuDanaEmitiranja(redak[1]);
            else
                tempListaDanaEmitiranja = new List<int>();

            DateTime tempPocetak;
            if (redak.Length >= 3)
                tempPocetak = DohvatiVrijemePocetka(redak[2]);
            else
                tempPocetak = DateTime.MinValue;

            List<KeyValuePair<int, int>> tempListaOsobaUloga;
            if (redak.Length >= 4)
                tempListaOsobaUloga = DohvatiListuOsobaUloga(redak[3]);
            else
                tempListaOsobaUloga = new List<KeyValuePair<int, int>>();

            plan.SetListaDanaEmitiranja(tempListaDanaEmitiranja);
            plan.SetPocetak(tempPocetak);
            plan.SetListaOsobaUloga(tempListaOsobaUloga);
            return plan;
        }

        private static Emisija IzradiEmisiju(string[] podatak)
        {
            int tempId = int.Parse(podatak[0].Trim());
            string tempNaziv = podatak[1].Trim();
            int tempVrsta = int.Parse(podatak[2].Trim());
            int tempTrajanje = int.Parse(podatak[3].Trim());
            List<KeyValuePair<int, int>> tempLista = IzradiListuOsobaUloga(podatak[4].Trim());
            IEmisijaBuilder builder = new EmisijaConcreteBuilder();
            EmisijaBuildDirector director = new EmisijaBuildDirector(builder);
            return director.Construct(tempId, tempNaziv, tempTrajanje, tempVrsta, tempLista);

        }

        private static List<KeyValuePair<int,int>> IzradiListuOsobaUloga(string zapis) 
        {
            List<KeyValuePair<int, int>> izlaznaLista = new List<KeyValuePair<int, int>>();
            if (zapis == "")
                return izlaznaLista;
            string[] tempObjektOsobaUloga = zapis.Split(',');
            if (tempObjektOsobaUloga.Length == 0)
                return izlaznaLista;
            for (int j = 0; j < tempObjektOsobaUloga.Length; j++)
            {
                if (tempObjektOsobaUloga[j].Trim() == "") { } //Ne postoji podatak
                else
                {
                    string[] osobaUloga = tempObjektOsobaUloga[j].Trim().Split('-');
                    if (osobaUloga.Length != 2) { } //Nije ispravno formatirano
                    else
                    {
                        if (!int.TryParse(osobaUloga[0].Trim(), out int tempIdOsoba)) { }
                        else
                        {
                            if (!int.TryParse(osobaUloga[1].Trim(), out int tempIdUloga)) { }
                            else
                            {
                                izlaznaLista.Add(
                                    new KeyValuePair<int, int>(tempIdOsoba, tempIdUloga)
                                );
                            }
                        }
                    }
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja dohvaca lokaciju mjesta(foldera) unutar kojega se nalazi datoteka
        /// </summary>
        /// <param name="lokacijaDatoteke">Putanja do datoteke (njena lokacija)</param>
        /// <returns>Vraća lokaciju (putanju) do mjesta (foldera) u kojem 
        /// se datoteka nalazi</returns>
        public static string DohvatiLokacijuMjestaDatoteke(string lokacijaDatoteke)
        {
            string lokacijaMjesta = new System.IO.FileInfo(lokacijaDatoteke).Directory.FullName;
            return lokacijaMjesta;
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ulaznih parametara
        /// </summary>
        /// <param name="args">Ulazni parametri</param>
        /// <returns>Vraća string ulaznih parametara</returns>
        public static string DohvatiUlazneParametre(string[] args)
        {
            string izlazniString = "";
            foreach (string item in args)
            {
                izlazniString += item + " ";
            }
            return izlazniString.Trim();
        }

        /// <summary>
        /// Metoda koja služi za provjeru ulaznih parametara
        /// </summary>
        /// <param name="args">Ulazni parametri</param>
        /// <param name="regex">Regex uzorak</param>
        /// <returns>Vraća true ukoliko parametri prolaze provjeru, 
        /// odnosno false ukoliko ne</returns>
        public static bool ProvjeriUlazneParametre(string[] args, string regex)
        {
            if (args.Length != 8)
            {
                return false;
            }
            else
            {
                Match matcher1 = Regex.Match(args[1], regex);
                Match matcher3 = Regex.Match(args[3], regex);
                Match matcher5 = Regex.Match(args[5], regex);
                Match matcher7 = Regex.Match(args[7], regex);

                if (!(args[0] == "-t") || !(args[2] == "-e") ||
                    !(args[4] == "-o") || !(args[6] == "-u"))
                {
                    return false;
                }
                else if (matcher1.Success && matcher3.Success &&
                    matcher5.Success && matcher7.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool ProvjeriUlazneParametreNovo(string[] args, string regex)
        {
            if (args.Length != 10 || !SadrziSvakuPotrebnuZastavicu(args))
                return false;
            else
            {
                if (!ParametriIspravnoRasporedeni(args))
                    return false;
                else
                {
                    Match matcher1 = Regex.Match(args[1], regex);
                    Match matcher3 = Regex.Match(args[3], regex);
                    Match matcher5 = Regex.Match(args[5], regex);
                    Match matcher7 = Regex.Match(args[7], regex);
                    Match matcher9 = Regex.Match(args[9], regex);

                    if (matcher1.Success && matcher3.Success &&
                    matcher5.Success && matcher7.Success && matcher9.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private static bool SadrziSvakuPotrebnuZastavicu(string[] args)
        {
            List<string> listaArgumenata = new List<string>();
            foreach (string arg in args)
            {
                listaArgumenata.Add(arg.Trim());
            }

            if (!listaArgumenata.Contains("-t"))
                return false;
            else if (!listaArgumenata.Contains("-e"))
                return false;
            else if (!listaArgumenata.Contains("-v"))
                return false;
            else if (!listaArgumenata.Contains("-o"))
                return false;
            else if (!listaArgumenata.Contains("-u"))
                return false;
            else
            {
                return true;
            }
        }

        private static bool ParametriIspravnoRasporedeni(string[] args)
        {
            if (!JeZastavica(args[0].Trim()))
                return false;
            else if (!JeZastavica(args[2].Trim()))
                return false;
            else if (!JeZastavica(args[4].Trim()))
                return false;
            else if (!JeZastavica(args[6].Trim()))
                return false;
            else if (!JeZastavica(args[8].Trim()))
                return false;
            else
                return true;
        }

        private static bool JeZastavica(string podatak)
        {
            if (podatak == "-t")
                return true;
            else if (podatak == "-e")
                return true;
            else if (podatak == "-v")
                return true;
            else if (podatak == "-o")
                return true;
            else if (podatak == "-u")
                return true;
            else
                return false;
        }

        private static string SaznajPutanjuDatTvKuce(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-t")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        private static string SaznajPutanjuDatOsoba(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-o")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        private static string SaznajPutanjuDatUloga(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-u")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        private static string SaznajPutanjuDatVrsteEmisija(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-v")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        private static string SaznajPutanjuDatEmisija(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-e")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        private static List<int> DohvatiListuDanaEmitiranja(string input)
        {
            List<int> tempList = new List<int>();
            Match matcher1 = Regex.Match(input.Trim(), @"^(([0-9]+[-]{1}[0-9]+))$");
            Match matcher2 = Regex.Match(input.Trim(), @"^([0-9]+){1}([,]{1}[0-9]+){0,}$");

            if (input.Trim() == "")
            {
                return tempList;
            }
            else if (matcher1.Success)
            {
                string[] tempPair = input.Split('-');
                int numOne = int.Parse(tempPair[0]);
                int numTwo = int.Parse(tempPair[1]);
                if (IsInRange(numOne, 1, 7) && IsInRange(numTwo, 1, 7) && (numOne != numTwo))
                {
                    int from = DohvatiManjeg(numOne, numTwo);
                    int to = DohvatiVeceg(numOne, numTwo);
                    for (int i = from; i <= to; i++)
                    {
                        tempList.Add(i);
                    }
                    return tempList;
                }
            }
            else if (matcher2.Success)
            {
                string[] tempRow = input.Split(',');
                List<int> tempIntList = new List<int>();
                for (int i = 0; i < tempRow.Length; i++)
                {
                    tempIntList.Add(int.Parse(tempRow[i]));
                }
                tempIntList.Sort();
                foreach (int broj in tempIntList)
                {
                    if (IsInRange(broj, 1, 7) && !tempList.Contains(broj))
                    {
                        tempList.Add(broj);
                    }
                }
                return tempList;
            }
            return tempList;
        }

        /// <summary>
        /// Metoda koja vraća true ili false, ovisno li broj pripada domeni
        /// </summary>
        /// <param name="number">Broj koji se provjerava</param>
        /// <param name="from">Broj od - inclusive</param>
        /// <param name="to">Broj do - inclusive</param>
        /// <returns>Broj je u domeni - true, broj je izvan - false</returns>
        private static bool IsInRange(int number, int from, int to)
        {
            if ((number >= from) && (number <= to))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda koja vraća manji broj od dva ponuđena.
        /// </summary>
        /// <param name="num1">Parametar broj 1</param>
        /// <param name="num2">Parametar broj 2</param>
        /// <returns>Integer</returns>
        private static int DohvatiManjeg(int num1, int num2)
        {
            if (num1 < num2)
                return num1;
            else
                return num2;
        }

        /// <summary>
        /// Metoda koja vraća veći broj od dva ponuđena.
        /// </summary>
        /// <param name="num1">Parametar broj 1</param>
        /// <param name="num2">Parametar broj 2</param>
        /// <returns>Integer</returns>
        private static int DohvatiVeceg(int num1, int num2)
        {
            if (num1 > num2)
                return num1;
            else
                return num2;
        }

        /// <summary>
        /// Metoda koja prima string zapis nekog vremena i vraca
        /// pripadajuci DateTime u HH:mm formatu
        /// </summary>
        /// <param name="input">Vrijeme u string formatu</param>
        /// <returns>DateTime vrijednost vremena</returns>
        private static DateTime DohvatiVrijemePocetka(string input)
        {
            string pocetak = input.Trim();
            //FORMATIRANJE VREMENA
            string formatiranoPocetak = VrijemeFormatirajPremaHHmm(pocetak);

            DateTime? timePocetak = DateTime.TryParseExact(formatiranoPocetak, timeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempPocetak)
                ? tempPocetak
                : (DateTime?)null;

            return tempPocetak;
        }

        /// <summary>
        /// Metoda koja dohvaca listu osoba,uloga iz stringa
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraca listu uosoba uloga ukoliko postoje ispravni zapisi, vraca praznu
        /// ukoliko niti jedan nije ispravan</returns>
        private static List<KeyValuePair<int, int>> DohvatiListuOsobaUloga(string podatak)
        {
            List<KeyValuePair<int, int>> tempLista = new List<KeyValuePair<int, int>>();
            if (podatak.Trim() == "") { }
            else
            {
                string[] privremeniObjektOsobaUloga = podatak.Trim().Split(',');
                if (privremeniObjektOsobaUloga.Length == 0) { }
                else if (privremeniObjektOsobaUloga.Length >= 1)
                {
                    for (int j = 0; j < privremeniObjektOsobaUloga.Length; j++)
                    {
                        if (privremeniObjektOsobaUloga[j].Trim() == "") { }
                        else
                        {
                            string[] osobaUloga=privremeniObjektOsobaUloga[j].Trim().Split('-');
                            if (osobaUloga.Length != 2) { }
                            else
                            {
                                if (!int.TryParse(osobaUloga[0].Trim(), out int tempIdOsoba)) { }
                                else
                                {
                                    if (!int.TryParse(osobaUloga[1].Trim(), out int tempIdUloga))
                                    {
                                    }
                                    else
                                    {
                                        tempLista.Add(new KeyValuePair<int, int>
                                            (tempIdOsoba, tempIdUloga));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tempLista;
        }
    }
}
