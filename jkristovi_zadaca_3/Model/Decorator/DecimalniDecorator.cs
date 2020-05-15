using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Decorator
{
    public class DecimalniDecorator : RedakDecorator
    {
        private string Podatak;
        public DecimalniDecorator(IRedakTablice redak)
            : base(redak)
        {
        }

        public override string NapraviRedak()
        {
            DodajPodatak();
            return base.NapraviRedak() + Podatak;
        }

        public void DodajPodatak()
        {
            string podatak = " {" + IspisHelper.Brojac + ",10:F2}";
            IspisHelper.Brojac++;
            Podatak = podatak;
        }
    }
}
