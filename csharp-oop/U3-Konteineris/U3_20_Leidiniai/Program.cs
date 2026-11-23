// U3-20. Leidiniai
// Eilutėje yra
// tokie duomenys: prenumeratoriaus pavardė, adresas, laikotarpio pradžia
// (sveikasis skaičius 1..12), laikotarpio ilgis (sveikasis skaičius 1..12),
// leidinio pavadinimas, vieno mėnesio leidinio kaina.
//   - Nustatykite kiekvienam mėnesiui, kurio leidinio pajamos yra didžiausios.
//   - Nustatykite bendrąsias leidinių pajamas.
//   - Sudarykite sąrašą leidinių, kurių pajamos mažesnės už vidutines.
//     Spausdinamas sąrašas turi būti surikiuotas pagal leidinius abėcėlės tvarka.
//   - Sudarykite nurodyto leidinio (įvedamas klaviatūra) nurodyto mėnesio
//     (įvedamas klaviatūra) prenumeratorių sąrašą.
//
// Duomenų failo U20.txt eilutės formatas (laukai atskirti ';'):
//   pavardė; adresas; pradžia; ilgis; leidinys; kaina
//
// Programos struktūra:
//   Prenumerata  - vienos failo eilutės (vienos prenumeratos) duomenys
//   Prenumeratos - konteineris (masyvas) prenumeratoms saugoti
//   Leidinys     - vieno leidinio suvestinė: pajamos pagal mėnesius
//   Leidiniai    - konteineris (masyvas) leidiniams saugoti
//   Program      - skaitymas, skaičiavimai, rezultatų spausdinimas
using System;
using System.Globalization;   // CultureInfo - skaičių ir teksto formatavimas
using System.IO;              // darbas su failais
using System.Text;            // Encoding - lietuviškos raidės (UTF-8)

namespace U3_20_Leidiniai
{
    //------------------------------------------------------------
    /** Klasė vienos prenumeratos (vienos duomenų failo eilutės)
    // duomenims saugoti
        @class Prenumerata */
    //------------------------------------------------------------
    class Prenumerata
    {
        private string pavardė;    // prenumeratoriaus pavardė
        private string adresas;    // prenumeratoriaus adresas
        private int pradžia;       // laikotarpio pradžia (mėnuo 1..12)
        private int ilgis;         // laikotarpio ilgis (mėnesiais 1..12)
        private string leidinys;   // leidinio pavadinimas
        private decimal kaina;     // vieno mėnesio leidinio kaina

        /** Konstruktorius be parametrų: visi laukai gauna
        // pradines (tuščias) reikšmes */
        public Prenumerata()
        {
            pavardė = "";
            adresas = "";
            pradžia = 1;
            ilgis = 0;
            leidinys = "";
            kaina = 0;
        }

        
        //------------------------------------------------------------
        public Prenumerata(string pavardė, string adresas, int pradžia, int ilgis,
                           string leidinys, decimal kaina)
        {
            // this. reikalingas, nes parametrų vardai sutampa su laukų vardais
            this.pavardė = pavardė;
            this.adresas = adresas;
            this.pradžia = pradžia;
            this.ilgis = ilgis;
            this.leidinys = leidinys;
            this.kaina = kaina;
        }

        /** Grąžina prenumeratoriaus pavardę */
        public string ImtiPavardę() { return pavardė; }
        /** Grąžina prenumeratoriaus adresą */
        public string ImtiAdresą() { return adresas; }
        /** Grąžina leidinio pavadinimą */
        public string ImtiLeidinį() { return leidinys; }
        /** Grąžina vieno mėnesio kainą */
        public decimal ImtiKainą() { return kaina; }
        /** Grąžina laikotarpio pradžios mėnesį */
        public int ImtiPradžią() { return pradžia; }
        /** Grąžina paskutinį laikotarpio mėnesį
        // (pvz. pradžia 3, ilgis 2 -> galioja 3 ir 4 mėn., pabaiga 4) */
        public int ImtiPabaigą() { return pradžia + ilgis - 1; }

        /** Ar prenumerata galioja nurodytą mėnesį
        @param mėnuo - mėnesio numeris 1..12 */
        public bool Galioja(int mėnuo)
        {
            // Mėnuo turi patekti į intervalą [pradžia; pabaiga]
            return mėnuo >= pradžia && mėnuo <= ImtiPabaigą();
        }

        /** Visos prenumeratos pajamos (mėnesių skaičius * mėnesio kaina) */
        public decimal Pajamos() { return ilgis * kaina; }

        // Spausdinimo metodas: visi prenumeratos duomenys vienoje lentelės
        // eilutėje (InvariantCulture - skaičiai visada su tašku)
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0,-14} {1,-26} {2,7:d} {3,6:d}  {4,-24} {5,8:f2} {6,9:f2}",
                pavardė, adresas, pradžia, ilgis, leidinys, kaina, Pajamos());
        }
    }

    //------------------------------------------------------------
    /** Konteinerinė klasė prenumeratoms saugoti
        @class Prenumeratos */
    //------------------------------------------------------------
    class Prenumeratos
    {
        const int CMax = 500;          // maksimalus prenumeratų skaičius
        private Prenumerata[] P;       // prenumeratų duomenys
        private int n;                 // prenumeratų skaičius

        /** Konstruktorius: sukuria tuščią prenumeratų masyvą */
        public Prenumeratos()
        {
            n = 0;
            P = new Prenumerata[CMax];
        }

        /** Grąžina prenumeratų skaičių */
        public int Imti() { return n; }

        /** Grąžina nurodyto indekso prenumeratą
        @param i - prenumeratos indeksas */
        public Prenumerata Imti(int i) { return P[i]; }

        /** Padeda naują prenumeratą, jei neviršijamas masyvo dydis
        @param ob - prenumeratos objektas
        @return true, jei pridėta */
        public bool Dėti(Prenumerata ob)
        {
            // Masyvas pilnas - naujos prenumeratos dėti negalima
            if (n >= CMax)
                return false;
            // Įrašoma į pirmą laisvą vietą, po to n padidinamas vienetu
            P[n++] = ob;
            return true;
        }
    }

    //------------------------------------------------------------
    /** Klasė vieno leidinio suvestinei saugoti: pavadinimas,
    // pajamos kiekvieną mėnesį ir bendros pajamos
        @class Leidinys */
    //------------------------------------------------------------
    class Leidinys
    {
        public const int CMėn = 12;     // mėnesių skaičius
        // Lietuviška kultūra - kad rikiuojant ir lyginant pavadinimus
        // lietuviškos raidės (ą, č, š ir t.t.) būtų teisingoje vietoje
        private static readonly CultureInfo Lt = new CultureInfo("lt-LT");

        private string pavadinimas;     // leidinio pavadinimas
        private decimal[] mėnPajamos;   // pajamos pagal mėnesius [1..12]

        /** Konstruktorius: leidinys be pajamų
        @param pavadinimas - leidinio pavadinimas */
        public Leidinys(string pavadinimas)
        {
            this.pavadinimas = pavadinimas;
            // CMėn + 1 elementų: indeksas 0 nenaudojamas, kad mėnesio
            // numeris (1..12) sutaptų su masyvo indeksu
            mėnPajamos = new decimal[CMėn + 1];
        }

        /** Grąžina leidinio pavadinimą */
        public string ImtiPavadinimą() { return pavadinimas; }

        /** Grąžina leidinio pajamas nurodytą mėnesį
        @param mėnuo - mėnesio numeris 1..12 */
        public decimal ImtiPajamas(int mėnuo) { return mėnPajamos[mėnuo]; }

        /** Bendros leidinio pajamos per visus metus */
        public decimal ImtiPajamas()
        {
            // Sudedamos visų 12 mėnesių pajamos
            decimal suma = 0;
            for (int m = 1; m <= CMėn; m++)
                suma += mėnPajamos[m];
            return suma;
        }

        /** Prideda prenumeratos pajamas prie tų mėnesių, kuriais ji galioja
        @param pr - šio leidinio prenumerata */
        public void Pridėti(Prenumerata pr)
        {
            // Kiekvienam mėnesiui, kurį prenumerata galioja,
            // pridedama vieno mėnesio kaina
            for (int m = 1; m <= CMėn; m++)
                if (pr.Galioja(m))
                    mėnPajamos[m] += pr.ImtiKainą();
        }

        /** Ar pavadinimas sutampa (neatsižvelgiant į raidžių dydį)
        @param pav - lyginamas pavadinimas */
        public bool Vadinasi(string pav)
        {
            // IgnoreCase - "Žmonės" ir "žmonės" laikomi tuo pačiu leidiniu;
            // Trim() pašalina atsitiktinius tarpus įvestame tekste
            return string.Compare(pavadinimas, pav.Trim(), Lt, CompareOptions.IgnoreCase) == 0;
        }

        //-----------------------------------------------------------
        /** Operatorius grąžina true, jeigu pirmojo leidinio pavadinimas
        // abėcėliškai yra pirmiau arba sutampa su antrojo */
        //-----------------------------------------------------------
        public static bool operator <=(Leidinys l1, Leidinys l2)
        {
            // string.Compare grąžina <0, 0 arba >0
            return string.Compare(l1.pavadinimas, l2.pavadinimas, Lt, CompareOptions.None) <= 0;
        }

        //-----------------------------------------------------------
        /** Operatorius grąžina true, jeigu pirmojo leidinio pavadinimas
        // abėcėliškai yra toliau arba sutampa su antrojo
        // (C# reikalauja, kad <= ir >= būtų užkloti poromis) */
        //-----------------------------------------------------------
        public static bool operator >=(Leidinys l1, Leidinys l2)
        {
            return string.Compare(l1.pavadinimas, l2.pavadinimas, Lt, CompareOptions.None) >= 0;
        }

        // Spausdinimo metodas: pavadinimas ir bendros metų pajamos
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0,-24} {1,12:f2}",
                                 pavadinimas, ImtiPajamas());
        }
    }

    //------------------------------------------------------------
    /** Konteinerinė klasė leidiniams saugoti
        @class Leidiniai */
    //------------------------------------------------------------
    class Leidiniai
    {
        const int CMax = 100;      // maksimalus leidinių skaičius
        private Leidinys[] L;      // leidinių duomenys
        private int n;             // leidinių skaičius

        /** Konstruktorius: sukuria tuščią leidinių masyvą */
        public Leidiniai()
        {
            n = 0;
            L = new Leidinys[CMax];
        }

        /** Grąžina leidinių skaičių */
        public int Imti() { return n; }

        /** Grąžina nurodyto indekso leidinį
        @param i - leidinio indeksas */
        public Leidinys Imti(int i) { return L[i]; }

        /** Padeda naują leidinį, jei neviršijamas masyvo dydis
        @param ob - leidinio objektas
        @return true, jei pridėta */
        public bool Dėti(Leidinys ob)
        {
            // Masyvas pilnas - naujo leidinio dėti negalima
            if (n >= CMax)
                return false;
            L[n++] = ob;
            return true;
        }

        /** Grąžina leidinio indeksą arba -1, jei tokio nėra
        @param pavadinimas - ieškomo leidinio pavadinimas */
        public int Ieškoti(string pavadinimas)
        {
            // Nuoseklioji paieška: tikrinamas kiekvienas leidinys iš eilės
            for (int i = 0; i < n; i++)
                if (L[i].Vadinasi(pavadinimas))
                    return i;
            return -1;
        }

        /** Bendros visų leidinių pajamos */
        public decimal BendrosPajamos()
        {
            decimal suma = 0;
            for (int i = 0; i < n; i++)
                suma += L[i].ImtiPajamas();
            return suma;
        }

        /** Vidutinės vieno leidinio pajamos (0, jei leidinių nėra) */
        public decimal VidutinėsPajamos()
        {
            // Tikrinama n > 0, kad nebūtų dalinama iš nulio
            return n > 0 ? BendrosPajamos() / n : 0;
        }

        /** Didžiausios nurodyto mėnesio vieno leidinio pajamos
        @param mėnuo - mėnesio numeris 1..12 */
        public decimal DidžiausiosPajamos(int mėnuo)
        {
            // Pradinė reikšmė 0, nes pajamos negali būti neigiamos.
            // Jei tą mėnesį prenumeratų nėra - grąžinamas 0
            decimal max = 0;
            for (int i = 0; i < n; i++)
                if (L[i].ImtiPajamas(mėnuo) > max)
                    max = L[i].ImtiPajamas(mėnuo);
            return max;
        }

        /** Surikiuoja leidinius išrinkimo metodu pagal pavadinimą abėcėliškai */
        public void Rikiuoti()
        {
            for (int i = 0; i < n - 1; i++)
            {
                // Tarp likusių (i..n-1) leidinių ieškomas abėcėliškai pirmasis
                Leidinys min = L[i];
                int im = i;            // rasto leidinio indeksas
                for (int j = i + 1; j < n; j++)
                    if (L[j] <= min)       // naudojamas užklotas operatorius <=
                    {
                        min = L[j];
                        im = j;
                    }
                // Rastas leidinys sukeičiamas vietomis su i-tuoju
                L[im] = L[i];
                L[i] = min;
            }
        }
    }

    //------------------------------------------------------------
    /** Pagrindinė klasė: duomenų skaitymas, skaičiavimai,
    // rezultatų spausdinimas */
    //------------------------------------------------------------
    class Program
    {
        // Duomenų ir rezultatų failai - projekto aplanke (du lygiai virš bin/Debug)
        static readonly string CFd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "U20.txt");
        static readonly string CFr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Rezultatai.txt");

        // Mėnesių pavadinimai: indeksas sutampa su mėnesio numeriu
        // (0 elementas tuščias ir nenaudojamas)
        static readonly string[] MėnPav = { "", "Sausis", "Vasaris", "Kovas", "Balandis",
            "Gegužė", "Birželis", "Liepa", "Rugpjūtis", "Rugsėjis", "Spalis", "Lapkritis", "Gruodis" };

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

            // Pradinių duomenų skaitymas ir spausdinimas
            Prenumeratos prenumeratos = new Prenumeratos();
            Skaityti(prenumeratos, CFd);
            Spausdinti(prenumeratos, CFr, " Pradiniai duomenys");

            // Be prenumeratų tolimesni skaičiavimai neturi prasmės
            if (prenumeratos.Imti() == 0)
            {
                Rašyti(CFr, " Duomenų faile nėra prenumeratų - skaičiuoti nėra ką.");
                Console.WriteLine("Duomenų faile nėra prenumeratų.");
                return;
            }

            // Iš prenumeratų sudaromas leidinių sąrašas su pajamomis pagal mėnesius
            Leidiniai leidiniai = new Leidiniai();
            FormuotiLeidinius(prenumeratos, leidiniai);

            // 1. Kiekvieno mėnesio didžiausių pajamų leidinys
            SpausdintiMėnesius(leidiniai, CFr);

            // 2. Bendrosios pajamos (leidinių sąrašas spausdinamas abėcėliškai)
            leidiniai.Rikiuoti();
            Spausdinti(leidiniai, CFr, " Leidinių pajamos");
            Rašyti(CFr, string.Format(CultureInfo.InvariantCulture,
                " Bendrosios leidinių pajamos: {0:f2} Eur", leidiniai.BendrosPajamos()));
            Rašyti(CFr, string.Format(CultureInfo.InvariantCulture,
                " Vidutinės vieno leidinio pajamos: {0:f2} Eur\r\n", leidiniai.VidutinėsPajamos()));

            // 3. Leidiniai, kurių pajamos mažesnės už vidutines (surikiuoti)
            Leidiniai mažesni = new Leidiniai();
            FormuotiMažesnius(leidiniai, mažesni);
            mažesni.Rikiuoti();
            if (mažesni.Imti() > 0)
                Spausdinti(mažesni, CFr, " Leidiniai, kurių pajamos mažesnės už vidutines");
            else
                Rašyti(CFr, " Leidinių, kurių pajamos mažesnės už vidutines, nėra\r\n");

            Console.WriteLine("Pagrindiniai rezultatai įrašyti: {0}", Path.GetFullPath(CFr));
            Console.WriteLine();

            // 4. Nurodyto leidinio nurodyto mėnesio prenumeratoriai
            // Pirmiausia parodomas galimų leidinių sąrašas, kad vartotojui
            // būtų lengviau įvesti pavadinimą
            Console.Write("Leidiniai:");
            for (int i = 0; i < leidiniai.Imti(); i++)
                Console.Write("{0} {1}", i == 0 ? "" : ",", leidiniai.Imti(i).ImtiPavadinimą());
            Console.WriteLine();
            string leidinys = SkaitytiLeidinį(leidiniai);
            int mėnuo = SkaitytiMėnesį();

            // Atrinkti prenumeratoriai spausdinami ir faile, ir ekrane
            Prenumeratos atrinkti = new Prenumeratos();
            FormuotiPrenumeratorius(prenumeratos, atrinkti, leidinys, mėnuo);
            string antraštė = string.Format(" Leidinio \"{0}\" prenumeratoriai, mėnuo: {1} ({2})",
                                            leidinys, mėnuo, MėnPav[mėnuo]);
            SpausdintiPrenumeratorius(atrinkti, CFr, antraštė);
            SpausdintiPrenumeratorius(atrinkti, null, antraštė);

            Console.WriteLine("Rezultatai: {0}", Path.GetFullPath(CFr));
            Console.WriteLine("Programa baigė darbą!");
        }

        //------------------------------------------------------------
        /** Failo duomenis surašo į prenumeratų konteinerį. Klaidingos
        // eilutės praleidžiamos, apie jas pranešama ekrane ir faile.
        @param prenumeratos - prenumeratų konteineris
        @param fv           - duomenų failo vardas */
        //------------------------------------------------------------
        static void Skaityti(Prenumeratos prenumeratos, string fv)
        {
            // Visos failo eilutės nuskaitomos į masyvą iš karto
            string[] lines = File.ReadAllLines(fv, Encoding.UTF8);
            for (int k = 0; k < lines.Length; k++)
            {
                string line = lines[k];
                // Tuščios eilutės tiesiog praleidžiamos
                if (line.Trim() == "")
                    continue;
                string klaida = null;          // klaidos aprašymas (null - klaidų nėra)
                string[] parts = line.Split(';');
                int pradžia = 0, ilgis = 0;
                decimal kaina = 0;
                // Tikrinami visi laukai iš eilės; radus pirmą klaidą,
                // tolesni tikrinimai nebevykdomi (else if grandinė)
                if (parts.Length != 6)
                    klaida = "turi būti 6 laukai, atskirti ';'";
                else if (parts[0].Trim() == "" || parts[4].Trim() == "")
                    klaida = "tuščia pavardė arba leidinio pavadinimas";
                else if (!int.TryParse(parts[2].Trim(), out pradžia) || pradžia < 1 || pradžia > 12)
                    klaida = "laikotarpio pradžia turi būti 1..12";
                else if (!int.TryParse(parts[3].Trim(), out ilgis) || ilgis < 1 || ilgis > 12)
                    klaida = "laikotarpio ilgis turi būti 1..12";
                else if (pradžia + ilgis > 13)
                    // Paskutinis mėnuo = pradžia + ilgis - 1, jis negali būti > 12
                    klaida = "prenumerata tęsiasi į kitus metus (pradžia + ilgis > 13)";
                else if (!decimal.TryParse(parts[5].Trim().Replace(',', '.'), NumberStyles.Number,
                                           CultureInfo.InvariantCulture, out kaina) || kaina < 0)
                    // Kablelis pakeičiamas tašku - tinka ir 2.50, ir 2,50
                    klaida = "kaina turi būti neneigiamas skaičius";

                // Klaidinga eilutė: pranešama ir pereinama prie kitos
                if (klaida != null)
                {
                    string pranešimas = string.Format(" {0} eilutė praleista ({1}): {2}", k + 1, klaida, line);
                    Console.WriteLine(pranešimas);
                    Rašyti(CFr, pranešimas);
                    continue;
                }
                // Teisinga eilutė: sukuriamas objektas ir dedamas į konteinerį
                Prenumerata pr = new Prenumerata(parts[0].Trim(), parts[1].Trim(), pradžia, ilgis,
                                                 parts[4].Trim(), kaina);
                if (!prenumeratos.Dėti(pr))
                {
                    Console.WriteLine("Per daug prenumeratų, likusios eilutės neskaitomos");
                    break;
                }
            }
        }

        //------------------------------------------------------------
        /** Iš prenumeratų suformuoja leidinių konteinerį: kiekvienas
        // leidinys įrašomas vieną kartą, sumuojamos jo mėnesių pajamos
        @param prenumeratos - prenumeratų konteineris
        @param leidiniai    - leidinių konteineris */
        //------------------------------------------------------------
        static void FormuotiLeidinius(Prenumeratos prenumeratos, Leidiniai leidiniai)
        {
            for (int i = 0; i < prenumeratos.Imti(); i++)
            {
                Prenumerata pr = prenumeratos.Imti(i);
                // Ar šis leidinys jau yra sąraše?
                int j = leidiniai.Ieškoti(pr.ImtiLeidinį());
                if (j < 0)
                {
                    // Nėra - pridedamas naujas leidinys sąrašo gale
                    if (!leidiniai.Dėti(new Leidinys(pr.ImtiLeidinį())))
                    {
                        Console.WriteLine("Per daug leidinių, \"{0}\" praleistas", pr.ImtiLeidinį());
                        continue;
                    }
                    j = leidiniai.Imti() - 1;   // naujo leidinio indeksas
                }
                // Prenumeratos pajamos pridedamos prie leidinio mėnesių pajamų
                leidiniai.Imti(j).Pridėti(pr);
            }
        }

        //------------------------------------------------------------
        /** Atrenka leidinius, kurių pajamos mažesnės už vidutines
        @param leidiniai - visų leidinių konteineris
        @param mažesni   - atrinktų leidinių konteineris */
        //------------------------------------------------------------
        static void FormuotiMažesnius(Leidiniai leidiniai, Leidiniai mažesni)
        {
            // Vidurkis apskaičiuojamas vieną kartą prieš ciklą
            decimal vidurkis = leidiniai.VidutinėsPajamos();
            for (int i = 0; i < leidiniai.Imti(); i++)
                if (leidiniai.Imti(i).ImtiPajamas() < vidurkis)
                    mažesni.Dėti(leidiniai.Imti(i));
        }

        //------------------------------------------------------------
        /** Atrenka nurodyto leidinio prenumeratas, galiojančias nurodytą mėnesį
        @param prenumeratos - visų prenumeratų konteineris
        @param atrinkti     - atrinktų prenumeratų konteineris
        @param leidinys     - leidinio pavadinimas
        @param mėnuo        - mėnesio numeris 1..12 */
        //------------------------------------------------------------
        static void FormuotiPrenumeratorius(Prenumeratos prenumeratos, Prenumeratos atrinkti,
                                            string leidinys, int mėnuo)
        {
            // Pagalbinis objektas - tik tam, kad galėtume naudoti metodą
            // Vadinasi() (lyginimas neatsižvelgiant į raidžių dydį)
            Leidinys l = new Leidinys(leidinys);
            for (int i = 0; i < prenumeratos.Imti(); i++)
            {
                Prenumerata pr = prenumeratos.Imti(i);
                // Tinka, jei sutampa leidinys ir prenumerata galioja tą mėnesį
                if (l.Vadinasi(pr.ImtiLeidinį()) && pr.Galioja(mėnuo))
                    atrinkti.Dėti(pr);
            }
        }

        //------------------------------------------------------------
        /** Spausdina kiekvieno mėnesio didžiausių pajamų leidinį (-ius).
        // Jei keli leidiniai turi vienodas didžiausias pajamas - spausdinami visi.
        @param leidiniai - leidinių konteineris
        @param fv        - rezultatų failo vardas */
        //------------------------------------------------------------
        static void SpausdintiMėnesius(Leidiniai leidiniai, string fv)
        {
            const string linija = "-----------------------------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                // Lentelės antraštė
                fr.WriteLine(" Didžiausių pajamų leidiniai kiekvieną mėnesį");
                fr.WriteLine(linija);
                fr.WriteLine(" Mėn. Pavadinimas    Pajamos, Eur  Leidinys (-iai)");
                fr.WriteLine(linija);
                for (int m = 1; m <= Leidinys.CMėn; m++)
                {
                    // Pirmiausia randama didžiausia to mėnesio pajamų reikšmė
                    decimal max = leidiniai.DidžiausiosPajamos(m);
                    // Po to surenkami visi leidiniai, turintys tokias pajamas
                    // (pavadinimai atskiriami kableliais)
                    string kurie = "";
                    for (int i = 0; i < leidiniai.Imti(); i++)
                        if (max > 0 && leidiniai.Imti(i).ImtiPajamas(m) == max)
                            kurie += (kurie == "" ? "" : ", ") + leidiniai.Imti(i).ImtiPavadinimą();
                    // max == 0 reiškia, kad tą mėnesį nė viena prenumerata negalioja
                    if (kurie == "")
                        fr.WriteLine(" {0,3:d}  {1,-12} {2,13}  {3}", m, MėnPav[m], "-", "prenumeratų nėra");
                    else
                        fr.WriteLine(string.Format(CultureInfo.InvariantCulture,
                            " {0,3:d}  {1,-12} {2,13:f2}  {3}", m, MėnPav[m], max, kurie));
                }
                fr.WriteLine(linija);
                fr.WriteLine();
            }
        }

        //------------------------------------------------------------
        /** Spausdina prenumeratų konteinerio duomenis lentele faile
        @param prenumeratos - prenumeratų konteineris
        @param fv           - rezultatų failo vardas
        @param antraštė     - užrašas virš lentelės */
        //------------------------------------------------------------
        static void Spausdinti(Prenumeratos prenumeratos, string fv, string antraštė)
        {
            const string linija =
                "---------------------------------------------------------------------------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraštė);
                fr.WriteLine(linija);
                fr.WriteLine(" Pavardė        Adresas                    Pradžia  Ilgis  Leidinys                    Kaina   Pajamos");
                fr.WriteLine(linija);
                // Kiekviena prenumerata - viena eilutė (formatą nustato ToString())
                for (int i = 0; i < prenumeratos.Imti(); i++)
                    fr.WriteLine(" {0}", prenumeratos.Imti(i).ToString());
                if (prenumeratos.Imti() == 0)
                    fr.WriteLine(" Sąrašas tuščias");
                fr.WriteLine(linija);
                fr.WriteLine();
            }
        }

        //------------------------------------------------------------
        /** Spausdina leidinių konteinerio duomenis lentele faile
        @param leidiniai - leidinių konteineris
        @param fv        - rezultatų failo vardas
        @param antraštė  - užrašas virš lentelės */
        //------------------------------------------------------------
        static void Spausdinti(Leidiniai leidiniai, string fv, string antraštė)
        {
            const string linija = "--------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraštė);
                fr.WriteLine(linija);
                fr.WriteLine(" Leidinys                 Pajamos, Eur");
                fr.WriteLine(linija);
                // Kiekvienas leidinys - viena eilutė (formatą nustato ToString())
                for (int i = 0; i < leidiniai.Imti(); i++)
                    fr.WriteLine(" {0}", leidiniai.Imti(i).ToString());
                fr.WriteLine(linija);
                fr.WriteLine();
            }
        }

        //------------------------------------------------------------
        /** Spausdina atrinktus prenumeratorius faile arba ekrane
        @param atrinkti - atrinktų prenumeratų konteineris
        @param fv       - rezultatų failo vardas (null - ekranas)
        @param antraštė - užrašas virš lentelės */
        //------------------------------------------------------------
        static void SpausdintiPrenumeratorius(Prenumeratos atrinkti, string fv, string antraštė)
        {
            const string linija = "------------------------------------------------------------";
            // Vienas metodas tinka ir failui, ir ekranui: abu yra TextWriter
            TextWriter fr = fv == null ? Console.Out : File.AppendText(fv);
            try
            {
                fr.WriteLine(antraštė);
                fr.WriteLine(linija);
                fr.WriteLine(" Pavardė        Adresas                    Laikotarpis (mėn.)");
                fr.WriteLine(linija);
                for (int i = 0; i < atrinkti.Imti(); i++)
                {
                    Prenumerata pr = atrinkti.Imti(i);
                    // Laikotarpis spausdinamas kaip "pradžia - pabaiga"
                    fr.WriteLine(" {0,-14} {1,-26} {2,2:d} - {3,2:d}",
                        pr.ImtiPavardę(), pr.ImtiAdresą(), pr.ImtiPradžią(), pr.ImtiPabaigą());
                }
                if (atrinkti.Imti() == 0)
                    fr.WriteLine(" Tokių prenumeratorių nėra");
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

        /** Įrašo vieną eilutę į rezultatų failą (prideda failo gale)
        @param fv      - rezultatų failo vardas
        @param tekstas - įrašomas tekstas */
        static void Rašyti(string fv, string tekstas)
        {
            using (var fr = File.AppendText(fv))
                fr.WriteLine(tekstas);
        }

        /** Klausia tol, kol įvedamas esamo leidinio pavadinimas.
        // Grąžina pavadinimą taip, kaip jis užrašytas duomenyse.
        @param leidiniai - leidinių konteineris (juose ieškoma) */
        static string SkaitytiLeidinį(Leidiniai leidiniai)
        {
            while (true)
            {
                Console.Write("Įveskite leidinio pavadinimą: ");
                string s = Console.ReadLine();
                // null - įvedimas nutrauktas (pvz. Ctrl+Z / Ctrl+D)
                if (s == null)
                    throw new EndOfStreamException("Nutrauktas įvedimas");
                // Tuščias tekstas iš karto laikomas neteisingu
                int j = s.Trim() == "" ? -1 : leidiniai.Ieškoti(s);
                if (j >= 0)
                    return leidiniai.Imti(j).ImtiPavadinimą();
                Console.WriteLine("Tokio leidinio nėra. Bandykite dar kartą.");
            }
        }

        /** Klausia tol, kol įvedamas mėnesio numeris 1..12 */
        static int SkaitytiMėnesį()
        {
            while (true)
            {
                Console.Write("Įveskite mėnesio numerį (1..12): ");
                string s = Console.ReadLine();
                // null - įvedimas nutrauktas (pvz. Ctrl+Z / Ctrl+D)
                if (s == null)
                    throw new EndOfStreamException("Nutrauktas įvedimas");
                int m;
                // Tinka tik sveikasis skaičius intervale [1; 12]
                if (int.TryParse(s.Trim(), out m) && m >= 1 && m <= 12)
                    return m;
                Console.WriteLine("Neteisingas mėnuo. Reikia sveikojo skaičiaus nuo 1 iki 12.");
            }
        }
    }
}
