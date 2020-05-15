using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Iterator
{
    public interface IAggregate
    {
        ICompositeIterator GetIterator();
    }
}
