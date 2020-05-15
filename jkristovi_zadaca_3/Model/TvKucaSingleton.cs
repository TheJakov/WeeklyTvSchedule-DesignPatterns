using jkristovi_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    public class TvKucaSingleton
    {
        private static volatile TvKucaSingleton tvKuca = new TvKucaSingleton();

        private TvKucaSingleton() { }

        public static TvKucaSingleton GetTvKucaInstance()
        {
            return tvKuca;
        }

        public List<TvProgram> ListaTvPrograma;

        public List<Osoba> ListaOsoba;

        public List<Uloga> ListaUloga;

        public List<VrstaEmisije> ListaVrstaEmisija;

        public List<Emisija> ListaEmisija;

        private string MyFolderLocation;

        public CompositeRaspored Raspored;

        public void SetMyFolderLocation(string location)
        {
            MyFolderLocation = location;
        }
        public string GetMyFolderLocation()
        {
            return MyFolderLocation;
        }

        public void SetCompositeRaspored()
        {
            if (Raspored == null)
            {
                Raspored = new CompositeRaspored();
                foreach (var program in ListaTvPrograma)
                {
                    program.GetTjedanComposite().SetMojNaziv(program.GetNaziv());
                    Raspored.AddChild(program.GetTjedanComposite());
                }
            }
        }
        public CompositeRaspored GetCompositeRaspored()
        {
            return Raspored;
        }
    }
}
