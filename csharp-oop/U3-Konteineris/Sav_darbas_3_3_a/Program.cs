// Savarankiško darbo 3 užduotis.
// Pirmoje failo eilutėje nurodytas fakulteto pavadinimas. Tekstiniame faile yra
// fakulteto žiemos sesijos pažymių sąrašas. Eilutėje apie studentą yra tokie
// duomenys: pavardė, vardas, grupė, pažymių kiekis, pažymiai. Nustatykite
// kiekvienos grupės bendrą mokymosi vidurkį. Rezultatus išspausdinkite
// surikiuotus mažėjančiai pagal vidurkį ir pagal grupes abėcėliškai.
//
// Duomenų failo Duom.txt eilutės formatas:
//   pavardė vardas; grupė; pažymių kiekis; pažymiai (atskirti tarpais)
using System;
using System.IO;
using System.Text;

namespace Sav_P3_2
{
    //------------------------------------------------------------
    /** Klasė vieno studento duomenims saugoti
        @class Studentas */
    //------------------------------------------------------------
    class Studentas
    {
        private string pavVrd;    // studento pavardė ir vardas
        private string grupe;     // studento grupė
        private int kiek;         // pažymių kiekis
        private int[] pazym;      // pažymiai (įvertinimai)
        private int suma;         // pagalbinis kintamasis: pažymių suma
        private double vidurkis;  // pagalbinis kintamasis: pažymių vidurkis
        private string eilute;    // pagalbinis kintamasis: pažymiai, paruošti spausdinti

        /** Konstruktorius be parametrų */
        public Studentas()
        {

        }

        //------------------------------------------------------------
        /** Konstruktorius su parametrais: įrašo duomenis ir iš karto
        // apskaičiuoja pažymių sumą, vidurkį bei spausdinimo eilutę
        @param pavv  - pavardė ir vardas
        @param grupe - grupė
        @param kiek  - pažymių kiekis
        @param pazym - pažymių masyvas */
        //------------------------------------------------------------
        public Studentas(string pavv, string grupe, int kiek, int[] pazym)
        {
            pavVrd = pavv;
            this.grupe = grupe;
            this.kiek = kiek;
            this.pazym = pazym;
            this.suma = 0;
            eilute = "";
            // Viename cikle skaičiuojama pažymių suma ir formuojama
            // spausdinimo eilutė (kiekvienas pažymys - 3 simbolių laukas)
            for (int j = 0; j < kiek; j++)
            {
                this.suma += pazym[j];
                eilute = eilute + String.Format("{0,3:d}   ", pazym[j]);
            }
            // PATAISYTA: kai pažymių nėra, nedalinama iš nulio
            // (* 1.0 - kad dalyba būtų realioji, o ne sveikųjų skaičių)
            vidurkis = kiek > 0 ? (suma * 1.0) / kiek : 0;
        }

        /** Grąžina pavardę ir vardą */
        public string ImtiPavv() { return pavVrd; }

        /** Grąžina grupę */
        public string ImtiGr() { return grupe; }

        /** Grąžina pažymių kiekį */
        public int ImtiKiek() { return kiek; }

        /** Grąžina i-tąjį pažymį
        @param i - pažymio indeksas */
        public int ImtiPaz(int i) { return pazym[i]; }

        /** Grąžina pažymių sumą */
        public int ImtiSuma() { return suma; }

        // Užklotas metodas ToString(): studento duomenys vienoje eilutėje
        public override string ToString()
        {
            string eilute1;
            eilute1 = string.Format("{0, -20}   {1, 2}  {2,-15} ", pavVrd, grupe, eilute);
            return eilute1;
        }

        // Užklotas metodas Equals()
        // PATAISYTA: null arba ne Studentas tipo objektas nebemeta klaidos
        public override bool Equals(object objektas)
        {
            // as grąžina null, jei objektas nėra Studentas,
            // o Equals(Studentas) null atveju grąžina false
            return Equals(objektas as Studentas);
        }

        /** Lygina studentus pagal pavardę ir vardą
        @param stud - kitas studentas */
        public bool Equals(Studentas stud)
        {
            // ReferenceEquals naudojamas vietoj stud == null, nes
            // operatorius == šioje klasėje užklotas
            if (ReferenceEquals(stud, null))
                return false;
            return stud.pavVrd == pavVrd;
        }

        // Užklotas metodas GetHashCode()
        // PATAISYTA: suderinta su Equals (lygūs objektai - vienodas kodas)
        public override int GetHashCode()
        {
            return pavVrd == null ? 0 : pavVrd.GetHashCode();
        }

        // Užklotas operatorius ==: true, jei sutampa pavardė ir vardas
        public static bool operator ==(Studentas stud1, Studentas stud2)
        {
            //Console.WriteLine("Darbas");
            // PATAISYTA: palyginimas su null nebemeta klaidos
            // (jei bent vienas null - lygūs tik tada, kai abu null)
            if (ReferenceEquals(stud1, null) || ReferenceEquals(stud2, null))
                return ReferenceEquals(stud1, stud2);
            return stud1.pavVrd == stud2.pavVrd;
            // arba
            //int poz = String.Compare(stud1.pavVrd, stud2.pavVrd,
            //                           StringComparison.CurrentCulture);
            //return poz == 0;
        }

        // Užklotas operatorius !=: priešingas operatoriui ==
        public static bool operator !=(Studentas stud1, Studentas stud2)
        {
            return !(stud1 == stud2);
            // arba
            //int poz = String.Compare(stud1.pavVrd, stud2.pavVrd,
            //                           StringComparison.CurrentCulture);
            //return poz != 0;
        }

        // Užklotas operatorius >: true, jei pavardė ir vardas abėcėliškai toliau
        public static bool operator >(Studentas stud1, Studentas stud2)
        {
            // String.Compare grąžina <0, 0 arba >0
            int poz = String.Compare(stud1.pavVrd, stud2.pavVrd
                                     StringComparison.CurrentCulture);
            return poz > 0;
        }

        // Užklotas operatorius <: true, jei pavardė ir vardas abėcėliškai pirmiau
        public static bool operator <(Studentas stud1, Studentas stud2)
        {
            int poz = String.Compare(stud1.pavVrd, stud2.pavVrd,
                                     StringComparison.CurrentCulture);
            return poz < 0;
        }

        // Užklotas operatorius >=: true, jei vidurkis didesnis, o esant
        // vienodiems vidurkiams - pavardė ir vardas abėcėliškai toliau
        // PATAISYTA: anksčiau buvo lyginami pažymių masyvai (nuorodos),
        // o ne vidurkiai
        public static bool operator >=(Studentas stud1, Studentas stud2)
        {
            int poz = String.Compare(stud1.pavVrd, stud2.pavVrd,
                                     StringComparison.CurrentCulture);
            // Pirmiausia lyginamas vidurkis, o jei jie lygūs - pavardė, vardas
            if ((stud1.vidurkis > stud2.vidurkis) ||
                ((stud1.vidurkis == stud2.vidurkis) && (poz > 0))) return true;
            else return false;
        }

        // Užklotas operatorius <=: true, jei vidurkis mažesnis, o esant
        // vienodiems vidurkiams - pavardė ir vardas abėcėliškai pirmiau
        // PATAISYTA: kaip ir operatoriuje >=
        public static bool operator <=(Studentas stud1, Studentas stud2)
        {
            int poz = String.Compare(stud1.pavVrd, stud2.pavVrd,
                                     StringComparison.CurrentCulture);
            if ((stud1.vidurkis < stud2.vidurkis) ||
                ((stud1.vidurkis == stud2.vidurkis) && (poz < 0))) return true;
            else return false;
        }
    }

    //------------------------------------------------------------
    /** Klasė vienos grupės suvestinei saugoti: visų grupės studentų
    // pažymių kiekis, suma ir bendras vidurkis
        @class Grupe */
    //------------------------------------------------------------
    class Grupe
    {
        private string grupe;     // grupės pavadinimas
        private int kiek;         // visų grupės studentų pažymių kiekis
        private int suma;         // visų grupės studentų pažymių suma
        private double vidurkis;  // bendras grupės pažymių vidurkis

        /** Konstruktorius be parametrų */
        public Grupe()
        {

        }

        //------------------------------------------------------------
        /** Konstruktorius su parametrais (pirmojo grupės studento duomenys)
        @param grupe - grupės pavadinimas
        @param kiek  - pažymių kiekis
        @param suma  - pažymių suma */
        //------------------------------------------------------------
        public Grupe(string grupe, int kiek, int suma)
        {
            this.grupe = grupe;
            this.kiek = kiek;
            this.suma = suma;
            // Vidurkis skaičiuojamas iš karto; kai pažymių nėra - 0
            vidurkis = kiek > 0 ? suma * 1.0 / kiek : 0;
        }

        /** Grąžina grupės pavadinimą */
        public string ImtiGr() { return grupe; }

        /** Grąžina grupės pažymių kiekį */
        public int ImtiKiek() { return kiek; }

        /** Grąžina grupės pažymių sumą (parametras i nenaudojamas) */
        public int ImtiSuma(int i) { return suma; }

        //------------------------------------------------------------
        /** Prideda dar vieno studento pažymių kiekį ir sumą,
        // perskaičiuoja grupės vidurkį
        @param kiek - studento pažymių kiekis
        @param suma - studento pažymių suma */
        //------------------------------------------------------------
        public void Deti(int kiek, int suma)
        {
            // Vidurkis skaičiuojamas iš visų pažymių (bendra suma / bendras
            // kiekis), o ne kaip studentų vidurkių vidurkis
            this.kiek += kiek;
            this.suma += suma;
            vidurkis = this.kiek > 0 ? this.suma * 1.0 / this.kiek : 0;
        }

        // Užklotas metodas ToString(): grupė ir vidurkis vienoje eilutėje
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -20}   {1, 7:f2}", grupe, vidurkis);
            return eilute;
        }

        // Užklotas operatorius >=: true, jei vidurkis didesnis, o esant
        // vienodiems vidurkiams - grupės pavadinimas abėcėliškai pirmiau
        // (naudojamas rikiuojant mažėjančiai pagal vidurkį)
        public static bool operator >=(Grupe gr1, Grupe gr2)
        {
            int poz = String.Compare(gr1.grupe, gr2.grupe,
                                     StringComparison.CurrentCulture);
            // poz < 0 - gr1 pavadinimas abėcėliškai pirmiau už gr2
            if ((gr1.vidurkis > gr2.vidurkis) ||
                ((gr1.vidurkis == gr2.vidurkis) && (poz < 0))) return true;
            else return false;
        }

        // Užklotas operatorius <=: true, jei vidurkis mažesnis, o esant
        // vienodiems vidurkiams - grupės pavadinimas abėcėliškai toliau
        public static bool operator <=(Grupe gr1, Grupe gr2)
        {
            int poz = String.Compare(gr1.grupe, gr2.grupe,
                                     StringComparison.CurrentCulture);
            if ((gr1.vidurkis < gr2.vidurkis) ||
                ((gr1.vidurkis == gr2.vidurkis) && (poz > 0))) return true;
            else return false;
        }
    }

    //------------------------------------------------------------
    /** Konteinerinė klasė studentams saugoti
        @class MasyvasStudentai */
    //------------------------------------------------------------
    class MasyvasStudentai
    {
        const int Cn = 50;         // studentų masyvo dydis
        private Studentas[] Studentai;  // studentų objektų masyvas
        private int kiek;                // studentų skaičius

        /** Konstruktorius: sukuria tuščią studentų masyvą */
        public MasyvasStudentai()
        {
            kiek = 0;
            Studentai = new Studentas[Cn];
        }

        /** Grąžina nurodyto indekso studentą
        @param i - studento indeksas */
        public Studentas ImtiStudenta(int i) { return Studentai[i]; }

        /** Grąžina studentų skaičių */
        public int ImtiKiek() { return kiek; }

        /** Padeda naują studentą į masyvą
        @param obj - studento objektas
        @return true, jei pridėta; false, jei masyvas pilnas */
        // PATAISYTA: neviršijamas masyvo dydis
        public bool DėtiStudenta(Studentas obj)
        {
            // Masyvas pilnas - naujo studento dėti negalima
            if (kiek >= Cn)
                return false;
            // Įrašoma į pirmą laisvą vietą, po to kiek padidinamas vienetu
            Studentai[kiek++] = obj;
            return true;
        }

        /** Grąžina nurodyto studento stud indeksą,
        // priešingu atveju grąžina -1 (naudoja užklotą operatorių ==)
        @param stud - ieškomas studentas */
        public int StudentoVieta(Studentas stud)
        {
            for (int i = 0; i < kiek; i++)
            {
                if (Studentai[i] == stud)
                    return i;
            }
            return -1;
        }
    }

    //------------------------------------------------------------
    /** Konteinerinė klasė grupėms saugoti
        @class Grupes */
    //------------------------------------------------------------
    class Grupes
    {
        const int Cn = 50;         // grupių masyvo dydis
        private Grupe[] Gr;        // grupių objektų masyvas
        private int kiek;          // grupių skaičius

        /** Konstruktorius: sukuria tuščią grupių masyvą */
        public Grupes()
        {
            kiek = 0;
            Gr = new Grupe[Cn];
        }

        /** Grąžina nurodyto indekso grupę
        @param i - grupės indeksas */
        public Grupe ImtiGrupe(int i) { return Gr[i]; }

        /** Grąžina grupių skaičių */
        public int ImtiKiek() { return kiek; }

        /** Padeda naują grupę į masyvą
        @param obj - grupės objektas
        @return true, jei pridėta; false, jei masyvas pilnas */
        // PATAISYTA: neviršijamas masyvo dydis
        public bool DėtiGr(Grupe obj)
        {
            // Masyvas pilnas - naujos grupės dėti negalima
            if (kiek >= Cn)
                return false;
            // Įrašoma į pirmą laisvą vietą, po to kiek padidinamas vienetu
            Gr[kiek++] = obj;
            return true;
        }

        //------------------------------------------------------------
        // TRŪKSTAMAS METODAS (1)
        /** Grąžina grupės, kurios pavadinimas grupe, indeksą masyve,
        // priešingu atveju grąžina -1
        @param grupe - ieškomos grupės pavadinimas */
        //------------------------------------------------------------
        public int Ieškoti(string grupe)
        {
            // Nuoseklioji paieška: tikrinama kiekviena grupė iš eilės
            for (int i = 0; i < kiek; i++)
                if (Gr[i].ImtiGr() == grupe)
                    return i;
            return -1;
        }

        //------------------------------------------------------------
        /** Prie j-tosios grupės prideda studento pažymių kiekį ir sumą
        @param j    - grupės indeksas
        @param kiek - pažymių kiekis
        @param suma - pažymių suma */
        //------------------------------------------------------------
        public void Deti_j(int j, int kiek, int suma)
        {
            Gr[j].Deti(kiek, suma);

        }

        //------------------------------------------------------------
        /** Surikiuoja grupes išrinkimo metodu: mažėjančiai pagal vidurkį,
        // esant vienodiems vidurkiams - abėcėliškai pagal pavadinimą.
        // Kiekviename žingsnyje surandama "didžiausia" likusi grupė
        // (naudojamas užklotas operatorius >=) ir sukeičiama su i-tąja. */
        //------------------------------------------------------------
        public void MinMax()
        {
            int maxInd;     // elemento su didžiausia reikšme indeksas
            for (int i = 0; i < kiek - 1; i++)
            {
                // Ieškoma "didžiausios" grupės tarp likusių (i..kiek-1)
                maxInd = i;
                for (int j = i + 1; j < kiek; j++)
                {
                    if (Gr[j] >= Gr[maxInd])
                        maxInd = j;
                }
                // Rasta grupė sukeičiama vietomis su i-tąja
                // (per pagalbinį kintamąjį gr)
                Grupe gr = Gr[i];
                Gr[i] = Gr[maxInd];
                Gr[maxInd] = gr;
            }
        }
    }

    //------------------------------------------------------------
    /** Pagrindinė klasė: duomenų skaitymas, skaičiavimai, spausdinimas */
    //------------------------------------------------------------
    class Program
    {
        const int Cn = 100;   // (nenaudojama) maksimalus įrašų skaičius
        // PATAISYTA: kelias skaičiuojamas nuo .exe failo vietos (bin/Debug),
        // todėl veikia ir Windows, ir Mac/Linux (per mono)
        static readonly string CFd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Duom.txt");
        static readonly string CFr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Rezultatai.txt");
        static void Main(string[] args)
        {
            // Lietuviškos raidės ekrane (be BOM simbolių)
            Console.OutputEncoding = new UTF8Encoding(false);
            // Jei nėra duomenų failo - pranešama ir baigiama
            if (!File.Exists(CFd))
            {
                Console.WriteLine("Nerastas duomenų failas: {0}", Path.GetFullPath(CFd));
                return;
            }
            // Ištrinami ankstesnio paleidimo rezultatai
            if (File.Exists(CFr))
                File.Delete(CFr);

            // Studentų duomenų skaitymas ir spausdinimas
            string fakultetas = "";
            MasyvasStudentai Pazymiai = new MasyvasStudentai();
            SkaitytiStudKont(CFd, Pazymiai, ref fakultetas);
            SpausdintiStudKont1(CFr, Pazymiai, "Studentų sąrašas", fakultetas);

            // Grupių vidurkiai: suformuojami, spausdinami, rikiuojami
            Grupes Gr = new Grupes();
            FormuotiGrupes(Pazymiai, Gr);                          // PAPILDYTA
            SpausdintiGrupes(CFr, Gr, "Grupių vidurkiai");
            Gr.MinMax();                                           // PAPILDYTA
            SpausdintiGrupes(CFr, Gr, "Grupių sąrašas po rikiavimo");

            Console.WriteLine("Rezultatai: {0}", Path.GetFullPath(CFr));
            Console.WriteLine("Programa baigė darbą!");
        }

        //------------------------------------------------------------
        // TRŪKSTAMAS METODAS (2)
        /** Iš studentų konteinerio suformuoja grupių konteinerį:
        // kiekvienai grupei sumuojamas visų jos studentų pažymių kiekis ir
        // pažymių suma, iš jų skaičiuojamas bendras grupės vidurkis
        @param Studentai - studentų konteineris
        @param Gr        - grupių konteineris */
        //------------------------------------------------------------
        static void FormuotiGrupes(MasyvasStudentai Studentai, Grupes Gr)
        {
            for (int i = 0; i < Studentai.ImtiKiek(); i++)
            {
                Studentas st = Studentai.ImtiStudenta(i);
                // Ar studento grupė jau yra sąraše?
                int j = Gr.Ieškoti(st.ImtiGr());
                if (j >= 0)
                    // Yra - pridedami studento pažymiai prie grupės
                    Gr.Deti_j(j, st.ImtiKiek(), st.ImtiSuma());
                // Nėra - sukuriama nauja grupė
                else if (!Gr.DėtiGr(new Grupe(st.ImtiGr(), st.ImtiKiek(), st.ImtiSuma())))
                    Console.WriteLine("Per daug grupių, grupė {0} praleista", st.ImtiGr());
            }
        }

        //------------------------------------------------------------
        /** Skaito duomenų failą: pirmoje eilutėje fakulteto pavadinimas,
        // kitose - studentų duomenys. Klaidingos eilutės praleidžiamos.
        @param fv         - duomenų failo vardas
        @param Studentai  - studentų konteineris
        @param fakultetas - nuskaitytas fakulteto pavadinimas */
        //------------------------------------------------------------
        static void SkaitytiStudKont(string fv, MasyvasStudentai Studentai, ref string fakultetas)
        {
            // using užtikrina, kad failas bus uždarytas net įvykus klaidai
            using (StreamReader srautas = new StreamReader(fv))
            {
                string eilute;  // duomenų failo eilutė
                // Pirmoji eilutė - fakulteto pavadinimas (null, jei failas tuščias)
                fakultetas = srautas.ReadLine();
                int i = 0;      // nuskaitytų studentų skaičius
                fakultetas = fakultetas == null ? "" : fakultetas.Trim();
                int nr = 1;     // failo eilutės numeris
                // Skaitoma eilutė po eilutės, kol pasiekiama failo pabaiga (null)
                while ((eilute = srautas.ReadLine()) != null)
                {
                    nr++;
                    // PATAISYTA: tuščios ir klaidingos eilutės praleidžiamos,
                    // o ne nutraukia programą
                    if (eilute.Trim() == "")
                        continue;
                    string[] eilDalis = eilute.Split(';'); // failo eilutės dalys
                    int kiek;
                    // Turi būti bent 4 laukai, o pažymių kiekis - teigiamas
                    if (eilDalis.Length < 4 || !int.TryParse(eilDalis[2].Trim(), out kiek) || kiek <= 0)
                    {
                        Console.WriteLine("{0} eilutė praleista (bloga struktūra): {1}", nr, eilute);
                        continue;
                    }
                    string pavVrd = eilDalis[0].Trim();
                    string grupe = eilDalis[1].Trim();
                    // Pažymiai atskirti tarpais arba tabuliacija;
                    // RemoveEmptyEntries - keli tarpai iš eilės netrukdo
                    string[] eilDalis1 = eilDalis[3].Split(new[] { ' ', '\t' },
                                             StringSplitOptions.RemoveEmptyEntries);
                    // Nurodytas kiekis turi sutapti su tikru pažymių skaičiumi
                    if (eilDalis1.Length != kiek)
                    {
                        Console.WriteLine("{0} eilutė praleista (nurodyta {1} pažymių, rasta {2}): {3}",
                                          nr, kiek, eilDalis1.Length, eilute);
                        continue;
                    }
                    // Pažymiai turi būti sveikieji skaičiai nuo 1 iki 10
                    int[] pazym = new int[kiek];
                    bool geri = true;   // ar visi pažymiai teisingi
                    for (int j = 0; j < kiek; j++)
                    {
                        if (!int.TryParse(eilDalis1[j], out pazym[j]) || pazym[j] < 1 || pazym[j] > 10)
                            geri = false;
                    }
                    if (!geri)
                    {
                        Console.WriteLine("{0} eilutė praleista (pažymiai turi būti 1..10): {1}", nr, eilute);
                        continue;
                    }
                    // Teisinga eilutė: sukuriamas objektas ir dedamas į konteinerį
                    Studentas studentas = new Studentas(pavVrd, grupe, kiek, pazym);
                    if (!Studentai.DėtiStudenta(studentas))
                    {
                        Console.WriteLine("Per daug studentų, likusios eilutės neskaitomos");
                        break;
                    }
                    i++;
                }
            }
        }

        //****************************************************************
        /** Spausdina studentų sąrašą lentele faile (kiekvienas laukas
        // atskirai). Šioje programoje nekviečiamas - paliktas kaip variantas.
        @param fv         - rezultatų failo vardas
        @param Studentai  - studentų konteineris
        @param antraste   - užrašas virš lentelės
        @param fakultetas - fakulteto pavadinimas */
        static void SpausdintiStudKont(string fv, MasyvasStudentai Studentai,
                                     string antraste, string fakultetas)
        {
            const string virsus =
                  "------------------------------------------------------------------\r\n"
                + " Nr.  Pavardė ir vardas        Grupė          Pažymiai\r\n"
                + "------------------------------------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("\n      " + fakultetas);
                fr.WriteLine("\n      " + antraste);
                fr.WriteLine(virsus);
                for (int i = 0; i < Studentai.ImtiKiek(); i++)
                {
                    // Pirmiausia eilės numeris, pavardė, vardas ir grupė,
                    // po to - visi pažymiai toje pačioje eilutėje
                    fr.Write("{0, 3}   {1, -20}  {2, 5}   ",
                           i + 1, Studentai.ImtiStudenta(i).ImtiPavv(), Studentai.ImtiStudenta(i).ImtiGr());
                    for (int j = 0; j < Studentai.ImtiStudenta(i).ImtiKiek(); j++)
                    {
                        fr.Write("{0,3}   ", Studentai.ImtiStudenta(i).ImtiPaz(j));
                    }
                    fr.WriteLine();
                }
                fr.WriteLine("------------------------------------------------------------------\n");
            }
        }

        /** Spausdina studentų sąrašą lentele faile, naudojant ToString()
        @param fv         - rezultatų failo vardas
        @param Studentai  - studentų konteineris
        @param antraste   - užrašas virš lentelės
        @param fakultetas - fakulteto pavadinimas */
        static void SpausdintiStudKont1(string fv, MasyvasStudentai Studentai,
                                     string antraste, string fakultetas)
        {
            const string virsus =
                  "------------------------------------------------------------------\r\n"
                + " Nr.  Pavardė ir vardas     Grupė            Pažymiai\r\n"
                + "------------------------------------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("\n      Fakultetas: " + fakultetas);
                fr.WriteLine("      " + antraste);
                fr.WriteLine(virsus);
                // Eilės numeris + visi studento duomenys iš ToString()
                for (int i = 0; i < Studentai.ImtiKiek(); i++)
                {
                    fr.WriteLine("{0, 3}   {1, -20}",
                           i + 1, Studentai.ImtiStudenta(i).ToString());
                }
                fr.WriteLine("------------------------------------------------------------------\n");
            }
        }
        //**************************************************************

        /** Spausdina grupių vidurkius lentele faile
        @param fv       - rezultatų failo vardas
        @param Gr       - grupių konteineris
        @param antraste - užrašas virš lentelės */
        static void SpausdintiGrupes(string fv, Grupes Gr, string antraste)
        {
            const string virsus =
                " Grupė                Vidurkis\r\n"
                + "-------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("\n      " + antraste);
                fr.WriteLine(virsus);
                if (Gr.ImtiKiek() == 0)
                    fr.WriteLine(" Grupių nėra");
                // Kiekviena grupė - viena eilutė (formatą nustato ToString())
                for (int i = 0; i < Gr.ImtiKiek(); i++)
                {
                    fr.WriteLine("{0}", Gr.ImtiGrupe(i).ToString());
                }
                fr.WriteLine("-------------------------------");
            }
        }
    }
}
