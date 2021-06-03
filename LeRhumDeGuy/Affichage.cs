using System;
using System.Threading;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe statique Affichage d'afficher les informations en console
    /// elle fonctionne de pair avec la classe statique Menu,
    /// Affichage gère la partie visuelle du menu et Menu gère la partie
    /// logique du menu
    /// </summary>
    public static class Affichage
    {
        //https://fr.rakko.tools/tools/68/
        //invita
        #region Méthodes
        ///<summary>
        ///Affichage du nom de projet ainsi que le nom de chacun des membres du projet 
        ///</summary>
        public static void Chargement()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("     _                 _____                         ______\n ___/__)              (, /   )  /)                  (, /    )\n(, /        _           /__ /  (/      ___            /    /    _\n  /       _(/_       ) /   \\_  / )_(_(_// (_        _/___ /_  _(/_\n (_____             (_/                           (_/___ /\n        )\n");
            Console.Write("     _____)\n   /                    /)              /)\n  /   ___              (/_  __      _  (/\n /     / )  (_(_  (_/_/_)  / (_(_(_/_)_/ )_\n(____ /          .-/\n                (_/\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\n\n\nPar :\n- Augustin Bruckner\n- Paul-Henri Favrelle\n- Lucas Lanterne");
            Console.ResetColor();
        }
        ///<summary>
        /// Texte pour annoncer le menu
        ///</summary>
        public static void MenuPrincipal()
        { 
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("=====================================\n\n");
            Console.Write(" _________   ______  ______   _    _\n| | | | | \\ | |     | |  \\ \\ | |  | |\n| | | | | | | |---- | |  | | | |  | |\n|_| |_| |_| |_|____ |_|  |_| \\_|__|_|\n\n");
            Console.Write("=====================================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("=====================================\n");
            Console.WriteLine("         Oyé Moussaillon !");
            Console.WriteLine("        Que veux tu faire ?");
            Console.Write("=====================================\n");
        }
        ///<summary>
        ///Choix du mode par l'utilisateur ainsi que la possibilité de
        ///quitter le programme.
        /// </summary>
        public static void MenuPrincipalActions()
        { 
            Console.WriteLine("a - Charger une carte");
            Console.WriteLine("b - Lire une trame");
            Console.WriteLine("q - Quitter\n\n");
            Console.Write("Alors moussaillon ? : ");
            Console.ResetColor();
        }
        /// <summary>
        /// Affiche à l'écran une erreur quand le chemin est mauvais
        /// </summary>
        public static void ErreurChemin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================");
            Console.WriteLine("ERREUR : Chemin introuvable");
            Console.WriteLine("===========================");
            Console.ResetColor();
        }
        /// <summary>
        /// En-tete du menu
        /// </summary>
        private static void MenuChargerCarte()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("    /)\n _ (/   _   __    _    _  __           __    _\n(__/ )_(_(_/ (_  (_/__(/_/ (_      (_(_/ (__(/_\n                .-/\n               (_/\n");
            Console.Write("\n _  _   __ _/_   _\n(__(_(_/ (_(__ _(/_\n\n\n");
            Console.ResetColor();
        }
        /// <summary>
        /// Demande d'indication du chemin par l'utilisateur et choix du nom de l'île.
        /// </summary>
        public static (string, string) DemanderCheminCarte()
        {
            (string, string) cheminEtNom;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("==============================================================================\n");
            Console.Write("Indique le chemin absolu de ton fichier .clair et le nom de l'ile moussailon !\n");
            Console.Write("==============================================================================\n\n\n\n");
            Console.WriteLine("Exemple de chemin valide : /home/usr/ile.clair");
            Console.Write("Chemin absolu : ");
            cheminEtNom.Item1 = Console.ReadLine();
            Console.Write("Nom de l'ile : ");
            cheminEtNom.Item2 = Console.ReadLine();
            return cheminEtNom;
        }
        /// <summary>
        /// Choix de l'affichage voulu et possibilité de quitter le problème
        /// </summary>
        public static void MenuCarte(Carte carte)
        {
            carte.AfficherLaCarte();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n=========== Options parcelles ===========");
            Console.WriteLine("a - Afficher la liste des parcelles");
            Console.WriteLine("b - Afficher la taille d'une parcelle");
            Console.WriteLine("c - Afficher les parcelles d'une taille supérieure à un entier x");
            Console.WriteLine("d - Afficher la taille moyenne des parcelles");
            Console.WriteLine("============ Options carte ==============");
            Console.WriteLine("e - Crypter la carte");
            Console.Write("\n\n(q-Quitter)Alors moussaillon ? : ");
            Console.ResetColor();
        }
        /// <summary>
        /// Affichage de la liste des parcelles
        /// </summary>
        /// <param name="carte">Carte de l'ile (objet)</param>
        public static void ListeParcelles(Carte carte)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===================");
            Console.WriteLine("Liste des parcelles");
            Console.WriteLine("===================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            carte.AfficherParcelles();
            Console.WriteLine("\n\nAppuyez sur entrée");
            Console.ResetColor();
        }
        /// <summary>
        /// On effectue une recherche dans la parcelle grâce a une lettre
        /// </summary>
        public static void InfoParcelle(Carte carte, string lettre)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("====================");
            Console.WriteLine("Recherche par lettre");
            Console.WriteLine("====================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            carte.InfoParcelleRecherche(Convert.ToChar(lettre));
            Console.WriteLine("\n\nAppuyez sur entrée");
            Console.ResetColor();
        }
        /// <summary>
        /// Affichage des parcelles supérieures ou égales à un entier n
        /// </summary>
        /// <param name="carte">Carte de l'ile (objet)</param>
        /// <param name="nombre">Entier n</param>
        public static void ParcelleSuperieure(Carte carte, int nombre)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===============================================");
            Console.WriteLine("Parcelles de taille supérieure ou égale à {0} :", nombre);
            Console.WriteLine("===============================================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            carte.TailleParcelleRecherche(nombre);
            Console.WriteLine("\n\nAppuyez sur entrée");
            Console.ResetColor();
        }
        /// <summary>
        /// Affiche l'aire moyen des parcelles
        /// </summary>
        public static void AireMoyen(Carte carte)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========================");
            Console.WriteLine("Aire moyen des parcelles :");
            Console.WriteLine("==========================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            carte.AireMoyenneParcelles();
            Console.WriteLine("\n\nAppuyez sur entrée");
            Console.ResetColor();
        }
        /// <summary>
        /// Ecran d'attente pendant le calcul de l'encryptage de la carte.
        /// </summary>
        public static void CryptageEnCours()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\n\n\n||-------------------------------||\nCryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||011011010----------------------||\nCryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||1110101011011101---------------||\nCryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||0011101011010101010110---------||\nCryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||1101110101110110011111011011010||\nCryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.ResetColor();
        }
        /// <summary>
        /// Affichage de la trame de l'ile
        /// Choix d'envoyer la trame ou non
        /// </summary>
        public static void AfficherTrame(Carte carte)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("             Trame de l'ile : ");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            carte.AfficherLaCarteCrypte();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n   ~('>\n    / )\n     4");
            Console.Write("\nPerroquet savant : \"Dois-je envoyer la trame à Elaine ?\"\n\n");
            Console.ResetColor();
            Console.Write("Votre réponse (o/n) : ");
        }
        /// <summary>
        /// Affichage du choix de destination
        /// </summary>
        public static void ChoixAddresse()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("            Choix de l'addresse ");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n   ~('>\n    / )\n     4");
            Console.Write("\nPerroquet savant : \"Donne-moi l'addresse d'Elaine moussaillon! \"\n");
            Console.WriteLine("(Exemple d'addresse valide : /home/monrepertoire/addresse/)");
            Console.Write("Addresse : ");
            Console.ResetColor();

        }
        /// <summary>
        /// Animation d'envoi
        /// </summary>
        public static void EnvoieEnCours()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n   ~('>\n    / )\n     4\n");
            Console.WriteLine("Guybrush---------------------Elaine");
            Console.WriteLine("Transport en cours...");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n             ~('>\n              / )\n               4\n");
            Console.WriteLine("Guybrush---------------------Elaine");
            Console.WriteLine("Transport en cours...");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n                     ~('>\n                      / )\n                       4\n");
            Console.WriteLine("Guybrush---------------------Elaine");
            Console.WriteLine("Transport en cours...");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n                              ~('>\n                               / )\n                                4\n");
            Console.WriteLine("Guybrush---------------------Elaine");
            Console.WriteLine("Transport en cours...");
            Thread.Sleep(1000);
            Console.Clear();
        }
        /// <summary>
        /// Simple confirmation à l'utilsateur que l'envoi de la carte est bien effectué 
        /// </summary>
        public static void ConfirmationEnvoi(string addresse)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("            Confirmation de l'envoi ");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n   ~('>\n    / )\n     4\n");
            Console.WriteLine("Perroquet savant : Elaine a bien reçu la trame\nà l'addresse suivante : " + addresse + "\n\n");
            Console.WriteLine("\n\nAppuyez sur entrée pour continuer");
            Console.ResetColor();
        }
        /// <summary>
        /// Choix du nom du chemin ainsi que le chemin de la destination du fichier
        /// </summary>
        public static (string, string, string) DemanderCheminTrame()
        {
            (string, string, string) cheminNomEtDestination;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("==========================================================================================\n");
            Console.Write("Indique le chemin de la trame, son nom ainsi que l'endroit où tu veux sauvegarder la carte\n");
            Console.Write("==========================================================================================\n\n\n\n");
            Console.WriteLine("Exemple de chemin valide : /home/usr/ile.chiffre");
            Console.Write("Chemin absolu : ");
            cheminNomEtDestination.Item1 = Console.ReadLine();
            Console.Write("Nom de l'ile : ");
            cheminNomEtDestination.Item2 = Console.ReadLine();
            Console.WriteLine("Exemple de destination valide : /home/usr/");
            Console.Write("Où veux-tu ranger le fichier.clair ? : ");
            cheminNomEtDestination.Item3 = Console.ReadLine();
            Console.ResetColor();
            Console.Clear();
            return cheminNomEtDestination;
        }
        /// <summary>
        /// Ecran d'attente pendant le calcul du décryptage de la carte.
        /// </summary>
        public static void DecryptageEnCours()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\n\n\n||-------------------------------||\nDecryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||011011010----------------------||\nDecryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||1110101011011101---------------||\nDecryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||0011101011010101010110---------||\nDecryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n\n\n||1101110101110110011111011011010||\nDecryptage de la carte en cours");
            Thread.Sleep(500);
            Console.Clear();
            Console.ResetColor();
        }
        /// <summary>
        /// Affichage de la carte une fois décryptée ainsi que le chemin de sa sauvegarde
        /// </summary>
        public static void AfficherCarteDecryptee(Carte carte, string destination)
        { 
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("              Carte de l'ile : ");
            Console.WriteLine("===========================================\n\n");
            Console.ResetColor();
            carte.AfficherLaCarte();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("La carte à été sauvegardée à l'addresse :");
            Console.WriteLine(destination);
            Console.Write("\n\nAppuyez sur entrée pour continuer...");
        }
        #endregion
    }
}