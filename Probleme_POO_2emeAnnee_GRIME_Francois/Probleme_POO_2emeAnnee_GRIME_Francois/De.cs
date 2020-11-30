using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_POO_2emeAnnee_GRIME_Francois
{
    public class De
    {
        char lettre;
        public char Lettre
        {
            get { return this.lettre; }
            //set { this.lettre = value; }
        }

        char[] listelettres;
        public char[] ListelettresDe
        {
            get { return this.listelettres; }
            set { this.listelettres = value; }
        }

        public De(char[] enssembleDeLettre)
        {
            this.listelettres = enssembleDeLettre;
            this.lettre = this.listelettres[0];
        }

        /// <summary>
        /// La fonction sert à modifier la face du dé qui sera vu par le joueur
        /// </summary>
        /// <param name="r">repésente un nombre aléatoire afin de prendre une face du dé aléatoirement</param>
        public void Lance(Random r)
        {
            int i = r.Next(0, 5);
            this.lettre = this.listelettres[i];

        }

        public string toString()
        {
            string ListeLettres = "Le dé est composé des lettres suivantes : \n\n";
            for (int i = 0; i <= 5; i++)
            {
                ListeLettres += listelettres[i] + " ";
            }
            ListeLettres += "\n\n De plus la lettre qui est sur la face supérieure est " + this.lettre;
            return ListeLettres;
        }


    }
}




