using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    public class Carte
    {
        private string cheminAccees;
        private char[,] carte = new char[10, 10];
        private List<string> carteCrypte = new List<string>();
        private List<UniteTerre> listeUniteTerre = new List<UniteTerre>();
        private List<UniteMer> listeUniteMer = new List<UniteMer>();
        private List<UniteForet> listeUniteForet = new List<UniteForet>();
        private List<Parcelle> listeParcelleTerre = new List<Parcelle>();

        public Carte(string cheminAccees)
        {
            this.cheminAccees = cheminAccees;
            this.LireLaCarte();
            this.CrypterLaCarte();
        }

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
            foreach (char unite in this.carte)
            {
                compteur++;
                this.AfficherLeCaractereEnCouleur(unite);
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

        public void InfoParcelle(char recherche)
        {
            bool estTrouvee = false;
            if (Convert.ToInt32(recherche) >= 97 && Convert.ToInt32(recherche) <= 122)
            {
                foreach (Parcelle parcelle in listeParcelleTerre)
                {
                    if (parcelle.RetournerLaLettre() == recherche)
                    {
                        parcelle.AfficherInformations();
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

        public List<string> RetournerLaTrame()
        {
            return this.carteCrypte;
        }

    }
}
