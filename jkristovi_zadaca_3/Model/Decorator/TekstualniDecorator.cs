﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Decorator
{
    public class TekstualniDecorator : RedakDecorator
    {
        private string Podatak;
        public TekstualniDecorator(IRedakTablice redak)
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
            string podatak = " {" + IspisHelper.Brojac + ",-39}";
            IspisHelper.Brojac++;
            Podatak = podatak;
        }
    }
}
