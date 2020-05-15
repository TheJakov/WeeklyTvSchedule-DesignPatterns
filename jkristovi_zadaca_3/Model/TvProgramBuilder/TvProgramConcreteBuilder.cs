using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3.TvProgramBuilder
{
    class TvProgramConcreteBuilder : ITvProgramBuilder
    {
        private TvProgram tvProgram;

        public TvProgramConcreteBuilder()
        {
            tvProgram = new TvProgram();
        }

        public TvProgram Build() {
            return tvProgram;
        }

        public ITvProgramBuilder SetId(int id)
        {
            tvProgram.SetId(id);
            return this;
        }

        public ITvProgramBuilder SetNaziv(string naziv)
        {
            tvProgram.SetNaziv(naziv);
            return this;
        }

        public ITvProgramBuilder SetPocetakPrikazivanja(DateTime pocetak)
        {
            tvProgram.SetPocetakPrikazivanja(pocetak);
            return this;
        }

        public ITvProgramBuilder SetKrajPrikazivanja(DateTime kraj)
        {
            tvProgram.SetKrajPrikazivanja(kraj);
            return this;
        }

        public ITvProgramBuilder SetNazivDatoteke(string nazivDatoteke)
        {
            tvProgram.SetNazivDatoteke(nazivDatoteke);
            return this;
        }

        public ITvProgramBuilder SetVremenskiPlan(List<VremenskiPlan> plan)
        {
            tvProgram.SetVremenskiPlan(plan);
            return this;
        }

        public ITvProgramBuilder SetTjedan()
        {
            tvProgram.SetTjedan();
            return this;
        }

    }
}
