using jkristovi_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Model.BrisanjePohranjivanje
{
    public class PohranjenoStanje
    {
        private int RedniBroj;
        private DateTime VrijemePohrane;
        private string Opis;

        private CompositeRaspored PocetniCvor;

        public void SetRedniBroj(int broj) 
        {
            RedniBroj = broj;
        }
        public int GetRedniBroj()
        {
            return RedniBroj;
        }

        public void SetVrijemePohrane(DateTime vrijeme)
        {
            VrijemePohrane = vrijeme;
        }
        public DateTime GetVrijemePohrane()
        {
            return VrijemePohrane;
        }

        public void SetOpis(string opis)
        {
            Opis = opis;
        }
        public string GetOpis()
        {
            return Opis;
        }

        public void SetPocetniCvor(CompositeRaspored dolazniCvor)
        {
            PocetniCvor = new CompositeRaspored();
            int indeksPrograma = 0;
            foreach (var program in dolazniCvor.GetChildList())
            {
                CompositeRaspored noviProgram = new CompositeRaspored();
                noviProgram.SetMojNaziv(program.GetMojNaziv());
                PocetniCvor.AddChild(noviProgram);
                int indeksDana = 0;
                foreach (var dan in program.GetChildList())
                {
                    CompositeRaspored noviDan = new CompositeRaspored();
                    PocetniCvor.GetChildList()[indeksPrograma].AddChild(noviDan);
                    foreach (var emisija in dan.GetChildList())
                    {
                        Emisija em = emisija as Emisija;
                        Emisija nova = TjedniPlanHelper.VratiNoviKopiraniObjekt(em);
                        nova.SetRedniBroj(em.GetRedniBroj());
                        nova.SetVrijemePrikazivanja(em.GetVrijemePrikazivanja());
                        PocetniCvor.GetChildList()[indeksPrograma]
                            .GetChildList()[indeksDana].AddChild(nova);
                    }
                    indeksDana++;
                }
                indeksPrograma++;
            }
        }
        public CompositeRaspored GetPocetniCvor()
        {
            return PocetniCvor;
        }
    }
}
