using System;
using System.Threading;
namespace LeRhumDeGuy
{
    public static class Menu
    {
        public static void Chargement()
        {
            Affichage.Chargement();
            Thread.Sleep(3000);
            Console.Clear();
        }

        public static void MenuPrinciapl()
        {
            bool reponseOK = false;
            char reponse = '*';
            while (!reponseOK)
            {
                Affichage.MenuPrincipal();
                Affichage.MenuPrincipalActions();
                reponse = Convert.ToChar(Console.ReadLine());
                reponseOK = Menu.VerifMenuPrincipal(reponse);
                Console.Clear();
            }
            if(reponse == 'a')
            {
                Menu.ChargerUneCarte();
            }
        }

        private static bool VerifMenuPrincipal(char reponse)
        {
            bool reponseOK;
            if (reponse == 'a' || reponse == 'b' || reponse == 'q')
            {
                reponseOK = true;
            }
            else
            {
                reponseOK = false;
            }
            return reponseOK;

        }

        private static void ChargerUneCarte()
        {
            string reponse = "";
            (string, string) cheminEtNom;
            cheminEtNom = Affichage.DemanderCheminCarte();
            Carte carte = new Carte(cheminEtNom.Item1, cheminEtNom.Item2);
            Console.Clear();
            Menu.MenuCarte(ref reponse, carte);

        }

        private static void MenuCarte(ref string reponse, Carte carte)
        {
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

        private static bool VerifChargerUneCarte(string reponse)
        {
            bool reponseOK = false;
            if(reponse == "a" || reponse == "b" || reponse == "c" || reponse == "d" || reponse == "e" || reponse == "q")
            {
                reponseOK = true;
            }
            else
            {
                reponseOK = false;
            }
            return reponseOK;
        }

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
                Menu.MenuCarte(ref reponse, carte);
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
                Menu.MenuCarte(ref reponse, carte);
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
                Menu.MenuCarte(ref reponse, carte);
            }
            if (reponse == "d")
            {
                while (sortie != "")
                {
                    Affichage.AireMoyen(carte);
                    sortie = Console.ReadLine();
                    Console.Clear();
                }
                Menu.MenuCarte(ref reponse, carte);
            }
            if (reponse == "e")
            {
                Affichage.CryptageEnCours();
                while (sortie != "o" || sortie != "n")
                {
                    Affichage.AfficherTrame(carte);
                    sortie = Console.ReadLine();
                    Console.Clear();
                    if (sortie == "n")
                    {
                        Menu.MenuCarte(ref reponse, carte);
                    }
                    if(sortie == "o")
                    {
                        string addresse;
                        Affichage.ChoixAddresse();
                        addresse = Console.ReadLine();
                        Affichage.EnvoieEnCours();
                        carte.SauvegaderLaCarteCrypte(addresse);
                        while (reponse != "")
                        {
                            Affichage.ConfirmationEnvoi(addresse);
                            reponse = Console.ReadLine();
                            Console.Clear();
                        }
                        Menu.MenuCarte(ref reponse, carte);
                        
                    }

                }
                Console.Clear();
            }
            if(reponse == "q")
            {
                Menu.MenuPrinciapl();
            }
        }

    }
}
