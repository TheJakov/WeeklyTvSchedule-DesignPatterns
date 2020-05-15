using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Composite
{
    public interface IComponentRaspored
    {
        void PrikaziPodatke();
        void PrikaziPodatkeVrsta(int vrsta);
        int GetTrajanje();
        int GetVrsta();
        DateTime GetVrijemePrikazivanja();

        //novo
        void AddChild(IComponentRaspored djete);

        List<IComponentRaspored> GetChildList();

        string GetMojNaziv();

    }
}
