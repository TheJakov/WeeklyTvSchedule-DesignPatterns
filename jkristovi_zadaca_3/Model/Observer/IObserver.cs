using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Observer
{
    public interface IObserver
    {
        void Update(ISubjectUloga subjectObject);
    }
}
