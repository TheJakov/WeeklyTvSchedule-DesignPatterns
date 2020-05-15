using System;
using System.Collections.Generic;
using jkristovi_zadaca_3.Composite;
using jkristovi_zadaca_3.Decorator;
using jkristovi_zadaca_3.Observer;
using jkristovi_zadaca_3.Visitor;

namespace jkristovi_zadaca_3
{
    public class Emisija : IComponentRaspored, IObserver, IVisitorElement
    {
        private int Id;
        private string Naziv;
        private int Trajanje;  //Predstavlja trajanje emisije u minutama
        private int Vrsta;
        private List<KeyValuePair<int, int>> ListaOsobaUloga;
        private DateTime VrijemePrikazivanja;

        //novo od 3. zadace
        private int RedniBroj = -1;
        public Emisija() { }

        public void SetId(int id)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public void SetNaziv(string naziv)
        {
            Naziv = naziv;
        }
        public string GetNaziv()
        {
            return Naziv;
        }

        public void SetTrajanje(int trajanje)
        {
            Trajanje = trajanje;
        }
        public int GetTrajanje()
        {
            return Trajanje;
        }

        public void SetVrsta(int vrsta)
        {
            Vrsta = vrsta;
        }
        public int GetVrsta()
        {
            return Vrsta;
        }

        public void SetListaOsobaUloga(List<KeyValuePair<int, int>> lista)
        {
            ListaOsobaUloga = lista;
        }
        public List<KeyValuePair<int, int>> GetListaOsobaUloga()
        {
            return ListaOsobaUloga;
        }

        public void SetVrijemePrikazivanja(DateTime vrijeme)
        {
            VrijemePrikazivanja = vrijeme;
        }
        public DateTime GetVrijemePrikazivanja()
        {
            return VrijemePrikazivanja;
        }

        //novo od 3. zadace
        public void SetRedniBroj(int rbr)
        {
            RedniBroj = rbr;
        }
        public int GetRedniBroj()
        {
            return RedniBroj;
        }

        public void PrikaziPodatke()
        {
            string osobeUloge = IspisHelper.DohvatiOsobeUloge(ListaOsobaUloga);
            string nazivVrste = IspisHelper.DohvatiNazivVrsteEmisije(Vrsta);
            IspisHelper.Brojac = 0;
            IRedakTablice redakTablice =
             new KratkiTekstDecorator(
                new KratkiTekstDecorator(
                    new KratkiTekstDecorator(
                        new CjelobrojniDecorator(
                            new CjelobrojniDecorator(
                                new CjelobrojniDecorator(
                                    new TekstualniDecorator(
                                        new TekstualniDecorator(
                                            new TekstualniDecorator(
                                                new ConcreteRedak())))))))));
            string format = redakTablice.NapraviRedak();
            string ispis = String.Format(format, "\n"+new String('.', 176),
                VrijemePrikazivanja.AddMinutes(Trajanje).ToShortTimeString(),
                VrijemePrikazivanja.ToShortTimeString(),
                Trajanje, Id, RedniBroj+".", osobeUloge, nazivVrste, Naziv);
            Console.WriteLine(ispis);
        }

        public void PrikaziPodatkeVrsta(int vrsta)
        {
            if (Vrsta == vrsta)
            {
                string osobeUloge = IspisHelper.DohvatiOsobeUloge(ListaOsobaUloga);
                string nazivVrste = IspisHelper.DohvatiNazivVrsteEmisije(Vrsta);
                IspisHelper.Brojac = 0;
                IRedakTablice redakTablice =
                 new KratkiTekstDecorator(
                    new KratkiTekstDecorator(
                        new KratkiTekstDecorator(
                            new CjelobrojniDecorator(
                                new CjelobrojniDecorator(
                                    new CjelobrojniDecorator(
                                        new TekstualniDecorator(
                                            new TekstualniDecorator(
                                                new TekstualniDecorator(
                                                    new ConcreteRedak())))))))));
                string format = redakTablice.NapraviRedak();
                string ispis = String.Format(format, "\n" + new String('.', 176),
                    VrijemePrikazivanja.AddMinutes(Trajanje).ToShortTimeString(),
                    VrijemePrikazivanja.ToShortTimeString(),
                    Trajanje, Id, RedniBroj+".", osobeUloge, nazivVrste, Naziv);
                Console.WriteLine(ispis);
            }
        }

        private List<IComponentRaspored> ChildList = new List<IComponentRaspored>();
        public List<IComponentRaspored> GetChildList()
        {
            return ChildList;
        }
        public void AddChild(IComponentRaspored item)
        {
            //Emisije ne dodaje nista
        }

        public string GetMojNaziv()
        {
            return Naziv;
        }

        public void Update(ISubjectUloga subject)
        {
            if (ListaOsobaUloga.Contains(subject.GetOsobaUlogaStaro()))
            {
                ListaOsobaUloga.Remove(subject.GetOsobaUlogaStaro());
                ListaOsobaUloga.Add(subject.GetOsobaUlogaNovo());
            }
        }

        public int Accept(IEmisijaVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
