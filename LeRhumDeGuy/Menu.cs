using System;
using System.Threading;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe statique Menu qui permet de naviguer dans le logiciel
    /// elle fonctionne de pair avec la classe statique Affichage,
    /// Affichage gère la partie visuelle du menu et Menu gère la partie
    /// logique du menu
    /// </summary>
    public static class Menu
    {
        #region Méthodes
        /// <summary>
        /// Afficher un faux chargement et le nom des créateurs"
        /// </summary>
        public static void Chargement()
        {
            Affichage.Chargement();
            Thread.Sleep(3000);
            Console.Clear();
        }
        /// <summary>
        /// Affichage du menu principal et des différentes possibilités
        /// </summary>
        public static void MenuPrinciapl()
        {
            bool reponseOK = false;
            string reponse = "*";
            // la boucle permet de contrôler les entrées de l'utilisateur
            while (!reponseOK)
            {
                Affichage.MenuPrincipal();
                Affichage.MenuPrincipalActions();
                reponse = Console.ReadLine();
                reponseOK = Menu.VerifMenuPrincipal(reponse);
                Console.Clear();
            }
            // Gestion des différentes réponses possibles
            if(reponse == "a")
            {
                Menu.ChargerUneCarte();
            }
            if(reponse == "b")
            {
                Menu.DecrypterUneTrame();
            }
            if (reponse == "q")
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Vérifier si la réponse saisie par l'utilisateur est valide ou non
        /// </summary>
        /// <returns>Validité de la réponse</returns>
        /// <param name="reponse">Réponse</param>
        private static bool VerifMenuPrincipal(string reponse)
        {
            bool reponseOK;
            if (reponse == "a" || reponse == "b" || reponse == "q")
            {
                reponseOK = true;
            }
            else
            {
                reponseOK = false;
            }
            return reponseOK;

        }

        /// <summary>
        /// Demander à l'utilisateur le chemin de la carte .clair et le nom
        /// de la carte
        /// </summary>
        private static void ChargerUneCarte()
        {
            (string, string) cheminEtNom;
            cheminEtNom = Affichage.DemanderCheminCarte();
            try
            {
                Carte carte = new Carte(cheminEtNom.Item1, cheminEtNom.Item2);
                Console.Clear();
                Menu.MenuCarte(carte);
            }catch (Exception)
            {
                Affichage.ErreurChemin();
                Thread.Sleep(2000);
                Console.Clear();
                Menu.ChargerUneCarte();
            }
        }
        /// <summary>
        /// Proposer à l'utilisateur les différentes actions possible avec
        /// la carte et traiter sa réponse
        /// </summary>
        /// <param name="carte">Carte de l'ile (classe)</param>
        private static void MenuCarte(Carte carte)
        {
            string reponse = "";
            bool reponseOK = false;
            while (!reponseOK)
            {
                Affichage.MenuCarte(carte);
                reponse = Console.ReadLine();
                reponseOK = Menu.VerifChargerUneCarte(reponse);
                Console.Clear();
            }
            Menu.ActionsChargerUneCarte(reponse, carte);
        }

        /// <summary>
        /// Vérifier si la réponse saisie para l'utilisateur est valide ou non
        /// </summary>
        /// <returns>Validité de la réponse</returns>
        /// <param name="reponse">Réponse de l'utilisateur</param>
        private static bool VerifChargerUneCarte(string reponse)
        {
            bool reponseOK = false;
            if(reponse == "a" || reponse == "b" || reponse == "c" ||
                        reponse == "d" || reponse == "e" || reponse == "q")
            {
                reponseOK = true;
            }
            else
            {
                reponseOK = false;
            }
            return reponseOK;
        }
        /// <summary>
        /// Traitement de la réponse de l'utilisateur
        /// </summary>
        /// <param name="reponse">Réponse de l'utilisateur</param>
        /// <param name="carte">Carte de l'ile (objet)</param>
        private static void ActionsChargerUneCarte(string reponse, Carte carte)
        {
            string sortie = "*";
            if(reponse == "a")
            {
                while (sortie != "")
                {
                    Affichage.ListeParcelles(carte);
                    sortie = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuCarte(carte); // retourne au menu de la carte
            }
            if (reponse == "b")
            {
                while (sortie != "")
                {
                    string lettre;
                    Console.Write("Lettre de la parcelle : ");
                    lettre = Console.ReadLine();
                    Console.Clear();
                    Affichage.InfoParcelle(carte, lettre);
                    sortie = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuCarte(carte);
            }
            if(reponse == "c")
            {
                while (sortie != "")
                {
                    int nombre;
                    Console.Write("Tu cherches les parcelles de taille supérieure ou égale à : ");
                    nombre = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Affichage.ParcelleSuperieure(carte, nombre);
                    sortie = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuCarte(carte);
            }
            if (reponse == "d")
            {
                while (sortie != "")
                {
                    Affichage.AireMoyen(carte);
                    sortie = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuCarte(carte);
            }
            if (reponse == "e")
            {
                Menu.Cryptage(carte);
            }
            if(reponse == "q")
            {
                Menu.MenuPrinciapl();
            }
        }

        /// <summary>
        /// Série d'actions quand l'utilisateur souhaite crypté la carte
        /// </summary>
        /// <param name="carte">Carte de l'ile (objet)</param>
        public static void Cryptage(Carte carte)
        {
            string sortie = "";
            Affichage.CryptageEnCours();
            while (sortie != "o" || sortie != "n")
            {
                Affichage.AfficherTrame(carte);
                sortie = Console.ReadLine();
                Console.Clear();
                if (sortie == "n")
                {
                    Menu.MenuCarte(carte);
                }
                if (sortie == "o")
                {
                    string addresse;
                    Affichage.ChoixAddresse();
                    addresse = Console.ReadLine();
                    try
                    {
                        carte.SauvegaderLaCarteCrypte(addresse);
                        Affichage.EnvoieEnCours();
                        while (sortie != "")
                        {
                            Affichage.ConfirmationEnvoi(addresse);
                            sortie = Console.ReadLine();
                            Console.Clear();
                        }
                        Menu.MenuCarte(carte);
                    }
                    catch (Exception)
                    {
                        Affichage.ErreurChemin();
                        Thread.Sleep(2000);
                        Console.Clear();
                        Menu.Cryptage(carte);
                    }
                }

            }
            Console.Clear();
        }

        /// <summary>
        /// Routine de décryptage de la carte
        /// </summary>
        public static void DecrypterUneTrame()
        {
            (string, string, string) CheminNomEtDestination;
            string reponse ="*";
            CheminNomEtDestination = Affichage.DemanderCheminTrame();
            try
            {
                Decrypteur.DecrypterLaTrame(CheminNomEtDestination.Item1,
                    CheminNomEtDestination.Item2, CheminNomEtDestination.Item3);
                Carte carte = new Carte(CheminNomEtDestination.Item3 +
                CheminNomEtDestination.Item2 + ".clair",
                    CheminNomEtDestination.Item2);
                Affichage.DecryptageEnCours();
                while (reponse != "")
                {
                    Affichage.AfficherCarteDecryptee(carte,
                    CheminNomEtDestination.Item3);
                    reponse = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuPrinciapl();
            }
            catch (Exception)
            {
                Affichage.ErreurChemin();
                Thread.Sleep(2000);
                Console.Clear();
                Menu.DecrypterUneTrame();
            }

        }
        #endregion
    }
}
