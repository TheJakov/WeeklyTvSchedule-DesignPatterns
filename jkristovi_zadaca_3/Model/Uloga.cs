using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jkristovi_zadaca_3
{
    public class Uloga
    {
        private int Id;
        private string Opis;

        public void SetId(int id)
        {
            Id = id;
        }
        public int GetId()
        {
            return Id;
        }

        public void SetOpis(string opis)
        {
            Opis = opis;
        }
        public string GetOpis()
        {
            return Opis;
        }
    }
}
