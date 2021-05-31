using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe UniteTerre : modélise une unité de terre et hérite de la
    /// classe Unite
    /// </summary>
    public class UniteTerre : Unite
    {
        #region Constructeur
        /// <summary>
        /// Seul constructeur de UniteTerre.
        /// </summary>
        /// <param name="x">Coordonnée x</param>
        /// <param name="y">Coordonnée y</param>
        /// <param name="l">lettre</param>
        public UniteTerre(int x, int y, char l) : base (x, y)
        {
            this.lettre = l;
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Ajoute les frontières en fonction d'un tuple de booléen qui
        /// désigne les 4 points cardinaux.
        /// </summary>
        /// <param name="liste">Liste des unités de terre de la carte</param>
        public void AjouterFrontieres(List<UniteTerre> liste)
        {
            //Nord Sud   Est   Ouest
            (bool, bool, bool, bool) voisins = this.VerifierVoisinsTerre(
                                                                      liste);
            if (!voisins.Item1) { this.DefinirFrontieres(1); }
            if (!voisins.Item2) { this.DefinirFrontieres(4); }
            if (!voisins.Item3) { this.DefinirFrontieres(8); }
            if (!voisins.Item4) { this.DefinirFrontieres(2); }
        }

        /// <summary>
        /// Vérifie tous les voisins de l'unité
        /// </summary>
        /// <returns>Les voisins de l'unité</returns>
        /// <param name="liste">Liste des unités de terre de la carte</param>
        private (bool, bool, bool, bool) VerifierVoisinsTerre(
                                                      List<UniteTerre> liste)
        {
            //Nord Sud   Est   Ouest
            (bool, bool, bool, bool) voisins = (false, false, false, false);
            (int, int) coordonnees = this.RetournerCoordonnes();
            foreach (UniteTerre unite in liste)
            {
                (int, int) coordonnees2 = unite.RetournerCoordonnes();
                //Frontière nord
                if (coordonnees.Item1 == coordonnees2.Item1 && (coordonnees.Item2 - 1) == coordonnees2.Item2 && this.RetournerLaLettre() == unite.RetournerLaLettre()) { voisins.Item1 = true; }
                //Frontière sud
                if (coordonnees.Item1 == coordonnees2.Item1 && (coordonnees.Item2 + 1) == coordonnees2.Item2 && this.RetournerLaLettre() == unite.RetournerLaLettre()) { voisins.Item2 = true; }
                //Frontière est
                if ((coordonnees.Item1 + 1) == coordonnees2.Item1 && coordonnees.Item2 == coordonnees2.Item2 && this.RetournerLaLettre() == unite.RetournerLaLettre()) { voisins.Item3 = true; }
                //Frontière ouest
                if ((coordonnees.Item1 - 1) == coordonnees2.Item1 && coordonnees.Item2 == coordonnees2.Item2 && this.RetournerLaLettre() == unite.RetournerLaLettre()) { voisins.Item4 = true; }
            }
            return voisins;
        }
        #endregion
    }
}
