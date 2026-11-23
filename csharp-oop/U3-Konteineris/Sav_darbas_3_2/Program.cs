// Savarankiško darbo 2 užduotis.
// Pateikiamas 9 aukštų namo, sudaryto iš laiptinių, parduodamų butų sąrašas.
// Kiekviename laiptinės aukšte yra po 3 butus. Žinomas buto numeris, bendras
// plotas, kambarių skaičius, pardavimo kaina, telefono Nr. Suraskite butus,
// kurie yra norimoje laiptinėje (laiptinė surandama pagal buto numerį), turi
// nurodytą kambarių skaičių ir kurių kaina neviršija nurodytos kainos ir juos
// surašykite į tinkamų butų konteinerį. Laiptinės numeris, kambarių skaičius,
// kaina įvedami iš konsolės.
//
// Duomenų failo Butai.txt eilutės formatas:
//   buto Nr.; plotas; kambarių skaičius; kaina; telefono Nr.
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sav_darbas_3_2
{
    //------------------------------------------------------------
    /** Klasė vieno buto duomenims saugoti
        @class Butas */
    //------------------------------------------------------------
    class Butas
    {
        public const int CAukštai = 9;          // aukštų skaičius name
        public const int CButaiAukšte = 3;      // butų skaičius laiptinės aukšte
        public const int CButaiLaiptinėje = CAukštai * CButaiAukšte;  // butų skaičius laiptinėje (27)

        private int nr;            // buto numeris
        private double plotas;     // bendras plotas, m2
        private int kambariai;     // kambarių skaičius
        private double kaina;      // pardavimo kaina, Eur
        private string telefonas;  // telefono numeris

        /** Konstruktorius be parametrų: visi laukai gauna
        // pradines (nulines) reikšmes */
        public Butas()
        {
            nr = 0;
            plotas = 0;
            kambariai = 0;
            kaina = 0;
            telefonas = "";
        }

        //------------------------------------------------------------
        /** Konstruktorius su parametrais
        @param nr        - buto numeris
        @param plotas    - bendras plotas
        @param kambariai - kambarių skaičius
        @param kaina     - pardavimo kaina
        @param telefonas - telefono numeris */
        //------------------------------------------------------------
        public Butas(int nr, double plotas, int kambariai, double kaina, string telefonas)
        {
            // this. reikalingas, nes parametrų vardai sutampa su laukų vardais
            this.nr = nr;
            this.plotas = plotas;
            this.kambariai = kambariai;
            this.kaina = kaina;
            this.telefonas = telefonas;
        }

        /** Grąžina buto numerį */
        public int ImtiNr() { return nr; }
        /** Grąžina kambarių skaičių */
        public int ImtiKambarius() { return kambariai; }
        /** Grąžina pardavimo kainą */
        public double ImtiKainą() { return kaina; }

        /** Laiptinės numeris, apskaičiuotas iš buto numerio
        // (1-27 butai - 1 laiptinė, 28-54 - 2 laiptinė ir t.t.).
        // Atimamas 1, kad numeracija prasidėtų nuo 0: tada sveikoji
        // dalyba iš 27 duoda laiptinės indeksą, o +1 - jos numerį.
        // Pvz. butas 30: (30 - 1) / 27 + 1 = 1 + 1 = 2 laiptinė */
        public int Laiptinė() { return (nr - 1) / CButaiLaiptinėje + 1; }

        /** Aukštas laiptinėje, apskaičiuotas iš buto numerio.
        // Liekana % 27 - buto vieta laiptinėje (0..26), ją dalijant iš 3
        // gaunamas aukšto indeksas (0..8), +1 - aukšto numeris.
        // Pvz. butas 30: (29 % 27) / 3 + 1 = 2 / 3 + 1 = 1 aukštas */
        public int Aukštas() { return (nr - 1) % CButaiLaiptinėje / CButaiAukšte + 1; }

        /** Ar butas tinka pirkėjui
        @param laiptinė  - norima laiptinė
        @param kambariai - norimas kambarių skaičius
        @param maxKaina  - didžiausia leistina kaina */
        public bool Tinka(int laiptinė, int kambariai, double maxKaina)
        {
            // Butas tinka tik tada, kai tenkinamos visos trys sąlygos.
            // this.kambariai - buto laukas, kambariai - parametras
            return Laiptinė() == laiptinė && this.kambariai == kambariai && kaina <= maxKaina;
        }

        // Spausdinimo metodas: buto duomenys vienoje lentelės eilutėje
        // (InvariantCulture - skaičiai visada su tašku)
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0,5:d} {1,9:d} {2,7:d} {3,9:f2} {4,10:d} {5,12:f2}   {6,-15}",
                nr, Laiptinė(), Aukštas(), plotas, kambariai, kaina, telefonas);
        }
    }

    //------------------------------------------------------------
    /** Konteinerinė klasė butų duomenims saugoti
        @class Butai */
    //------------------------------------------------------------
    class Butai
    {
        const int CMax = 100;      // maksimalus butų skaičius
        private Butas[] B;         // butų duomenys
        private int n;             // butų skaičius

        /** Konstruktorius: sukuria tuščią butų masyvą */
        public Butai()
        {
            n = 0;
            B = new Butas[CMax];
        }

        /** Grąžina butų skaičių */
        public int Imti() { return n; }

        /** Grąžina nurodyto indekso buto objektą
        @param i - buto indeksas */
        public Butas Imti(int i) { return B[i]; }

        /** Padeda naują butą į masyvą, jei neviršijamas masyvo dydis
        @param ob - buto objektas
        @return true, jei butas pridėtas */
        public bool Dėti(Butas ob)
        {
            // Masyvas pilnas - naujo buto dėti negalima
            if (n >= CMax)
                return false;
            // Įrašoma į pirmą laisvą vietą, po to n padidinamas vienetu
            B[n++] = ob;
            return true;
        }

        /** Ar konteineryje jau yra butas su tokiu numeriu
        @param nr - buto numeris */
        public bool YraNr(int nr)
        {
            // Nuoseklioji paieška: tikrinamas kiekvienas butas iš eilės
            for (int i = 0; i < n; i++)
                if (B[i].ImtiNr() == nr)
                    return true;
            return false;
        }

        /** Didžiausias laiptinės numeris tarp sąrašo butų
        // (naudojamas nustatyti, kokį laiptinės numerį galima įvesti) */
        public int DidžiausiaLaiptinė()
        {
            // Pradinė reikšmė 0, nes laiptinių numeriai prasideda nuo 1
            int max = 0;
            for (int i = 0; i < n; i++)
                if (B[i].Laiptinė() > max)
                    max = B[i].Laiptinė();
            return max;
        }
    }

    //------------------------------------------------------------
    /** Pagrindinė klasė: įvedimas, atranka, spausdinimas */
    //------------------------------------------------------------
    class Program
    {
        // Duomenų ir rezultatų failai - projekto aplanke (du lygiai virš bin/Debug)
        static readonly string CFd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Butai.txt");
        static readonly string CFr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Rezultatai.txt");

        static void Main(string[] args)
        {
            // Lietuviškos raidės ekrane ir įvedant klaviatūra (be BOM simbolių)
            Console.OutputEncoding = new UTF8Encoding(false);
            Console.InputEncoding = new UTF8Encoding(false);
            // Jei nėra duomenų failo - pranešama ir baigiama
            if (!File.Exists(CFd))
            {
                Console.WriteLine("Nerastas duomenų failas: {0}", Path.GetFullPath(CFd));
                return;
            }
            // Ištrinami ankstesnio paleidimo rezultatai, nes toliau
            // į failą tik pridedama (AppendText)
            if (File.Exists(CFr))
                File.Delete(CFr);

            // Pradinių duomenų skaitymas ir spausdinimas (faile ir ekrane;
            // null vietoj failo vardo reiškia spausdinimą ekrane)
            Butai visi = new Butai();
            Skaityti(visi, CFd);
            Spausdinti(visi, CFr, " Parduodamų butų sąrašas");
            Spausdinti(visi, null, " Parduodamų butų sąrašas");

            // Be butų tolimesnė atranka neturi prasmės
            if (visi.Imti() == 0)
            {
                Console.WriteLine("Duomenų faile nėra butų.");
                return;
            }

            // Pirkėjo reikalavimai įvedami klaviatūra; klausiama tol,
            // kol įvedama leistina reikšmė
            int laiptinė = SkaitytiSveikąjį(
                string.Format("Įveskite laiptinės numerį (1..{0}): ", visi.DidžiausiaLaiptinė()),
                1, visi.DidžiausiaLaiptinė());
            int kambariai = SkaitytiSveikąjį("Įveskite kambarių skaičių: ", 1, 100);
            double kaina = SkaitytiKainą("Įveskite didžiausią kainą, Eur: ");

            // Tinkamų butų atranka į naują konteinerį
            Butai tinkami = new Butai();
            Formuoti(visi, tinkami, laiptinė, kambariai, kaina);

            // Antraštėje parodomi pirkėjo reikalavimai
            string antraštė = string.Format(CultureInfo.InvariantCulture,
                " Tinkami butai: {0} laiptinė, {1} kamb., kaina iki {2:f2} Eur",
                laiptinė, kambariai, kaina);
            // Rezultatų spausdinimas (arba pranešimas, kad tinkamų nėra)
            if (tinkami.Imti() > 0)
            {
                Spausdinti(tinkami, CFr, antraštė);
                Spausdinti(tinkami, null, antraštė);
            }
            else
            {
                // Tinkamų butų nėra - pranešimas įrašomas į failą ir ekraną
                using (var fr = File.AppendText(CFr))
                {
                    fr.WriteLine(antraštė);
                    fr.WriteLine(" Tinkamų butų nėra");
                }
                Console.WriteLine(antraštė);
                Console.WriteLine(" Tinkamų butų nėra");
            }

            Console.WriteLine();
            Console.WriteLine("Rezultatai: {0}", Path.GetFullPath(CFr));
            Console.WriteLine("Programa baigė darbą!");
        }

        //------------------------------------------------------------
        /** Failo duomenis surašo į konteinerį. Klaidingos eilutės
        // praleidžiamos, apie jas pranešama ekrane.
        @param butai - butų konteineris
        @param fv    - duomenų failo vardas */
        //------------------------------------------------------------
        static void Skaityti(Butai butai, string fv)
        {
            // Visos failo eilutės nuskaitomos į masyvą iš karto
            string[] lines = File.ReadAllLines(fv, Encoding.UTF8);
            for (int k = 0; k < lines.Length; k++)
            {
                string line = lines[k];
                // Tuščios eilutės tiesiog praleidžiamos
                if (line.Trim() == "")
                    continue;
                string[] parts = line.Split(';');   // eilutės laukai
                int nr, kamb;
                double plotas, kaina;
                // 1) Struktūros tikrinimas: ar pakanka laukų ir ar
                //    skaičiai užrašyti teisingai (TryParse nemeta klaidos)
                if (parts.Length < 5
                    || !int.TryParse(parts[0].Trim(), out nr)
                    || !SkaitytiSkaičių(parts[1], out plotas)
                    || !int.TryParse(parts[2].Trim(), out kamb)
                    || !SkaitytiSkaičių(parts[3], out kaina))
                {
                    Console.WriteLine("{0} eilutė praleista (bloga struktūra): {1}", k + 1, line);
                    continue;
                }
                // 2) Reikšmių tikrinimas: numeris, plotas ir kambariai
                //    turi būti teigiami, kaina - neneigiama
                if (nr <= 0 || plotas <= 0 || kamb <= 0 || kaina < 0)
                {
                    Console.WriteLine("{0} eilutė praleista (neleistinos reikšmės): {1}", k + 1, line);
                    continue;
                }
                // 3) Buto numeris turi būti unikalus
                if (butai.YraNr(nr))
                {
                    Console.WriteLine("{0} eilutė praleista (butas Nr. {1} jau yra): {2}", k + 1, nr, line);
                    continue;
                }
                // Teisinga eilutė: sukuriamas objektas ir dedamas į konteinerį
                if (!butai.Dėti(new Butas(nr, plotas, kamb, kaina, parts[4].Trim())))
                {
                    Console.WriteLine("Per daug butų, likusios eilutės neskaitomos");
                    break;
                }
            }
        }

        //------------------------------------------------------------
        /** Iš visų butų konteinerio atrenka tinkamus butus
        @param visi      - visų butų konteineris
        @param tinkami   - tinkamų butų konteineris
        @param laiptinė  - norima laiptinė
        @param kambariai - norimas kambarių skaičius
        @param kaina     - didžiausia leistina kaina */
        //------------------------------------------------------------
        static void Formuoti(Butai visi, Butai tinkami, int laiptinė, int kambariai, double kaina)
        {
            // Į naują konteinerį dedamos nuorodos į tinkamus butus
            // (objektai nekopijuojami)
            for (int i = 0; i < visi.Imti(); i++)
                if (visi.Imti(i).Tinka(laiptinė, kambariai, kaina))
                    tinkami.Dėti(visi.Imti(i));
        }

        //------------------------------------------------------------
        /** Spausdina konteinerio duomenis lentele faile arba ekrane
        @param butai    - butų konteineris
        @param fv       - rezultatų failo vardas (null - spausdina ekrane)
        @param antraštė - užrašas virš lentelės */
        //------------------------------------------------------------
        static void Spausdinti(Butai butai, string fv, string antraštė)
        {
            const string linija =
                "-------------------------------------------------------------------------------";
            // Vienas metodas tinka ir failui, ir ekranui: abu yra TextWriter
            TextWriter fr = fv == null ? Console.Out : File.AppendText(fv);
            try
            {
                // Lentelės antraštė
                fr.WriteLine(antraštė);
                fr.WriteLine(linija);
                fr.WriteLine("{0,5} {1,9} {2,7} {3,9} {4,10} {5,12}   {6}",
                    "Nr.", "Laiptinė", "Aukštas", "Plotas", "Kambariai", "Kaina, Eur", "Telefonas");
                fr.WriteLine(linija);
                // Kiekvienas butas - viena eilutė (formatą nustato ToString())
                for (int i = 0; i < butai.Imti(); i++)
                    fr.WriteLine(butai.Imti(i).ToString());
                if (butai.Imti() == 0)
                    fr.WriteLine(" Sąrašas tuščias");
                fr.WriteLine(linija);
                fr.WriteLine();
            }
            finally
            {
                // Uždaromas tik failas; Console.Out uždaryti negalima
                if (fv != null)
                    fr.Dispose();
            }
        }

        /** Skaičius su tašku arba kableliu (pvz. 54.5 arba 54,5)
        @param s       - tekstas su skaičiumi
        @param reikšmė - gautas skaičius
        @return true, jei tekstą pavyko paversti skaičiumi */
        static bool SkaitytiSkaičių(string s, out double reikšmė)
        {
            // Kablelis pakeičiamas tašku ir skaitoma su InvariantCulture,
            // todėl rezultatas nepriklauso nuo kompiuterio kalbos nustatymų
            return double.TryParse(s.Trim().Replace(',', '.'), NumberStyles.Float,
                                   CultureInfo.InvariantCulture, out reikšmė);
        }

        /** Klausia tol, kol įvedamas sveikasis skaičius intervale [min, max]
        @param klausimas - ekrane rodomas tekstas
        @param min       - mažiausia leistina reikšmė
        @param max       - didžiausia leistina reikšmė */
        static int SkaitytiSveikąjį(string klausimas, int min, int max)
        {
            while (true)
            {
                Console.Write(klausimas);
                string s = Console.ReadLine();
                // null - įvedimas nutrauktas (pvz. Ctrl+Z / Ctrl+D)
                if (s == null)
                    throw new EndOfStreamException("Nutrauktas įvedimas");
                int x;
                // Tinka tik sveikasis skaičius nurodytame intervale
                if (int.TryParse(s.Trim(), out x) && x >= min && x <= max)
                    return x;
                Console.WriteLine("Neteisinga reikšmė. Reikia sveikojo skaičiaus nuo {0} iki {1}.", min, max);
            }
        }

        /** Klausia tol, kol įvedamas neneigiamas skaičius
        @param klausimas - ekrane rodomas tekstas */
        static double SkaitytiKainą(string klausimas)
        {
            while (true)
            {
                Console.Write(klausimas);
                string s = Console.ReadLine();
                // null - įvedimas nutrauktas (pvz. Ctrl+Z / Ctrl+D)
                if (s == null)
                    throw new EndOfStreamException("Nutrauktas įvedimas");
                double x;
                // Tinka skaičius su tašku arba kableliu, ne mažesnis už 0
                if (SkaitytiSkaičių(s, out x) && x >= 0)
                    return x;
                Console.WriteLine("Neteisinga reikšmė. Reikia neneigiamo skaičiaus (pvz. 85000 arba 85000,50).");
            }
        }
    }
}
