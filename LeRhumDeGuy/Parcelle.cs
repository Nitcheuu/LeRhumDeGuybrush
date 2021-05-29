using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    public class Parcelle
    {
        private char lettre;
        private List<UniteTerre> listeUnite = new List<UniteTerre>();
        private (int, int) coin1;
        private (int, int) coin2;
        private (int, int) dimensionsParcelle;

        public Parcelle(List<UniteTerre> liste, char lettre)
        {
            foreach (UniteTerre unite in liste)
            {
                this.listeUnite.Add(unite);
            }

            this.lettre = lettre;

            this.coin1 = listeUnite[0].RetournerCoordonnes();

            this.coin2 = listeUnite[0].RetournerCoordonnes();

            this.dimensionsParcelle = (0, 0);

            this.DefinirLesCoins();

            this.DefinirLesDimensions();

        }

        private void DefinirLesCoins()
        {
            foreach (UniteTerre unite in this.listeUnite)
            {
                if (unite.RetournerCoordonnes().Item1 < this.coin1.Item1 && unite.RetournerCoordonnes().Item2 < this.coin1.Item2)
                {
                    this.coin1 = unite.RetournerCoordonnes();
                }
                if (unite.RetournerCoordonnes().Item1 >= this.coin1.Item1 && unite.RetournerCoordonnes().Item2 > this.coin1.Item2)
                {
                    this.coin2 = unite.RetournerCoordonnes();
                }
            }
        }

        private void DefinirLesDimensions()
        {
            int i;
            for (i = coin1.Item1; i <= coin2.Item1; i++)
            {
                this.dimensionsParcelle.Item1++;
            }
            for (i = coin1.Item2; i <= coin2.Item2; i++)
            {
                this.dimensionsParcelle.Item2++;
            }
        }

        public void AfficherInformations(string mode)
        {
            if (mode == "Partiel" || mode == "Entier")
            {
                Console.WriteLine("PARCELLE {0} - {1} unites", this.lettre, this.listeUnite.Count);
            }
            if (mode == "Entier")
            {
                foreach (UniteTerre unite in this.listeUnite)
                {
                    Console.Write("{0}  ", unite.RetournerCoordonnes());
                }
                Console.Write("\n");
            }
            if (mode != "Partiel" && mode != "Entier")
            {
                Console.WriteLine("ERREUR : classe Parcelle > AfficherInformations(mode)");
                Console.WriteLine("ERREUR : mode non reconnu");
            }
        }


        public char RetournerLaLettre()
        {
            return this.lettre;
        }

        public int RetournerLaTaille()
        {
            return this.listeUnite.Count;
        }

    }

}

