using System.Globalization;

namespace Cseveges
{
    internal class Program
    {
        static List<Beszelgetes> Beszelgetesek = new();
        static List<string> Tagok = new();
        static void Main(string[] args)
        {
            Feladat3a();
            Feladat3b();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            Feladat8();
        }
        static void Feladat3a()
        {
            using StreamReader sr = new(@"..\..\..\src\csevegesek.txt");
            _ = sr;
            int Darab = 0;
            while (!sr.EndOfStream)
            {
                string Sor = sr.ReadLine();
                string[] Tulajdonsagok = Sor.Split(';');                
                try
                {
                    DateTime kezdet = DateTime.ParseExact(Tulajdonsagok[0], "yy.MM.dd-HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    DateTime veg = DateTime.ParseExact(Tulajdonsagok[1], "yy.MM.dd-HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    string kezdemenyezo = Tulajdonsagok[2];
                    string fogado = Tulajdonsagok[3];
                    Beszelgetesek.Add(new Beszelgetes(kezdet, veg, kezdemenyezo, fogado));
                    Darab++;
                }
                catch (Exception)
                {
                    //Console.WriteLine("Hibás sor kezelve");
                }
            }
            //Console.WriteLine($"Beolvasva {Darab}db csevegés");
        }
        static void Feladat3b()
        {
            using StreamReader sr = new(@"..\..\..\src\tagok.txt");
            _ = sr;
            int Darab = 0;
            while (!sr.EndOfStream)
            {
                string Sor = sr.ReadLine();
                Tagok.Add(Sor);
                Darab++;
            }
            //Console.WriteLine($"Beolvasva {Darab}db tag");
        }
        static void Feladat4()
        {
            foreach (var tag in Tagok)
            {
                Console.WriteLine($"{tag} \t" + Beszelgetesek.Count(x => x.Kezdemenyezo == tag || x.Fogado == tag));
            }
        }
        static void Feladat5()
        {
            TimeSpan Ido = new TimeSpan(0,0,0);
            Beszelgetes Leghosszabb = null;
            foreach (var beszelgetes in Beszelgetesek)
            {
                TimeSpan kulonbseg = beszelgetes.Veg.Subtract(beszelgetes.Kezdet);
                if (kulonbseg > Ido)
                {
                    Ido = kulonbseg;
                    Leghosszabb = beszelgetes;
                }
            }
            Console.WriteLine("5. feladat: Leghosszabb beszélgetés:");
            Console.WriteLine($"\tHossza: {String.Format(Ido.ToString(),"{00.00.00}")}");
            Console.WriteLine($"\tKezdet idő: {Leghosszabb.Kezdet.ToString()}");
            Console.WriteLine($"\tVég:        {Leghosszabb.Veg.ToString()}");
            Console.WriteLine($"\tKezedeményező: {Leghosszabb.Kezdemenyezo}");
            Console.WriteLine($"\tFogadó:        {Leghosszabb.Fogado}");
        }
        static void Feladat6()
        {
            Console.Write("6. feladat: Kérek egy tag nevét: ");
            string Nev = Console.ReadLine();
            TimeSpan OsszesIdo = new TimeSpan(0, 0, 0);
            foreach (var beszelgetes in Beszelgetesek)
            {
                if (beszelgetes.Fogado == Nev || beszelgetes.Kezdemenyezo == Nev)
                {

                    TimeSpan kulonbseg = beszelgetes.Veg.Subtract(beszelgetes.Kezdet);
                    OsszesIdo += kulonbseg;
                }
            }
            Console.WriteLine($"Általa beszélt idő: {String.Format(OsszesIdo.ToString(),"00.00.00")}");
        }
        static void Feladat7()
        {
            Console.WriteLine("7. feladat: Tagok akik nem beszéltek senkivel:");
            foreach (var tag in Tagok)
            {
                int Alkalom = Beszelgetesek.Count(x => x.Kezdemenyezo == tag || x.Fogado == tag);
                if (Alkalom == 0)
                {
                    Console.WriteLine($"{tag}");
                }
            }
        }
        static void Feladat8()
        {
            Console.WriteLine("8. feladat: Leghosszabb csendes időszak 15h-tól");
            DateTime maxCsendKezdete = new DateTime(2021, 9, 27, 15, 0, 0);
            DateTime maxCsendVege = Beszelgetesek[0].Kezdet;
            TimeSpan maxCsendHossz = maxCsendVege - maxCsendKezdete;
            DateTime aktCsendVege = Beszelgetesek[0].Veg;

            foreach (var b in Beszelgetesek.Skip(1))
            {
                if (b.Kezdet > aktCsendVege)   // Ha csend van:
                {
                    TimeSpan aktCsendHossz = b.Kezdet - aktCsendVege;
                    if (aktCsendHossz > maxCsendHossz)
                    {
                        maxCsendHossz = aktCsendHossz;
                        maxCsendKezdete = aktCsendVege;
                        maxCsendVege = b.Kezdet;
                    }
                }
                if (b.Veg > aktCsendVege) aktCsendVege = b.Veg;
            }
            Console.WriteLine($"\tKezdete: {maxCsendKezdete.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tVége:    {maxCsendVege.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tHossza:  {maxCsendHossz}");
        }
    }
}