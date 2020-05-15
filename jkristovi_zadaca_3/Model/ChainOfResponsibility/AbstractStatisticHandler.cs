using jkristovi_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.Model.ChainOfResponsibility
{
    public abstract class AbstractStatisticHandler
    {
        public static int VRSTA = 1;
        public static int PROFIT = 2;

        protected int Opcija;
        protected AbstractStatisticHandler Next;

        public AbstractStatisticHandler SetNext(AbstractStatisticHandler handler)
        {
            Next = handler;
            return this;
        }

        public void DoWork(int opcija, IComponentRaspored component)
        {
            if(Opcija == opcija)
            {
                Calculate(component);              
            }
            if (Next != null)
            {
                Next.DoWork(opcija, component);
            }
        }

        public abstract void Calculate(IComponentRaspored component);

        protected static double DobijPostotak(int dio, int cjelina)
        {
            double omjer = (double)dio / cjelina;
            double postotak = omjer * 100;
            double izlaz = Math.Round(postotak, 2);
            return izlaz;
        }
    }
}
