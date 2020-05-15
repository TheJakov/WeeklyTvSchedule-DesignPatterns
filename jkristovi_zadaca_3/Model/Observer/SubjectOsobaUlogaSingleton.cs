using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Observer
{
    public class SubjectOsobaUlogaSingleton : ISubjectUloga
    {
        private static volatile SubjectOsobaUlogaSingleton 
            subjectOU = new SubjectOsobaUlogaSingleton();

        private SubjectOsobaUlogaSingleton() { }

        public static SubjectOsobaUlogaSingleton GetInstance()
        {
            return subjectOU;
        }

        private KeyValuePair<int, int> osobaUlogaNovo = new KeyValuePair<int, int>(-1, -1);
        private KeyValuePair<int, int> osobaUlogaStaro = new KeyValuePair<int, int>(-1, -1);

        private List<IObserver> ListaObservera = new List<IObserver>();

        public KeyValuePair<int, int> GetOsobaUlogaNovo()
        {
            return osobaUlogaNovo;
        }
        public void SetOsobaUlogaNovo(KeyValuePair<int, int> par)
        {
            osobaUlogaNovo = par;
            NotifyObservers();
        }

        public KeyValuePair<int, int> GetOsobaUlogaStaro()
        {
            return osobaUlogaStaro;
        }
        public void SetOsobaUlogaStaro(KeyValuePair<int, int> par)
        {
            osobaUlogaStaro = par;
        }

        public void AddObserver(IObserver observer)
        {
            ListaObservera.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            ListaObservera.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in ListaObservera)
            {
                observer.Update(this);
            }
        }
    }
}
