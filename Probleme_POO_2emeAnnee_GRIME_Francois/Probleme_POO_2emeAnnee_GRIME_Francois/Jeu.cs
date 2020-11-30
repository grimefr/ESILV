using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_POO_2emeAnnee_GRIME_Francois
{
    public class Jeu
    {
        Plateau plateau;
        public Plateau GetPlateau
        {
            get { return this.plateau; }
        }

        Dictionnaire dictionnaire;
        public Dictionnaire GetDictionnaire
        {
            get { return this.dictionnaire; }
        }

        /// <summary>
        /// Constructeur de la classe jeu, on initialise le plateau à l'aide du fichier externe "Des.txt" et le dictionnaire avec le fichier texte "MotsPossible.txt". 
        /// </summary>
        public Jeu()
        {
            string[] DesFichier = File.ReadAllLines("Des.txt");
            char[] lettreSurDes = new char[6];

            //initialisation des dés

            De[] listeDe = new De[16];

            for (int i = 0; i < 16; i++)
            {
                listeDe[i] = new De(new char[6]);
            }



            int compteurColonne = 0;
            Random r = new Random();
            //puis on remplie les faces supérieures des dés grâce au tableau de string issu de "Des.txt".
            for (int i = 0; i < 16; i++)
            {
                compteurColonne = 0;
                for (int j = 0; j < DesFichier[i].Length; j++)
                {
                    if (Char.IsLetter(DesFichier[i][j]))
                    {
                        listeDe[i].ListelettresDe[compteurColonne] = DesFichier[i][j];
                        compteurColonne++;
                    }
                }
                listeDe[i].Lance(r);
            }

            this.plateau = new Plateau(listeDe);                     
            //Le plateau est construit, on passe à la création du dictionnaire.


            string[] MotsPossiblesFichier = File.ReadAllLines("MotsPossibles.txt");

            //on cherche le maximum en colonne possible du tableau, afin de ne pas dépasser d'index lors de la boucle qui suit..

            int max = 0;
            for (int i = 0; i < 14; i++)
            {
                if (max < MotsPossiblesFichier[i].Length)
                    max = MotsPossiblesFichier[i].Length;
            }

            //on crée ensuite notre tableau de string qui servira à créer la classe dico.
            string[,] MotDico = new string[15, 20576]; 
            //afin de trouver la valeur 207576 j'avais mis un compteur afin de savoir combien il y avait de mot au maximum.
            int compteurLigne = 0;
            string mot = "";
            for (int i = 0; i < 28; i++)
            {
                mot = "";
                compteurColonne = 0;

                for (int j = 0; j < MotsPossiblesFichier[i].Length; j++)
                {
                    if (Char.IsDigit(MotsPossiblesFichier[i][j]))
                        i++;

                    if (Char.IsLetter(MotsPossiblesFichier[i][j]))
                        mot += MotsPossiblesFichier[i][j];

                    if (MotsPossiblesFichier[i][j] == ' ')
                    {
                        MotDico[compteurLigne, compteurColonne] = mot;
                        compteurColonne++;
                        mot = "";
                    }
                }
                compteurLigne++;
                //console.WriteLine(compteurColonne);        test effectué auparavant
            }
            this.dictionnaire = new Dictionnaire(MotDico);
        }

        /// <summary>
        /// Fonction qui sert à lancer les dés du plateau, on obtient donc un nouveau tableau de lettres.
        /// </summary>
        public void RafraichissementPlateau()
        {

            Random r = new Random();
            for (int i = 0; i < 16; i++)
            {
                this.plateau.GetDePlateau[i].Lance(r);
                this.plateau.FaceSuperieur[i] = this.plateau.GetDePlateau[i].Lettre;
            }



        }
    }
}
