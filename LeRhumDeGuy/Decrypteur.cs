using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    public static class Decrypteur
    {
        public static void DecrypterLaTrame(List<string> trame)
        {
            List<int> frontieres = Decrypteur.FiltrerLaTrame(trame);
            int[,] frontieresTableau = Decrypteur.ConvertListEnTableau(frontieres);
            char[,] tableauNull = Decrypteur.InitialiserTableauNul();
            char[,] carte = Decrypteur.DefinirLaCarte(tableauNull, frontieresTableau);
            int i, j;
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    Console.Write(carte[i, j]);
                }
                Console.Write("\n");
            }

        }

        private static List<int> FiltrerLaTrame(List<string> trame)
        {
            List<int> frontieres = new List<int>();
            foreach (string caractere in trame)
            {
                if (caractere == ":" || caractere == "|")
                {

                }
                else
                {
                    frontieres.Add(Convert.ToInt32(caractere));
                }
            }
            return frontieres;
        }

        private static int[,] ConvertListEnTableau(List<int> frontieres)
        {
            int[,] frontieresTableau = new int[10, 10];
            int position = 0, i, j;
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    frontieresTableau[i, j] = frontieres[position];
                    position++;
                }
            }

            return frontieresTableau;
        }

        private static char[,] InitialiserTableauNul()
        {
            char[,] tableau = new char[10, 10];
            int i, j;
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    tableau[i, j] = '*';
                }
            }
            return tableau;
        }

        private static (bool, bool, bool, bool) LectureBinaireVoisins(int frontiere)
        {
            (bool, bool, bool, bool) NordOuestSudEst = (true, true, true, true);
            if(frontiere >= 8) { NordOuestSudEst.Item4 = false; frontiere -= 8; }
            if(frontiere >= 4) { NordOuestSudEst.Item3 = false; frontiere -= 4; }
            if (frontiere >= 2) { NordOuestSudEst.Item2 = false; frontiere -= 2; }
            if (frontiere >= 1) { NordOuestSudEst.Item1 = false;}
            return NordOuestSudEst;
        }

        private static char[,] DefinirLaCarte(char[,] carte, int[,] frontieres)
        {
            int y, x, ASCII = 97;
            for (y = 0; y < 10; y++)
            {
                for (x = 0; x < 10; x++)
                {
                    if(frontieres[y, x] < 32 && frontieres[y,x] >= 0)
                    {
                        Decrypteur.AlgorithmeDecryptage(ref carte, frontieres, x, y, ref ASCII);
                    }
                    else if(frontieres[y, x] < 64 && frontieres[y,x] >= 32)
                    {
                        carte[y, x] = 'F';
                    }
                    else if(frontieres[y, x] >= 64)
                    {
                        carte[y, x] = 'M';
                    }
                }
            }
            return carte;
        }

        private static void  AlgorithmeDecryptage(ref char[,] carte, int[,] frontieres, int x, int y, ref int ASCII)
        {
            (bool, bool, bool, bool) NordOuestSudEst = Decrypteur.LectureBinaireVoisins(frontieres[y, x]);
            char lettre = '*';
            if (NordOuestSudEst.Item1) { if (carte[y - 1, x] != '*') { lettre = carte[y - 1, x]; } }
            if (NordOuestSudEst.Item2) { if (carte[y, x - 1] != '*') { lettre = carte[y, x - 1]; } }
            if (NordOuestSudEst.Item3) { if (carte[y + 1, x] != '*') { lettre = carte[y + 1, x]; } }
            if (NordOuestSudEst.Item4) { if (carte[y, x + 1 ] != '*') { lettre = carte[y, x + 1]; } }
            Decrypteur.AppliquerLesLettres(ref carte, NordOuestSudEst, ref ASCII, lettre, x, y);

        }

        private static void AppliquerLesLettres(ref char[,] carte, (bool, bool, bool, bool) NordOuestSudEst, ref int ASCII, char lettre, int x, int y)
        {
            if(lettre == '*')
            {
                carte[y, x] = Convert.ToChar(ASCII);
                ASCII++;
            }
            else
            {
                carte[y, x] = lettre;
            }
        }

    }
}
