using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Decorator
{
    public abstract class RedakDecorator : IRedakTablice
    {
        protected IRedakTablice Redak;
        protected int Index = 0;
        public RedakDecorator(IRedakTablice redak)
        {
            Redak = redak;
        }

        public virtual string NapraviRedak()
        {
            return Redak.NapraviRedak();
        }
    }
}
