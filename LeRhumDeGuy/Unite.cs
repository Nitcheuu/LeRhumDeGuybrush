using System;
namespace LeRhumDeGuy
{
    /// <summary>
    /// Classe abstraitre Unite : modélise toutes les unités.
    /// </summary>
    abstract public class Unite
    {
        #region Attributs
        /// <summary>
        /// Coordonnées de l'unité (x, y).
        /// </summary>
        protected (int, int) coordonnees;
        /// <summary>
        /// Lettre de l'Unité
        /// </summary>
        protected char lettre;
        /// <summary>
        /// Valeur des frontières.
        /// </summary>
        protected int frontiere;
        #endregion
        #region Constructeur
        /// <summary>
        /// Seul constructeur de Unite
        /// </summary>
        /// <param name="x">Coordonnée x</param>
        /// <param name="y">Coordonnée y</param>
        public Unite(int x, int y)
        {
            this.coordonnees.Item1 = x;
            this.coordonnees.Item2 = y;
            this.frontiere = 0;
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Définir la valeur de l'attribut frontière. On ne peut pas le
        /// faire lors de l'instaciation.
        /// </summary>
        /// <param name="nombreFrontiere">Valeur des frontières</param>
        public void DefinirFrontieres(int nombreFrontiere)
        {
            this.frontiere += nombreFrontiere;
        }

        /*virtual public void afficherCaracteristiques()
        {
            Console.WriteLine("Unite de coos x : {0} - y : {1}", coordonnees.Item1, coordonnees.Item2);
            Console.WriteLine("Lettre : {0}", this.lettre);
            Console.WriteLine("Frontieres : {0}", this.frontiere);
        }*/

        /// <summary>
        /// Retourne l'attribut lettre
        /// </summary>
        /// <returns>lettre</returns>
        public char RetournerLaLettre()
        {
            return this.lettre;
        }
        /// <summary>
        /// Retourne l'attribut frontière
        /// </summary>
        /// <returns>frontière</returns>
        public int RetournerValeurFrontiers()
        {
            return this.frontiere;
        }
        /// <summary>
        /// Retourne l'attribut coordonnées
        /// </summary>
        /// <returns>coordonnées</returns>
        public (int, int) RetournerCoordonnes()
        {
            return this.coordonnees;
        }
        #endregion
    }
}
