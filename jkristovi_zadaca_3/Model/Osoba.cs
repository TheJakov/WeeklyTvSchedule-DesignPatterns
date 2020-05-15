using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    public class Osoba
    {
        private int Id;
        private string ImePrezime;

        public void SetId(int id)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public void SetImePrezime(string ime_prezime)
        {
            ImePrezime = ime_prezime;
        }
        public string GetImePrezime()
        {
            return ImePrezime;
        }
    }
}
