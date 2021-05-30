using System;
using System.Threading;
namespace LeRhumDeGuy
{
    public static class Affichage
    {
        //https://fr.rakko.tools/tools/68/
        //invita
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

        public static void MenuPrincipalActions()
        {
            Console.WriteLine("a - Charger une carte");
            Console.WriteLine("b - Lire une trame");
            Console.WriteLine("q - Quitter\n\n");
            Console.Write("Alors moussaillon ? : ");
            Console.ResetColor();
        }

        private static void MenuChargerCarte()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("    /)\n _ (/   _   __    _    _  __           __    _\n(__/ )_(_(_/ (_  (_/__(/_/ (_      (_(_/ (__(/_\n                .-/\n               (_/\n");
            Console.Write("\n _  _   __ _/_   _\n(__(_(_/ (_(__ _(/_\n\n\n");
            Console.ResetColor();
        }

        public static (string, string) DemanderCheminCarte()
        {
            (string, string) cheminEtNom;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("==============================================================================\n");
            Console.Write("Indique le chemin absolu de ton fichier .clair et le nom de l'ile moussailon !\n");
            Console.Write("==============================================================================\n\n\n\n");
            Console.Write("Chemin absolu : ");
            cheminEtNom.Item1 = Console.ReadLine();
            Console.Write("Nom de l'ile : ");
            cheminEtNom.Item2 = Console.ReadLine();
            return cheminEtNom;
        }

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

    }
}
