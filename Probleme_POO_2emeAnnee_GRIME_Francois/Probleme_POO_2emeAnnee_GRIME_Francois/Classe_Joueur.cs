using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_POO_2emeAnnee_GRIME_Francois
{
    public class ClasseJoueur
    {
        string nom;
        public string GetNom
        {
            get { return this.nom; }
        }

        int score;
        public int GetScore
        {
            get { return this.score; }
        }

        string[] motsTrouves;
        public string[] GetmotsTrouves
        {
            get { return this.motsTrouves; }
        }

        /// <summary>
        /// Constructeur de la classe joueur, on commence avec un score de 0 et un nombre de mot trouvé maximum à 30, en faire plus est quasiment impossible.
        /// </summary>
        /// <param name="nom1">Nom du joueur</param>
        public ClasseJoueur(string nom1)
        {
            this.nom = nom1;
            this.score = 0;
            this.motsTrouves = new string[30];
        }

        /// <summary>
        /// Fonction qui teste si un mot a déjà était trouvé.
        /// </summary>
        /// <param name="mot">Mot à tester</param>
        /// <returns>la valeur du test (V/F)</returns>
        public bool Contain(string mot)
        {
            bool rep = false;
            for (int i = 0; i < this.motsTrouves.Length; i++)
            {
                if (mot == motsTrouves[i]) { rep = true; }
            }
            return rep;
        }
        /// <summary>
        /// Ajoute un mot dans la liste de mots déjà trouvé.
        /// </summary>
        /// <param name="mot">Mot à ajouter</param>
        public void Add_Mot(string mot)
        {
            int i = 0;
            while (motsTrouves[i] != null)
            {
                i++;
            }
            motsTrouves[i] = mot;
            if (mot.Length == 3) { this.score += 2; }
            if (mot.Length == 4) { this.score += 3; }
            if (mot.Length == 5) { this.score += 4; }
            if (mot.Length == 6) { this.score += 5; }
            if (mot.Length >= 7) { this.score += 11; }
        }
        /// <summary>
        /// Fonction qui sert à décrire l'état de la classe.
        /// </summary>
        /// <returns>une chaîne de mot décrivant la classe ClasseJoueur</returns>
        public string toString()
        {
            string chaineDeMots = "";
            for (int i = 0; i < this.motsTrouves.Length; i++)
            {
                chaineDeMots += motsTrouves[i] + " ";
            }
            return "Le joueur " + nom + " a un  score de " + score + " et la liste des mots déjà trouvée sont : " + chaineDeMots;
        }



    }
}
