using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Observer
{
    public interface ISubjectUloga
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);

        KeyValuePair<int,int> GetOsobaUlogaNovo();
        void SetOsobaUlogaNovo(KeyValuePair<int,int> par);
        KeyValuePair<int, int> GetOsobaUlogaStaro();
        void SetOsobaUlogaStaro(KeyValuePair<int, int> par);

        void NotifyObservers();
    }
}
