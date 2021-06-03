using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe Parcelle : Modélise une parcelle
    /// </summary>
    public class Parcelle
    {
        #region Attributs
        /// <summary>
        /// Lettre de la parcelle
        /// </summary>
        private char lettre;
        /// <summary>
        /// Liste des unités de terre qui composent la parcelle
        /// </summary>
        private List<UniteTerre> listeUnite = new List<UniteTerre>();
        //private (int, int) coin1;
        //private (int, int) coin2;
        //private (int, int) dimensionsParcelle;
        #endregion
        #region Constructeur
        /// <summary>
        /// Seul constructeur de la classe
        /// </summary>
        /// <param name="liste">Liste des unités de terre</param>
        /// <param name="lettre">Lettre de la parcelle</param>
        public Parcelle(List<UniteTerre> liste, char lettre)
        {
            foreach (UniteTerre unite in liste)
            {
                this.listeUnite.Add(unite);
            }

            this.lettre = lettre;

        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Affiche les Informations de la parcelle 2 modes possibles :
        /// Partiel : Affiche seulement la lettre et la taille
        /// Entier : Affiche toutes les informations sur la parcelle
        /// </summary>
        /// <param name="mode">Partiel ou Entier</param>
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
        }
        /// <summary>
        /// Retourne la lettre de la parcelle
        /// </summary>
        /// <returns>lettre</returns>
        public char RetournerLaLettre()
        {
            return this.lettre;
        }
        /// <summary>
        /// Retourne la taille de la parcelle (nombre d'unités)
        /// </summary>
        /// <returns>taille</returns>
        public int RetournerLaTaille()
        {
            return this.listeUnite.Count;
        }
        #endregion
    }

}

