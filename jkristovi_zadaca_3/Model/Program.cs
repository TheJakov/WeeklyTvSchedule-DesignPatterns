using System;
using System.Collections.Generic;

namespace jkristovi_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            DatotekeHelper.ProvjeraUlaznihParametara(args);

            DatotekeHelper.UcitajPodatkeDatotekeTvKuce(args);
            DatotekeHelper.UcitajPodatkeDatotekeOsoba(args);
            DatotekeHelper.UcitajPodatkeDatotekeUloga(args);
            DatotekeHelper.UcitajPodatkeDatotekeVrsteEmisija(args);
            DatotekeHelper.UcitajPodatkeDatotekeEmisija(args);
            DatotekeHelper.UcitajPodatkeDatotekaTvPrograma();

            TjedniPlanHelper.PopunjavanjeTjednogRasporeda();

            IspisHelper.PrikazGlavnogIzbornika();

            Console.ReadLine();
        }
    }
}

