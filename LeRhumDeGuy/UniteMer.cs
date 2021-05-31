﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe UniteMer : Modélise une unité de mer et hérite de la classe
    /// Unite
    /// </summary>
    public class UniteMer : Unite
    {
        #region Constructeurs
        /// <summary>
        /// Seul constructeur de la classe
        /// </summary>
        /// <param name="x">Coordonnée x</param>
        /// <param name="y">Coordonnée y</param>
        public UniteMer(int x, int y) : base(x, y)
        {
            // Application des propriétés de la classe
            this.lettre = 'M';
            this.frontiere += 64;
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Ajoute les frontiéres en fonction d'un tuple de boléens qui
        /// représente les 4 points cardinaux
        /// </summary>
        /// <param name="liste">Liste des unités de mer de la carte</param>
        public void AjouterFrontieres(List<UniteMer> liste)
        {
            //Nord Sud   Est   Ouest
            (bool, bool, bool, bool) voisins = this.VerifierVoisinsMer(liste);
            if (!voisins.Item1) { this.DefinirFrontieres(1); }
            if (!voisins.Item2) { this.DefinirFrontieres(4); }
            if (!voisins.Item3) { this.DefinirFrontieres(8); }
            if (!voisins.Item4) { this.DefinirFrontieres(2); }
        }
        /// <summary>
        /// Vérifie tous les voisins de l'unité
        /// </summary>
        /// <returns>Voisins de l'unité</returns>
        /// <param name="liste">Liste des unités de mer de la carte</param>
        private (bool, bool, bool, bool) VerifierVoisinsMer(List<UniteMer> liste)
        {
            //Nord Sud   Est   Ouest
            (bool, bool, bool, bool) voisins = (false, false, false, false);
            (int, int) coordonnees = this.RetournerCoordonnes();
            foreach (UniteMer unite in liste)
            {
                (int, int) coordonnees2 = unite.RetournerCoordonnes();
                //Frontière nord
                if (coordonnees.Item1 == coordonnees2.Item1 && (coordonnees.Item2 - 1) == coordonnees2.Item2) { voisins.Item1 = true; }
                //Frontière sud
                if (coordonnees.Item1 == coordonnees2.Item1 && (coordonnees.Item2 + 1) == coordonnees2.Item2) { voisins.Item2 = true; }
                //Frontière est
                if ((coordonnees.Item1 + 1) == coordonnees2.Item1 && coordonnees.Item2 == coordonnees2.Item2) { voisins.Item3 = true; }
                //Frontière ouest
                if ((coordonnees.Item1 - 1) == coordonnees2.Item1 && coordonnees.Item2 == coordonnees2.Item2) { voisins.Item4 = true; }
            }
            return voisins;
        }
        #endregion
    }
}
