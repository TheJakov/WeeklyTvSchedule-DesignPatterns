using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Visitor
{
    public interface IEmisijaVisitor
    {
        int Visit(Emisija emisija);
    }
}
