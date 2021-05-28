using System;
namespace LeRhumDeGuy
{
    abstract public class Unite
    {

        protected (int, int) coordonnees;

        protected char lettre;

        protected int frontiere;

        public Unite(int x, int y)
        {

            this.coordonnees.Item1 = x;

            this.coordonnees.Item2 = y;

            this.frontiere = 0;
            
        }

        public (int, int) RetournerCoordonnes()
        {
            return this.coordonnees;
        }

        public void DefinirFrontieres(int nombreFrontiere)
        {
            this.frontiere += nombreFrontiere;
        }

        virtual public void afficherCaracteristiques()
        {
            Console.WriteLine("Unite de coos x : {0} - y : {1}", coordonnees.Item1, coordonnees.Item2);
            Console.WriteLine("Lettre : {0}", this.lettre);
            Console.WriteLine("Frontieres : {0}", this.frontiere);
        }

        public char RetournerLaLettre()
        {
            return this.lettre;
        }

        public int RetournerValeurFrontiers()
        {
            return this.frontiere;
        }

    }
}
