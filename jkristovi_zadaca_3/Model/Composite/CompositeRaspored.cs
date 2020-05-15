using System;
using System.Collections.Generic;
using jkristovi_zadaca_3.Iterator;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Composite
{
    public class CompositeRaspored : IComponentRaspored, IAggregate,IAggregateVrsta
    {
        private List<IComponentRaspored> ChildList = new List<IComponentRaspored>();

        //atribut koji se koristi samo kod Tv Programa, u svrhu ispisa
        private string MojNaziv = "";
        public ICompositeIterator GetIterator()
        {
            return new EmisijeIterator(this);
        }

        public ICompositeIterator GetIterator(int idVrsta)
        {
            return new EmisijeVrstaIterator(this, idVrsta);
        }

        public void PrikaziPodatke()
        {
            for (ICompositeIterator iter = GetIterator(); iter.HasNext();)
            {
                IComponentRaspored item = (IComponentRaspored) iter.Next();
                item.PrikaziPodatke();
            }
        }

        public void PrikaziPodatkeVrsta(int vrsta)
        {
            for (ICompositeIterator iter = GetIterator(vrsta); iter.HasNext();)
            {
                IComponentRaspored item = (IComponentRaspored)iter.Next();
                item.PrikaziPodatkeVrsta(vrsta);
            }
        }

        public void AddChild(IComponentRaspored djete)
        {
            ChildList.Add(djete);
        }
        public List<IComponentRaspored> GetChildList()
        {
            return ChildList;
        }
        public int GetTrajanje()
        {
            return -999; //Ja nemam trajanje, ja sam tjedan/dan koji sadrzi djecu (dane/emisije)
        }
        public DateTime GetVrijemePrikazivanja()
        {
            //Ja nemam zadano vri.prikazivanja, ja sam tjedan/dan koji sadrzi djecu (dane/emisije)
            return DateTime.MinValue;
        }

        public int GetVrsta()
        {
            return -999; //Ja nemam vrstu, ja sam tjedan/dan koji sadrzi djecu (dane/emisije)
        }

        /// <summary>
        /// Metoda koja sluzi za ispis imena kod programa.
        /// Inace se ne koristi.
        /// </summary>
        public void SetMojNaziv(string naziv)
        {
            MojNaziv = naziv;
        }
        public string GetMojNaziv()
        {
            return MojNaziv;
        }

        /// <summary>
        /// Java ima koncept unutarnje klase i implicitno ima skriveno 'this$0' član koji pamti
        /// kojoj vanjskoj klasi ona pripada. Kod jezika C#, gdje je unuatarnja klasa kao i kod
        /// C++ jezika, taj efekt se mora eksplicitno ostvariti. :(
        /// Objašnjeno: https://devblogs.microsoft.com/oldnewthing/?p=30273
        /// </summary>
        private class EmisijeIterator : ICompositeIterator
        {
            CompositeRaspored c_;

            public EmisijeIterator(CompositeRaspored c)
            {
                c_ = c;
            }
            private int Index;

            public bool HasNext()
            {
                if (Index < c_.ChildList.Count)
                {
                    return true;
                }
                return false;
            }

            public Object Next()
            {
                if (this.HasNext())
                {
                    return c_.ChildList[Index++];
                }
                return null;
            }
        }

        private class EmisijeVrstaIterator : ICompositeIterator
        {
            CompositeRaspored c_;
            private int Index;
            private int VrstaId;

            public EmisijeVrstaIterator(CompositeRaspored c, int vrstaId)
            {
                c_ = c;
                VrstaId = vrstaId;
            }
            public bool HasNext()
            {
                if (Index < c_.ChildList.Count)
                {
                    return true;
                }
                return false;
            }

            public Object Next()
            {
                if (this.HasNext())
                {
                    return c_.ChildList[Index++];
                }
                return null;
            }
        }
    }
}
