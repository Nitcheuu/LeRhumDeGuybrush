using System;

namespace LeRhumDeGuy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Carte carte = new Carte("/home/nitcheuu/Bureau/scabb.clair");
            carte.AfficherLaCarte();
            carte.AfficherLesStats();
            carte.AfficherLaCarteCrypte();
            carte.InfoParcelle('i');
            Decrypteur.DecrypterLaTrame(carte.RetournerLaTrame());
        }
    }
}
