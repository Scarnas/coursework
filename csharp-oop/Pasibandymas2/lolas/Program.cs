using System;
using System.IO;

namespace lolas
{



    class Gatve
    {
        readonly string gatve;
        readonly int ilgis;
        readonly int saligsk;

        private int juostusk;



        public Gatve()
        {
        
        }

        public Gatve(string gatve, int ilgis,int juostusk, int saligsk)
        {
            this.gatve = gatve;
            this.ilgis = ilgis;
            this.saligsk = saligsk;
            this.juostusk = juostusk;
        }



        public string ImtiGatve()
        {
            return gatve;
        }

        public int ImtiIlgi()
        {
            return ilgis;
        }

        public int ImtiSaligSk()
        {
            return saligsk;
        }
        public int ImtiJuostuSk()
        {
            return juostusk;
        }






    }
    
internal class Program
    {


        static void Ivesti(string fv, Gatve[] Gatves, out int kiek)
        {
            string gatve;
            int ilgis;
            int saligsk;
            int juostusk;

            using (StreamReader reader = new StreamReader(fv)) 
            {
            kiek = 0;
            string line;
            string[] parts;
            line = reader.ReadLine();

                while (line != null)
                {
                    
                   
                    
                        parts = line.Split(';');
                    gatve = parts[0];
                    juostusk = int.Parse(parts[1]);
                        ilgis = int.Parse(parts[2]);
                        saligsk = int.Parse(parts[3]);
                        Gatves[kiek] = new Gatve(gatve, ilgis, juostusk, saligsk);
                        kiek++;
                        line = reader.ReadLine();

                    

                }
            }


        }


        static void Spausdinti(Gatve[] Gatves, int kiek, string koment)
        {
            
            Console.WriteLine("Komentaras: {0}", koment);

            const string virsus = "-------------------------------------------\r\n"
                                + "|Gatve.  | Ilgis | Juostu skaicius |  SaligatviuSkaicius.     |\r\n"
                                + "---------------------------------------------\r\n";
                                
            Gatve tarp;
            Console.WriteLine(virsus);
            for (int i = 0; i < kiek; i++)
            {
                tarp = Gatves[i];
                Console.WriteLine("{0,8}. |  {1,12}. | {2,5:d} | ", tarp.ImtiGatve(), tarp.ImtiIlgi(), tarp.ImtiJuostuSk(), tarp.ImtiSaligSk());
               Console.WriteLine("------------------------------"); 

            }



        }

        static int IlgiausiaGatve(Gatve[] Gatves, int kiek)
        {
            int naujasIndex = 0;

            for (int i = 0; i < kiek; i++)
            {
                if (Gatves[i].ImtiIlgi() > Gatves[naujasIndex].ImtiIlgi())
                {
                    naujasIndex = i;
                }
            }
            return naujasIndex;
        }

        static void Formuoti(Gatve[] Gatves, int kiek, int ss, Gatve[] GatvesA, out int kiekA)
        {
            kiekA = 0;
            for (int i = 0; i < kiek; i++)
            {
                if (Gatves[i].ImtiSaligSk() == ss )
                {
                    GatvesA[kiekA] = Gatves[i];
                    kiekA++;


                }
             
            }
        }

        static double GatviuVidutinisIlgis(Gatve[] Gatves, int kiek, int juostusk)
        {
            double suma = 0;
            int kiekis = 0;
            for (int i = 0; i < kiek; i++)
            {
                if (Gatves[i].ImtiJuostuSk() == juostusk)
                {
                    suma = suma + Gatves[i].ImtiIlgi();
                    kiekis++;
                }
                if (kiekis == 0)
                {   
                    return 0;
                }

            }
            return suma/kiekis;
        }






        
        static void Main(string[] args)
        {

            int n;
            int n1;
            const string Cfd = "Kaunas.txt";
            const int Cn = 100;
            Gatve[] gatve1 = new Gatve[Cn];
            Gatve[] gatve = new Gatve[Cn];
            Ivesti(Cfd, gatve, out n);
            Console.WriteLine("yra {0} lines faile", n);
            Spausdinti(gatve, n, "kakaliukas");
            IlgiausiaGatve(gatve, n);
            Formuoti(gatve,n,0,gatve1, out n1);

        }
}


}
