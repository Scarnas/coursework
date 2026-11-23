using System;
using System.IO;

namespace paskutinis
{


    class Gatve
    {
        private string gatve;
        private int juostusk;
        private int ilgis;
        private int saligsk;

        public Gatve()
        {
            
        }

        public Gatve(string gatve, int juostusk, int ilgis, int saligsk)
        {
            this.gatve = gatve;
            this.juostusk = juostusk;
            this.ilgis = ilgis;
            this.saligsk = saligsk;
        }


        public string ImtiGatve()
        {
            return gatve;
        }

        
        public int ImtiJuostuSk()
        {
            return juostusk;
        }

        public int ImtiIlgi()
        {
            return ilgis;
        }
        
        public int ImtiSaligSk()
        {
            return saligsk;
        }
    }
    

internal class Program
    {

        static void Ivesti(string fv, Gatve[] Gatves, out int kiek)
        {
            
            string gatve;
            int juostusk;
            int ilgis;
            int saligsk;
            

            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                string[] parts;
                kiek = 0;
                line = reader.ReadLine();

                while (line != null)
                {
                    parts = line.Split(';');
                    gatve = parts[0];
                    juostusk = int.Parse(parts[1]);
                    ilgis = int.Parse(parts[2]);
                    saligsk = int.Parse(parts[3]);
                    Gatves[kiek] = new Gatve(gatve, juostusk, ilgis, saligsk);
                    kiek++;
                    line = reader.ReadLine();

                }


            }


        }


        static void Spausdinti(Gatve[] Gatves, int kiek, string koment)
        {
            Console.WriteLine("Komentaras: {0}", koment);

            const string virsus = "-------------------------------------\r\n"
                                + " |Gatve   |JuostuSk | Ilgis | SaligSk\r\n"
                                + "-------------------------------------";

            Console.WriteLine(virsus);
            
            for (int i = 0; i < kiek; i++)
            {
               
                Console.WriteLine("{0,1} {1,5}, {2,5:d}, {3,7:f2}", Gatves[i].ImtiGatve(), Gatves[i].ImtiJuostuSk(), Gatves[i].ImtiIlgi(), Gatves[i].ImtiSaligSk());
            }

            Console.WriteLine("-------------------------------------");
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



           
            {
                
            }

        }

        static void Formuoti(Gatve[] Gatves, int kiek, int ss, Gatve[] GatvesA,
         out int kiekA)
            {
            kiekA = 0;
            for (int i = 0; i < kiek; i++)
            {
                if (Gatves[i].ImtiSaligSk() == ss)
                {
                    GatvesA[kiekA] = new Gatve(Gatves[i].ImtiGatve(), Gatves[i].ImtiJuostuSk(), Gatves[i].ImtiIlgi(), Gatves[i].ImtiSaligSk());
                    kiekA++;
                }
            }
        }
        
        static double GatviuVidutinisIlgis(Gatve[] Gatves, int kiek, int juostusk)
        {
           int suma = 0;
            double kiekis = 0;

            for (int i = 0; i < kiek; i++)
            {
                if (Gatves[i].ImtiJuostuSk() == juostusk)
                {
                    suma += Gatves[i].ImtiIlgi();
                    kiekis++;
                }
                if (kiekis == 0)
                {
                    return 0;
                }
            }
            return suma / kiekis;
        }
        static void Main(string[] args)
        {
            int n;
            int n1;
            const string Cfd = "Kaunas.txt";
            const int Cn = 100;
            Gatve[] gatve = new Gatve[Cn];
            Gatve[] gatve1 = new Gatve[Cn];
            Ivesti(Cfd, gatve, out n);
            int ilggat = IlgiausiaGatve(gatve, n); 
            Console.WriteLine("Ilgiausia gatve yra {0}, pavadinimas: {1} ", gatve[ilggat].ImtiIlgi(), gatve[ilggat].ImtiGatve());
            Formuoti(gatve,n,0,gatve1,out n1);
            
                
            
            

            
    }

 }



}