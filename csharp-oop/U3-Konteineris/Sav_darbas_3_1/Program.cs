// Savarankiško darbo 1 užduotis.
// Pakeitimai atlikti tik klasėje Studentas:
//   1) operatoriai > ir < lygina pagal įvertinimą, o esant vienodiems
//      įvertinimams - pagal pavardę, vardą abėcėliškai;
//   2) realizuotas metodas Equals, lyginantis pagal pavardę, vardą;
//   3) papildomi pataisymai, kad visi 4 lyginimai Equals veiktų teisingai
//      (atsakymai ir paaiškinimai - komentaruose klasėje Studentas).
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiškas_darbas_3_1
{
    //------------------------------------------------------------
    /** Klasė studento duomenims saugoti
        @class Studentas */
    //------------------------------------------------------------
    class Studentas
    {
        private string pavVrd;  // studento pavardė ir vardas
        private int pazym;      // pažymys (įvertinimas)

        /** Konstruktorius be parametrų */
        public Studentas()
        {

        }

        /** Konstruktorius: tik pavardė ir vardas, pažymys lygus 0
        @param pavv - pavardė ir vardas */
        public Studentas(string pavv)
        {
            pavVrd = pavv;
            this.pazym = 0;
        }

        /** Konstruktorius su visais duomenimis
        @param pavv  - pavardė ir vardas
        @param pazym - pažymys */
        public Studentas(string pavv, int pazym)
        {
            pavVrd = pavv;
            this.pazym = pazym;
        }

        /** Grąžina pavardę ir vardą */
        public string ImtiPavv() { return pavVrd; }

        /** Grąžina pažymį */
        public int ImtiPazym() { return pazym; }

        // Užklotas metodas ToString(): pavardė, vardas ir pažymys vienoje eilutėje
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -20}   {1, 2}", pavVrd, pazym);
            return eilute;
        }

     
        /** Lygina studentus pagal pavardę ir vardą
        @param stud - kitas studentas */
        public bool Equals(Studentas stud)
        {
            // Su null objektas niekada nelygus
            if (ReferenceEquals(stud, null))
                return false;
            // Pažymys nelyginamas - tik pavardė ir vardas
            return pavVrd == stud.pavVrd;
        }

        /** Užklotas Object.Equals - kviečiamas ir per object tipo nuorodas
        @param obj - kitas objektas */
        public override bool Equals(object obj)
        {
            // as grąžina null, jei obj nėra Studentas, todėl
            // klaidos nebus - Equals(Studentas) grąžins false
            return Equals(obj as Studentas);
        }

        // Užklotas metodas GetHashCode() - suderintas su Equals
        public override int GetHashCode()
        {
            // Jei pavardė ir vardas nenurodyti (null) - kodas 0
            return pavVrd == null ? 0 : pavVrd.GetHashCode();
        }

        /** Palygina pavardes ir vardus abėcėliškai (lietuviška abėcėlė)
        @return <0, jei a pirmiau už b; 0, jei lygūs; >0 - kitais atvejais */
        private static int PalygintiPavv(string a, string b)
        {
            // rikiuojamos pagal lietuvišką abėcėlę
            return String.Compare(a, b, new System.Globalization.CultureInfo("lt-LT"),
                                  System.Globalization.CompareOptions.None);
        }

        /** Grąžina <0, 0 arba >0: pirmiausia lygina įvertinimą,
        // esant vienodiems įvertinimams - pavardę, vardą abėcėliškai */
        private static int Palyginti(Studentas stud1, Studentas stud2)
        {
            // Skirtingi pažymiai - užtenka palyginti juos
            if (stud1.pazym != stud2.pazym)
                return stud1.pazym.CompareTo(stud2.pazym);
            // Vienodi pažymiai - lyginama pavardė, vardas
            return PalygintiPavv(stud1.pavVrd, stud2.pavVrd);
        }

        // Užklotas operatorius >
        // true, jei įvertinimas didesnis, arba įvertinimai lygūs,
        // o pavardė, vardas abėcėliškai yra toliau
        public static bool operator >(Studentas stud1, Studentas stud2)
        {
            // Visa lyginimo logika - metode Palyginti()
            return Palyginti(stud1, stud2) > 0;
        }

        // Užklotas operatorius <
        // true, jei įvertinimas mažesnis, arba įvertinimai lygūs,
        // o pavardė, vardas abėcėliškai yra pirmiau
        public static bool operator <(Studentas stud1, Studentas stud2)
        {
            // C# reikalauja, kad > ir < būtų užkloti poromis
            return Palyginti(stud1, stud2) < 0;
        }
    }

    //------------------------------------------------------------
    /** Pagrindinė klasė (pateikta užduotyje, nekeičiama):
    // tiria užklotų operatorių ir metodo Equals veikimą */
    //------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            // Trys studentai: stud1 ir stud2 - vienodi duomenys,
            // stud3 - ta pati pavardė ir vardas, bet kitas pažymys
            Studentas stud1 = new Studentas("Pavardenis Vardenis", 7);
            Studentas stud2 = new Studentas("Pavardenis Vardenis", 7);
            Studentas stud3 = new Studentas("Pavardenis Vardenis", 10);
            

            // I dalis
            // Operatoriaus > tyrimas: 7 < 10, todėl stud1 > stud3 yra false
            Console.WriteLine();
            Console.WriteLine("stud1:   " + stud1);
            Console.WriteLine("stud2:   " + stud2.ToString());
            Console.WriteLine("stud3:   " + stud3.ToString());
            Console.WriteLine();
            Console.WriteLine("Užkloto operatoriaus > panaudojimo tyrimas");
            Console.WriteLine();
            Console.WriteLine("Lyginami stud1 ir stud3 objektai:");
            if (stud1 > stud3)
                Console.WriteLine("Atsakymas: stud1 > stud3.");
            else
                Console.WriteLine("Atsakymas: stud1 < stud3 arba stud1 = stud3.");
            Console.WriteLine();
            Console.WriteLine();


            // Equals panaudojimo tyrimas
            // I dalis
            // Kintamųjų tipas object: kviečiamas virtualus Equals(object),
            // todėl reikalingas jo užklojimas (override)
            Console.WriteLine("Equals panaudojimo tyrimas");
            Console.WriteLine();
            // Tie patys objektai, tik nuorodos tipas - object
            object stud11 = stud1;
            object stud12 = stud2;
            Console.WriteLine("stud11:   " + stud11.ToString());
            Console.WriteLine("stud12:   " + stud12.ToString());
            Console.WriteLine("I lyginimas");
            Console.WriteLine("stud11.Equals(stud12), Tipai objects");
            if (stud11.Equals(stud12))
                Console.WriteLine("LYGU.");
            else
                Console.WriteLine("NELYGU.");
            Console.WriteLine();

            // II dalis
            // var reiškia tipą Studentas: kviečiamas Equals(Studentas)
            var stud31 = stud1;
            var stud32 = stud2;
            Console.WriteLine("stud31:   " + stud31.ToString());
            Console.WriteLine("stud32:   " + stud32.ToString());
            Console.WriteLine("II lyginimas");
            Console.WriteLine("stud31.Equals(stud32), Tipai su var");
            if (stud31.Equals(stud32))
                Console.WriteLine("LYGU1.");
            else
                Console.WriteLine("NELYGU1.");
            Console.WriteLine();

            // III dalis
            // Statinis Object.Equals(a, b): viduje kviečia a.Equals(object)
            Console.WriteLine("stud1:   " + stud1.ToString());
            Console.WriteLine("stud2:   " + stud1.ToString());
            Console.WriteLine("III lyginimas");
            Console.WriteLine("Equals (stud1, stud2), Tipai Studentas");
            if (Equals(stud1, stud2))
                Console.WriteLine("LYGU2.");
            else
                Console.WriteLine("NELYGU2.");
            Console.WriteLine();


            // IV dalis
            // Tipas Studentas: kviečiamas Equals(Studentas)
            Console.WriteLine("stud1:   " + stud1.ToString());
            Console.WriteLine("stud1:   " + stud1.ToString()); 
            Console.WriteLine("IV lyginimas");
            Console.WriteLine("stud1.Equals(stud2), Tipai Studentas");
            if (stud1.Equals(stud2))
                Console.WriteLine("LYGU3.");
            else
                Console.WriteLine("NELYGU3.");
            Console.WriteLine();
        }
    }
}

