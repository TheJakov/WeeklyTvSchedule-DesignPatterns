using jkristovi_zadaca_3.Composite;
using System;
using System.Collections.Generic;

namespace jkristovi_zadaca_3
{
    public class TvProgram
    {
        private int Id;
        private string Naziv;
        private DateTime PocetakPrikazivanja;
        private DateTime KrajPrikazivanja;
        private string NazivDatoteke;

        private List<VremenskiPlan> VremenskiPlanLista;
        //private List<CompositeRaspored> Tjedan;
        private CompositeRaspored Tjedan;

        public TvProgram()
        {
        }

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

        public void SetPocetakPrikazivanja(DateTime time)
        {
            PocetakPrikazivanja = time;
        }
        public DateTime GetPocetakPrikazivanja()
        {
            return PocetakPrikazivanja;
        }
        
        public void SetKrajPrikazivanja(DateTime time)
        {
            KrajPrikazivanja = time;
        }
        public DateTime GetKrajPrikazivanja()
        {
            return KrajPrikazivanja;
        }

        public void SetNazivDatoteke(string nazivDatoteke)
        {
            NazivDatoteke = nazivDatoteke;
        }
        public string GetNazivDatoteke()
        {
            return NazivDatoteke;
        }

        public void SetVremenskiPlan(List<VremenskiPlan> plan)
        {
            VremenskiPlanLista = plan;
        }
        public List<VremenskiPlan> GetVremenskiPlan()
        {
            return VremenskiPlanLista;
        }

        public void SetTjedan()
        {
            Tjedan = new CompositeRaspored();

            CompositeRaspored Ponedeljak = new CompositeRaspored();
            CompositeRaspored Utorak = new CompositeRaspored();
            CompositeRaspored Srijeda = new CompositeRaspored();
            CompositeRaspored Cetvrtak = new CompositeRaspored();
            CompositeRaspored Petak = new CompositeRaspored();
            CompositeRaspored Subota = new CompositeRaspored();
            CompositeRaspored Nedelja = new CompositeRaspored();

            Tjedan.AddChild(Ponedeljak);
            Tjedan.AddChild(Utorak);
            Tjedan.AddChild(Srijeda);
            Tjedan.AddChild(Cetvrtak);
            Tjedan.AddChild(Petak);
            Tjedan.AddChild(Subota);
            Tjedan.AddChild(Nedelja);
            Tjedan.GetChildList();
        }
        public List<IComponentRaspored> GetTjedan()
        {
            return Tjedan.GetChildList();
        }
        public CompositeRaspored GetTjedanComposite()
        {
            return Tjedan;
        }
    }
}
