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

        #region Méthodes
        /// <summary>
        /// Lecture de la carte
        /// </summary>
        private void LireLaCarte()
        {
            this.ChargerLesUnites();//Lecture du fichier
            this.InstancierLesUnites();
            this.InstancierLesParcelles();
            //Modification de l'attribut frontiere des unites
            this.DefinirFrontieresUnitesMer();
            this.DefinirFrontieresUnitesForet();
            this.DefinirFrontieresUnitesTerre();
        }

        /// <summary>
        /// Lecture du fichier .clair pour le récupérer dans un tableau
        /// de caractères 2 dimensions
        /// </summary>
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

        /// <summary>
        /// Instancier les objets des classes UniteTerre, UniteMer,
        /// UniteForet. Cette méthode utilise les valeurs ASCII des caractères
        /// pour savoir à quelle classe appartient tel caractère. Elle ajoute
        /// également tous les objets dans des listes qui leurs sont propres.
        /// </summary>
        private void InstancierLesUnites()
        {
            int x, y;
            for(y = 0; y<10; y++) {
                for (x = 0; x < 10; x++)
                {
                    // si carte[] entre a et z
                    if (Convert.ToInt32(carte[y, x]) >= 97 &&
                            Convert.ToInt32(carte[y, x]) <= 122)
                    {
                        UniteTerre uniteTerre = new UniteTerre(x, y, 
                                                                carte[y, x]);
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

        /// <summary>
        /// Instancier les objets de la classe parcelle en fonction de la
        /// liste des unités de terre.
        /// </summary>
        private void InstancierLesParcelles()
        {
            List<UniteTerre> listeGroupeParLettre = new List<UniteTerre>();
            int lettreASCII = 97;
            // 26 en référence au nombre de lettres minuscules possible
            for (int i = 0; i < 26; i++)
            {
                foreach (UniteTerre uniteTerre in this.listeUniteTerre)
                {
                    if (uniteTerre.RetournerLaLettre() == 
                                                Convert.ToChar(lettreASCII))
                    {
                        // liste qui regroupe les unites de terre en fonction
                        // de leur lettre
                        listeGroupeParLettre.Add(uniteTerre);
                    }
                }
                if (listeGroupeParLettre.Count != 0)
                {
                    Parcelle parcelle = new Parcelle(listeGroupeParLettre,
                                                Convert.ToChar(lettreASCII));
                    this.listeParcelleTerre.Add(parcelle);
                }
                listeGroupeParLettre.Clear();
                lettreASCII++;
            }
        }

        /// <summary>
        /// Cryptage de la carte sous forme de trame.
        /// </summary>
        private void CrypterLaCarte()
        {
            int x, y;
            for (y = 0; y < 10; y++)
            {
                for (x = 0; x < 10; x++)
                {
                    // Ajoute à la liste carteCryptee la frontière qui
                    // correspond aux coordonnées x y
                    this.RecuperLesFrontieres((x, y));
                    this.AjouterLesSeparateurs(x); // "|" ou ":"
                }
            }
        }

        /// <summary>
        /// Ajoute à la liste trame les frontières.
        /// </summary>
        /// <param name="coordonnees">coordonnées de l'unité</param>
        private void RecuperLesFrontieres((int, int) coordonnees)
        {
            // Vérifie toutes les unités pour voirs si les coordonnées en
            // paramètres correspondent avec celles d'une unité
            foreach (UniteTerre unite in listeUniteTerre)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 &&
                unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(
                                          unite.RetournerValeurFrontiers()));
                }
            }
            foreach (UniteForet unite in listeUniteForet)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 &&
                unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(
                                          unite.RetournerValeurFrontiers()));
                }
            }
            foreach (UniteMer unite in listeUniteMer)
            {
                if (unite.RetournerCoordonnes().Item1 == coordonnees.Item1 && 
                unite.RetournerCoordonnes().Item2 == coordonnees.Item2)
                {
                    this.carteCrypte.Add(Convert.ToString(
                                          unite.RetournerValeurFrontiers()));
                }
            }
        }

        /// <summary>
        /// Ajoute à la liste trame les séparateurs ":" et "|".
        /// </summary>
        /// <param name="coordonneesX">Coordonnée x de l'unité</param>
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

        /// <summary>
        /// Affichage de la carte.
        /// </summary>
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
                // Gestion des espacements et des retours à la ligne
                Console.Write(" ");
                if (compteur == 10)
                {
                    Console.Write("\n");
                    compteur = 0;
                }
            }
        }

        /// <summary>
        /// Affiche en console un caractère en couleur. Chaque type d'unité
        /// a sa couleur qui lui est propre.
        /// </summary>
        /// <param name="caractere">Caractère de l'unité</param>
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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(caractere);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Afficher la trame en console.
        /// </summary>
        public void AfficherLaCarteCrypte()
        {
            foreach (string crypte in this.carteCrypte)
            {
                Console.Write(crypte);
            }
            Console.Write("\n");
        }

        /// <summary>
        /// Enregistrer la trame dans la mémoire interne sous forme d'un
        /// fichier .chiffre
        /// </summary>
        /// <param name="adresse">Adresse de la sauvegarde</param>
        public void SauvegaderLaCarteCrypte(string adresse)
        {
            // Concaténation pour obtenir un chemin valide
            // si l'utilisateur rentre /home/me/ le fichier sera enregistré
            // à l'addresse /home/me/nom.chiffre
            using (StreamWriter sw = new StreamWriter(adresse+ this.nom + 
                                                                ".chiffre"))
            {
                foreach(string caractere in this.carteCrypte)
                {
                    sw.Write(caractere);
                }
            }
        }

        /// <summary>
        /// Définition des frontières pour les unités de mer en fonction de
        /// leurs voisins.
        /// </summary>
        private void DefinirFrontieresUnitesMer()
        {
            foreach (UniteMer unite in this.listeUniteMer)
            {
                unite.AjouterFrontieres(this.listeUniteMer);
            }
        }

        /// <summary>
        /// Définition des frontières pour les unités de forêt en fonction de
        /// leurs voisins.
        /// </summary>
        private void DefinirFrontieresUnitesForet()
        {
            foreach (UniteForet unite in this.listeUniteForet)
            {
                unite.AjouterFrontieres(this.listeUniteForet);
            }
        }

        /// <summary>
        /// Définition des frontières pour les unités de terre en fonction de
        /// leurs voisins.
        /// </summary>
        private void DefinirFrontieresUnitesTerre()
        {
            foreach (UniteTerre unite in this.listeUniteTerre)
            {
                unite.AjouterFrontieres(this.listeUniteTerre);
            }
        }

        /// <summary>
        /// Afficher en console la liste des parcelles, leur taille ainsi que
        /// les coordonnées de toutes les unités qui la compose
        /// </summary>
        public void AfficherParcelles()
        {
            foreach(Parcelle parcelle in this.listeParcelleTerre)
            {
                // Mode entier pour tout afficher sur la parcelle
                parcelle.AfficherInformations("Entier");
            }
        }

        /// <summary>
        /// Afficher en console la taille d'une parcelle en particulier.
        /// </summary>
        /// <param name="recherche">Caractère de la parcelle</param>
        public void InfoParcelleRecherche(char recherche)
        {
            // On vérifie si la parcelle est trouvée
            bool estTrouvee = false;
            if (Convert.ToInt32(recherche) >= 97 && Convert.ToInt32(
                                                           recherche) <= 122)
            {
                foreach (Parcelle parcelle in listeParcelleTerre)
                {
                    if (parcelle.RetournerLaLettre() == recherche)
                    {
                        // Mode partiel pour ne pas afficher 
                        parcelle.AfficherInformations("Partiel");
                        estTrouvee = true;
                    }
                }
                if (!estTrouvee)
                {
                    Console.WriteLine("La carte ne possede pas de parcelle {0}"
                    , recherche);
                }
            }
            else
            {
                Console.WriteLine("ERREUR : Les noms de parcelles ne peuvent etre que des caracteres en minuscule");
            }
        }

        /// <summary>
        /// Afficher en console les parcelles de taille supérieure ou égale
        /// à un entier n
        /// </summary>
        /// <param name="tailleMinimale">Taille minimale.</param>
        public void TailleParcelleRecherche(int tailleMinimale)
        {
            foreach(Parcelle parcelle in this.listeParcelleTerre)
            {
                if(parcelle.RetournerLaTaille() >= tailleMinimale)
                {
                    // N'afficher que la taille
                    parcelle.AfficherInformations("Partiel");
                }
            }
        }

        /// <summary>
        /// Afficher en console l'aire moyenne des parcelles.
        /// </summary>
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
        #endregion 

        /*public List<string> RetournerLaTrame()
        {
            return this.carteCrypte;
        }*/

    }
}
