using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace _2017OktEmeltReversi
{
    class Tabla
    {
        char[,] t = { };

        public Tabla(string fajlNeve)
        {
            t = new char[8, 8];
            string[] fajl = File.ReadAllLines(fajlNeve);
            for (int sor = 0; sor <= 7; sor++)
            {
                for (int oszlop = 0; oszlop <= 7; oszlop++)
                {
                    t[sor, oszlop] = fajl[sor][oszlop];
                }
            }
        }

        public void Megjelenit()
        {
            for (int sor = 0; sor < t.GetLength(0); sor++)
            {
                Console.Write("\t");
                for (int oszlop = 0; oszlop < t.GetLength(1); oszlop++)
                {
                    Console.Write($"{t[sor, oszlop]}");
                }
                Console.WriteLine();
            }
        }

        public int Osszeg(char korong)
        {
            int db = 0;
            foreach (var item in t)
            {
                if (item == korong)
                    db++;
            }
            return db;
        }

        public bool VanForditas(char jatekos, int sor, int oszlop, int iranySor, int iranyOszlop)
        {
            int aktSor = 0;
            int aktOszlop = 0;
            char ellenfel = ' ';
            bool nincsEllenfel = false;
            aktSor = sor + iranySor;
            aktOszlop = oszlop + iranyOszlop;
            ellenfel = 'K';
            if (jatekos == 'K')
                ellenfel = 'F';
            nincsEllenfel = true;
            while (aktSor > 0 && aktSor < 8 && aktOszlop > 0 && aktOszlop < 8 && t[aktSor, aktOszlop] == ellenfel)
            {
                aktSor = aktSor + iranySor;
                aktOszlop = aktOszlop + iranyOszlop;
                nincsEllenfel = false;
            }
            if (nincsEllenfel || aktSor < 0 || aktSor > 7 || aktOszlop < 0 || aktOszlop > 7 || t[aktSor, aktOszlop] != jatekos)
                return false;
            return true;
        }

        public bool isSzabalyos(char jatekos, int sor, int oszlop)
        {
            if (t[sor, oszlop] != '#')
                return false;
            for (int dS = -1; dS <= 1; dS++)
                for (int dO = -1; dO <= 1; dO++)
                    if (!(dS == 0 && dO == 0) &&
                        VanForditas(jatekos, sor, oszlop, dS, dO))
                        return true;
            return false;
        }
    }
    class Program
    {
        static Tabla tabla;
        static void Main(string[] args)
        {
            F4();
            F5();
            F6();
            F8();
            F9();
            Console.ReadKey();
        }

        private static void F9()
        {
            const string pam = "K;1;3";
            Console.WriteLine($"\n9. feladat: [jatekos;sor;oszlop] = {pam}");
            string[] m = pam.Split(';');
            char beJatekos = char.Parse(m[0]);
            int beSor = int.Parse(m[1]);
            int beOszlop = int.Parse(m[2]);
            Console.WriteLine(tabla.isSzabalyos(beJatekos, beSor, beOszlop) ? "\tSzabályos lépés!" : "\tNem szabályos lépés!");
        }

        private static void F8()
        {
            const string pam = "F;4;1;0;1";
            Console.WriteLine($"\n8. feladat: [jatekos;sor;oszlop;iranySor;iranyOszlop] = {pam}");
            string[] m = pam.Split(';');
            char beJatekos = char.Parse(m[0]);
            int beSor = int.Parse(m[1]);
            int beOszlop = int.Parse(m[2]);
            int beIranySor = int.Parse(m[3]);
            int beIranyOszlop = int.Parse(m[4]);
            Console.WriteLine(tabla.VanForditas(beJatekos, beSor, beOszlop, beIranySor, beIranyOszlop) ? "\tVan fordítás!" : "\tNincs fordítás!");
        }

        private static void F6()
        {
            Console.WriteLine($"\n6. feladat: Összegzés\n\tKék korongok száma: {tabla.Osszeg('K')}\n\tFehér korongok száma: {tabla.Osszeg('F')}\n\tÜres mezők száma: {tabla.Osszeg('#')}");
        }

        private static void F5()
        {
            Console.WriteLine("5. feladat: A betöltött tábla");
            tabla.Megjelenit();
        }

        private static void F4()
        {
            tabla = new Tabla(@"allas.txt");
        }
    }
}
