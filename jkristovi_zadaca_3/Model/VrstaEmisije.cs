using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    public class VrstaEmisije
    {
        private int Id;
        private string Naziv;
        private bool MozeImatReklame;
        private int MaxTrajReklama;

        public void SetId(int id)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public void SetNaziv(string naziv)
        {
            Naziv = naziv;
        }
        public string GetNaziv()
        {
            return Naziv;
        }

        public void SetMozeImatReklame(bool vrijednost)
        {
            MozeImatReklame = vrijednost;
        }
        public bool GetMozeImatReklame()
        {
            return MozeImatReklame;
        }

        public void SetMaxTrajanjeReklama(int trajanje)
        {
            MaxTrajReklama = trajanje;
        }
        public int GetMaxTrajanjeReklama()
        {
            return MaxTrajReklama;
        }
    }
}
