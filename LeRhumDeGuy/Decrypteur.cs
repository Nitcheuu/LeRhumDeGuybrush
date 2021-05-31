using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe statique Decrypteur : Permet de décrypter une carte et de
    /// sauvegarder la carte décryptée dans la mémoire interne.
    /// </summary>
    public static class Decrypteur
    {
        #region Méthodes
        /// <summary>
        /// Seule méthode publique de la classe, elle permet de décrypter la
        /// carte de a à z et de sauvegarder la carte dans un fichier
        /// .clair
        /// </summary>
        /// <param name="chemin">Chemin de la trame .chiffre</param>
        /// <param name="nom">Nom de sauvegarde du fichier .clair</param>
        /// <param name="destination">Destination de sauvegarde</param>
        public static void DecrypterLaTrame(string chemin, string nom,
                                                        string destination)
        {
            // Récupération de la trame sous forme d'une liste
            List<string> trame = Decrypteur.LireLaTrame(chemin);
            // Suppression des séparateurs ":" et "|"
            List<int> frontieres = Decrypteur.FiltrerLaTrame(trame);
            // Convertir la liste en un tableau 2D
            int[,] frontieresTableau = Decrypteur.ConvertListEnTableau(
                                                                frontieres);
            // Initialiser un tableau de caractère '*' (ici null)
            char[,] tableauNull = Decrypteur.InitialiserTableauNul();
            // Récupérer la carte
            char[,] carte = Decrypteur.DefinirLaCarte(tableauNull, 
                                                        frontieresTableau);
            // Sauvegarder la carte à l'addresse précisée en paramètres
            Decrypteur.SauvegarderLaCarte(nom, destination, carte);
        }
        /// <summary>
        /// Sauvegarder la carte à une addresse
        /// </summary>
        /// <param name="nom">Nom du fichier</param>
        /// <param name="destination">Localisation de la sauvegarde</param>
        /// <param name="carte">Contenu du fichier</param>
        private static void SauvegarderLaCarte(string nom, string destination
                                                            , char[,] carte)
        {
            int i, j;
            using (StreamWriter sw = new StreamWriter(destination + nom +
                                                                 ".clair"))
            {
                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 10; j++)
                    {
                        sw.Write(carte[i, j]);
                    }
                    sw.Write("\n");
                }
            }

        }
        /// <summary>
        /// Lecture de la trame et stockage dans une liste
        /// </summary>
        /// <returns>Trame sous forme de liste</returns>
        /// <param name="chemin">Chemin de la trame</param>
        private static List<string> LireLaTrame(string chemin)
        {
            List<string> trame = new List<string>();
            string ligne, decomposition ="";
            int i;
            StreamReader sr = new StreamReader(chemin);
            while ((ligne = sr.ReadLine()) != null)
            {
                for (i = 0; i < ligne.Length; i++)
                {
                    if (ligne[i] == ':' || ligne[i] == '|')
                    {
                        trame.Add(decomposition);
                        trame.Add(Convert.ToString(ligne[i]));
                        decomposition = "";
                    }
                    else
                    {
                       decomposition += Convert.ToString(ligne[i]);
                    }
                }
            }
            return trame;
        }

        /// <summary>
        /// Filtre la trame des caractères parasites ":" et "|"
        /// </summary>
        /// <returns>Trame</returns>
        /// <param name="trame">Liste frontières</param>
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

        /// <summary>
        /// Convetir la liste en un tableau 2 dimensions 
        /// </summary>
        /// <returns>Liste des frontières</returns>
        /// <param name="frontieres">Tableau des frontières</param>
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

        /// <summary>
        /// Initialise un tableau remplie du caractère null '*'
        /// (Totalement arbitraire)
        /// </summary>
        /// <returns>Tableau de caractères nul</returns>
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
        /// <summary>
        /// Lecture binaire des frontières
        /// </summary>
        /// <returns>Tuple de boléens qui représente les voisins</returns>
        /// <param name="frontiere">frontière d'une unité</param>
        private static (bool, bool, bool, bool) LectureBinaireVoisins(int frontiere)
        {
            (bool, bool, bool, bool) NordOuestSudEst = (true, true, true, true);
            if(frontiere >= 8) { NordOuestSudEst.Item4 = false; frontiere -= 8; }
            if(frontiere >= 4) { NordOuestSudEst.Item3 = false; frontiere -= 4; }
            if (frontiere >= 2) { NordOuestSudEst.Item2 = false; frontiere -= 2; }
            if (frontiere >= 1) { NordOuestSudEst.Item1 = false;}
            return NordOuestSudEst;
        }
        /// <summary>
        /// Décryptage de la trame à l'aide du tableau des frontières
        /// </summary>
        /// <returns>Tableau 2D de la carte</returns>
        /// <param name="carte">carte nulle ('*')</param>
        /// <param name="frontieres">Tableau des frontières</param>
        private static char[,] DefinirLaCarte(char[,] carte, int[,] frontieres)
        {
            // Le code ASCII sert à changer de lettre
            int y, x, ASCII = 97;
            for (y = 0; y < 10; y++)
            {
                for (x = 0; x < 10; x++)
                {
                    // si c'est une unité de terre on utilise l'algo de
                    // décryptage pour trouver la lettre, démarche inutile
                    // dans le cas d'une unité de forêt ou de mer
                    if(frontieres[y, x] < 32 && frontieres[y,x] >= 0)
                    {
                        Decrypteur.AlgorithmeDecryptage(ref carte, 
                                                frontieres, x, y, ref ASCII);
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
        /// <summary>
        /// Algorithme de décryptage, il agit unité par unité et applique la
        /// lettre à l'unité mais aussi à tous ses voisins. Il détecte une
        /// le commencement d'une nouvelle parcelle en vérifiant si les
        /// voisins sont nulls eux aussi. Il procède par balayage de haut en
        /// bas et de droite à gauche.
        /// </summary>
        /// <param name="carte">Tableau de la carte en référence</param>
        /// <param name="frontieres">Tableau des frontières</param>
        /// <param name="x">Coordonnée x de l'unité</param>
        /// <param name="y">Coordonnée y de l'unité</param>
        /// <param name="ASCII">Code ASCII en ref(prochaine lettre)</param>
        private static void  AlgorithmeDecryptage(ref char[,] carte, 
                            int[,] frontieres, int x, int y, ref int ASCII)
        {
            (bool, bool, bool, bool) NordOuestSudEst =
            Decrypteur.LectureBinaireVoisins(frontieres[y, x]);
            char lettre = '*';
            // Vérification des lettres des voisins
            // lettre change de valeur si une unité voisine n'est pas nulle
            if (NordOuestSudEst.Item1) { if (carte[y - 1, x] != '*') {
                                            lettre = carte[y - 1, x]; } }
            if (NordOuestSudEst.Item2) { if (carte[y, x - 1] != '*') { 
                                            lettre = carte[y, x - 1]; } }
            if (NordOuestSudEst.Item3) { if (carte[y + 1, x] != '*') { 
                                            lettre = carte[y + 1, x]; } }
            if (NordOuestSudEst.Item4) { if (carte[y, x + 1 ] != '*') { 
                                            lettre = carte[y, x + 1]; } }
            Decrypteur.AppliquerLesLettres(ref carte, NordOuestSudEst,
                                                    ref ASCII, lettre, x, y);

        }
        /// <summary>
        /// Appliquers the les lettres.
        /// </summary>
        /// <param name="carte">Tableau de la carte en référence</param>
        /// <param name="NordOuestSudEst">Tuple des voisins</param>
        /// <param name="ASCII">code ASCII en ref (prochaine lettre)</param>
        /// <param name="lettre">Lettre à appliquer</param>
        /// <param name="x">Coordonnée x de l'unité</param>
        /// <param name="y">Coordonnée y de l'unité</param>
        private static void AppliquerLesLettres(ref char[,] carte, 
            (bool, bool, bool, bool) NordOuestSudEst, ref int ASCII,
            char lettre, int x, int y)
        {
            // Si la lettre est nulle alors nouvelle parcelle et on
            // incrémente ASCII pour passer à la lettre suivante
            if(lettre == '*')
            {
                lettre = Convert.ToChar(ASCII);
                carte[y, x] = lettre;
                ASCII++;
            }
            else
            {
                carte[y, x] = lettre;
            }
            // Application de la lettre aux voisins
            if (NordOuestSudEst.Item1) { carte[y - 1, x] = lettre; }
            if (NordOuestSudEst.Item2) { carte[y, x - 1] = lettre; }
            if (NordOuestSudEst.Item3) { carte[y + 1, x] = lettre; }
            if (NordOuestSudEst.Item4) { carte[y, x + 1] = lettre; }
        }
        #endregion
    }
}
