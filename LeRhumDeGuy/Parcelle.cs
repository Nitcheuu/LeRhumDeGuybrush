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
        private int frontieres;

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

            this.frontieres = 0;

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

        public void AfficherInformations()
        {
            Console.WriteLine("La parcelle {0} possede {1} unites", this.lettre, this.listeUnite.Count);
            Console.WriteLine("Elle est de dimension {0}", this.dimensionsParcelle);
            Console.WriteLine("Son point le plus petit est en : {0}", this.coin1);
            Console.WriteLine("Son point le plus grand est en : {0}", this.coin2);
            Console.WriteLine("Frontieres : {0}", this.frontieres);
            foreach (UniteTerre unite in this.listeUnite)
            {
                unite.afficherCaracteristiques();
            }
        }

        public char RetournerLaLettre()
        {
            return this.lettre;
        }

    }

}

