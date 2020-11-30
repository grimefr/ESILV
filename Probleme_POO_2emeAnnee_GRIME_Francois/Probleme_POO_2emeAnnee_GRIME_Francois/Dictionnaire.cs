using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_POO_2emeAnnee_GRIME_Francois
{
    public class Dictionnaire
    {
        string[,] dictionnaire;
        public string[,] GetDictionnaire
        {
            get { return this.dictionnaire; }
        }
        public Dictionnaire(string[,] dico)
        {
            this.dictionnaire = dico;
        }

        public override string ToString()
        {
            int compteur = 0;
            string mots = " ";

            for (int i = 0; i < this.dictionnaire.GetLength(0); i++)
            {
                for (int j = 0; j < dictionnaire.GetLength(1); j++)
                {
                    if (dictionnaire[i, j] != null)
                    {
                        mots += dictionnaire[i, j] + " ";
                        compteur++;
                    }
                }
                mots += "\n";
            }
            return "Le dictionnaire est composé de  " + compteur + " mots, les voici :\n\n" + mots;
        }
        /// <summary>
        /// La fonction teste si un mot appartient au dictionnaire.
        /// </summary>
        /// <param name="debut">index du début du tableau de mot à tester</param>
        /// <param name="fin">index de la fin du tableau de mot à tester</param>
        /// <param name="mot">mot dont on cherche à savoir s'il appartient au dictionnaire.</param>
        /// <returns></returns>
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {

            int milieu = (debut + fin) / 2;
            if (debut > fin || mot.Length < 2 || mot.Length > 15) return false;
            else
            {
                if (string.Compare(mot, this.dictionnaire[mot.Length - 2, milieu]) == 0 || string.Compare(mot, this.dictionnaire[mot.Length - 2, debut]) == 0 || string.Compare(mot, this.dictionnaire[mot.Length - 2, fin]) == 0)
                {
                    return true;
                }
                else
                {
                    if (string.Compare(this.dictionnaire[mot.Length - 2, milieu], mot) < 0 && this.dictionnaire[mot.Length - 2, milieu] != " " && this.dictionnaire[mot.Length - 2, milieu] != null)
                    {
                        return RechDichoRecursif(milieu + 1, fin-1, mot);
                    }
                    else
                    {
                        return RechDichoRecursif(debut+1, milieu - 1, mot);
                    }
                }
            }


        }



    }
}
