using System;

namespace LeRhumDeGuy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Carte carte = new Carte("/home/nitcheuu/IUT/Objet/Conception/Projet/scabb.clair");
            carte.AfficherLaCarte();
            carte.AfficherLesStats();
            carte.AfficherLaCarteCrypte();
            carte.AfficherParcelles();
            carte.InfoParcelleRecherche('a');
            carte.TailleParcelleRecherche(4);
            carte.AireMoyenneParcelles();
            Decrypteur.DecrypterLaTrame(carte.RetournerLaTrame());
        }
    }
}