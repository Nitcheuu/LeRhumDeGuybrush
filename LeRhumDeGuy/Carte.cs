using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe carte : "numérise" la carte
    /// </summary>
    public class Carte
    {
        #region Attributs
        /// <summary>
        /// Le chemin du fichier .clair
        /// </summary>
        private string cheminAccees;
        /// <summary>
        /// Nom de la carte
        /// </summary>
        private string nom;
        /// <summary>
        /// La carte après lecture du fichier .clair
        /// </summary>
        private char[,] carte = new char[10, 10];
        /// <summary>
        /// La trame
        /// </summary>
        private List<string> carteCrypte = new List<string>();
        /// <summary>
        /// La liste des objets de la classe UniteTerre
        /// </summary>
        private List<UniteTerre> listeUniteTerre = new List<UniteTerre>();
        /// <summary>
        /// La liste des objets de la classe UniteMer
        /// </summary>
        private List<UniteMer> listeUniteMer = new List<UniteMer>();
        /// <summary>
        /// La liste des objets de la classe UniteForet
        /// </summary>
        private List<UniteForet> listeUniteForet = new List<UniteForet>();
        /// <summary>
        /// La liste des objets de la classe Parcelle
        /// </summary>
        private List<Parcelle> listeParcelleTerre = new List<Parcelle>();
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe Carte
        /// </summary>
        /// <param name="cheminAccees">Chemin d'acces au fichier.clair</param>
        public Carte(string cheminAccees, string nom)
        {
            this.cheminAccees = cheminAccees;
            this.nom = nom;
            this.LireLaCarte();
            this.CrypterLaCarte();
        }
        #endregion

        private void LireLaCarte()
        {
            this.ChargerLesUnites();
            this.InstancierLesUnites();
            this.InstancierLesParcelles();
            this.DefinirFrontieresUnitesMer();
            this.DefinirFrontieresUnitesForet();
            this.DefinirFrontieresUnitesTerre();
        }

        private void ChargerLesUnites()
        {
            int i, j = 0;
            string ligne;
            StreamReader sr = new StreamReader(this.cheminAccees);
            while((ligne = sr.ReadLine()) != null)
            {
                for(i=0; i<10; i++)
                {
                    carte[j, i] = ligne[i];
                }
                j++;
            }
        }

        private void InstancierLesUnites()
        {
            int x, y;
            for(y = 0; y<10; y++) {
                for (x = 0; x < 10; x++)
                {
                    if (Convert.ToInt32(carte[y, x]) >= 97 && Convert.ToInt32(carte[y, x]) <= 122)
                    {
                        UniteTerre uniteTerre = new UniteTerre(x, y, carte[y, x]);
                        this.listeUniteTerre.Add(uniteTerre);
                    }
                    else if (carte[y, x] == 'M')
                    {
                        UniteMer uniteMer = new UniteMer(x, y);
                        this.listeUniteMer.Add(uniteMer);
                    }
                    else if (carte[y, x] == 'F')
                    {
                        UniteForet uniteForet = new UniteForet(x, y);
                        this.listeUniteForet.Add(uniteForet);
                    }
                }
            }

        }

        private void InstancierLesParcelles()
        {
            List<UniteTerre> listeGroupeParLettre = new List<UniteTerre>();
            int lettreASCII = 97;
            for (int i = 0; i < 26; i++)
            {
                foreach (UniteTerre uniteTerre in this.listeUniteTerre)
                {
                    if (uniteTerre.RetournerLaLettre() == Convert.ToChar(lettreASCII))
                    {
                        listeGroupeParLettre.Add(uniteTerre);
                    }
                }
                if (listeGroupeParLettre.Count != 0)
                {
                    Parcelle parcelle = new Parcelle(listeGroupeParLettre, Convert.ToChar(lettreASCII));
                    this.listeParcelleTerre.Add(parcelle);
                }
                listeGroupeParLettre.Clear();
                lettreASCII++;
            }
        }


        private void CrypterLaCarte()
        {
            int i;
            (int, int) coordonnees;
            coordonnees.Item1 = 0;
            coordonnees.Item2 = 0;
            for (i = 0; i < 100; i++)
            {

                this.RecuperLesFrontieres(coordonnees);

                this.AjouterLesSeparateurs(coordonnees.Item1);

                coordonnees.Item1++;
                if (coordonnees.Item1 == 10) { coordonnees.Item1 = 0; coordonnees.Item2++; }
            }

        }

        private void RecuperLesFrontieres((int, int) coordonnees)
        {
            foreach (UniteTerre unite in listeUniteTerre)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 && unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(unite.RetournerValeurFrontiers()));
                }
            }
            foreach (UniteForet unite in listeUniteForet)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 && unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(unite.RetournerValeurFrontiers()));
                }
            }
            foreach (UniteMer unite in listeUniteMer)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 && unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(unite.RetournerValeurFrontiers()));
                }
            }
        }

        private void AjouterLesSeparateurs(int coordonneesX)
        {
            if (coordonneesX < 9)
            {
                this.carteCrypte.Add(":");
            }
            else
            {
                this.carteCrypte.Add("|");
            }
        }


        public void AfficherLaCarte()
        {
            int compteur = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nCarte de {0} island :\n", this.nom);
            Console.ResetColor();
            foreach (char unite in this.carte)
            {
                compteur++;
                this.AfficherLeCaractereEnCouleur(unite);
                Console.Write(" ");
                if (compteur == 10)
                {
                    Console.Write("\n");
                    compteur = 0;
                }
            }
        }

        private void AfficherLeCaractereEnCouleur(char caractere)
        {
            if (caractere == 'M')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(caractere);
            }
            else if (caractere == 'F')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(caractere);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write(caractere);
            }
            Console.ResetColor();
        }


        public void AfficherLaCarteCrypte()
        {
            foreach (string crypte in this.carteCrypte)
            {
                Console.Write(crypte);
            }
            Console.Write("\n");
        }

        public void SauvegaderLaCarteCrypte(string adresse)
        {
            using (StreamWriter sw = new StreamWriter(adresse+ this.nom + ".chiffre"))
            {
                foreach(string caractere in this.carteCrypte)
                {
                    sw.Write(caractere);
                }
            }
        }

        public void AfficherLesStats()
        {
            Console.WriteLine("La carte possède : {0} unites", carte.Length);
            Console.WriteLine("Dont {0} unites de terre", listeUniteTerre.Count);
            Console.WriteLine("Dont {0} unites de mer", listeUniteMer.Count);
            Console.WriteLine("Dont {0} unites de foret", listeUniteForet.Count);
            Console.WriteLine("Dont {0} parcelles", listeParcelleTerre.Count);
        }

        private void DefinirFrontieresUnitesMer()
        {
            foreach (UniteMer unite in this.listeUniteMer)
            {
                unite.AjouterFrontieres(this.listeUniteMer);
            }
        }

        private void DefinirFrontieresUnitesForet()
        {
            foreach (UniteForet unite in this.listeUniteForet)
            {
                unite.AjouterFrontieres(this.listeUniteForet);
            }
        }

        private void DefinirFrontieresUnitesTerre()
        {
            foreach (UniteTerre unite in this.listeUniteTerre)
            {
                unite.AjouterFrontieres(this.listeUniteTerre);
            }
        }

        public void AfficherParcelles()
        {
            foreach(Parcelle parcelle in this.listeParcelleTerre)
            {
                parcelle.AfficherInformations("Entier");
            }
        }

        public void InfoParcelleRecherche(char recherche)
        {
            bool estTrouvee = false;
            if (Convert.ToInt32(recherche) >= 97 && Convert.ToInt32(recherche) <= 122)
            {
                foreach (Parcelle parcelle in listeParcelleTerre)
                {
                    if (parcelle.RetournerLaLettre() == recherche)
                    {
                        parcelle.AfficherInformations("Partiel");
                        estTrouvee = true;
                    }
                }
                if (!estTrouvee)
                {
                    Console.WriteLine("La carte ne possede pas de parcelle {0}", recherche);
                }
            }
            else
            {
                Console.WriteLine("ERREUR : Les noms de parcelles ne peuvent etre que des caracteres en minuscule");
            }
        }

        public void TailleParcelleRecherche(int tailleMinimale)
        {
            foreach(Parcelle parcelle in this.listeParcelleTerre)
            {
                if(parcelle.RetournerLaTaille() >= tailleMinimale)
                {
                    parcelle.AfficherInformations("Partiel");
                }
            }
        }

        public void AireMoyenneParcelles()
        {
            double moyenne, somme = 0;
            foreach(Parcelle parcelle in this.listeParcelleTerre)
            {
                somme += parcelle.RetournerLaTaille();
            }
            moyenne = somme / this.listeParcelleTerre.Count;
            Console.WriteLine("Aire moyenne : {0}", moyenne);
        }

        public List<string> RetournerLaTrame()
        {
            return this.carteCrypte;
        }

    }
}
