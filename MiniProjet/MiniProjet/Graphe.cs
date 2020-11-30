using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace MiniProjet
{
    class Graphe
    {

        int[,] graphe;
        int taille;
        string[] couleurSommet;
        int[,] liaison;
        public Graphe()
        {
        }
        public Graphe(int taille)
        {
            Random alea = new Random();
            this.taille = taille;
            int[,] graphe = new int[taille, taille];
            int index = 0;
            int[,] liaison = new int[taille, taille];
            for (int i = 0; i < taille; i++)
            {
                graphe[i, i] = 0;
                for (int j = 0; j < i; j++)
                {
                    graphe[i, j] = alea.Next(2);
                    graphe[j, i] = graphe[i, j];
                    if (graphe[i, j] == 1)
                    {
                        liaison[i, index] = i;
                        index++;
                    }
                }
                index = 0;
            }
            this.graphe = graphe;
            this.couleurSommet = new string[taille];
            this.liaison = liaison;
        }

        public int Taille { set { this.taille = value; } get { return this.taille; } }


        public override string ToString()
        {
            string rep = " \\  ";
            for (int i = 0; i < taille; i++)
            {
                rep += " " + (i + 1);
            }
            rep += "\n     ";
            for (int i = 0; i < taille; i++)
                rep += "__";
            rep += "\n";

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (j == 0)
                        rep += (i + 1) + " | ";
                    rep += " " + graphe[i, j];

                }
                rep += "\n";
            }

            if (couleurSommet != null)
            {
                for (int i = 0; i < taille; i++)
                {
                    Console.WriteLine((i + 1) + " est de la couleur : " + couleurSommet[i]);
                }
            }
            return rep;
        }

        //public int IndexMaxLiaison()
        //{
        //    int max = 0;
        //    int index = 1;
        //    int somme = 0;
        //    for (int i = 0; i < taille; i++)
        //    {
        //        for (int j = 0; j < taille; j++)
        //        {
        //            if (graphe[j, i] == 1)
        //                somme++;

        //        }
        //        if (max < somme)
        //        {
        //            max = somme;
        //            index = i;
        //        }
        //        somme = 0;
        //    }
        //    return index;
        //}
        public void Colorisation_2()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (graphe[i, j] == 1 && couleurSommet[i] == null)
                    {
                        if (!CouleurAppartient("bleu", j) && couleurSommet[i] == null)
                            couleurSommet[i] = "bleu";
                        if (!CouleurAppartient("rouge", j) && couleurSommet[i] == null)
                            couleurSommet[i] = "rouge";
                    }
                }
                if (couleurSommet[i] == null&&liaison[i,0]==0)
                    couleurSommet[i] = "bleu";


            }
        }
        public void Colorisation_3()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (graphe[i, j] == 1 && couleurSommet[i] == null)
                    {
                        if (!CouleurAppartient("bleu", j) && couleurSommet[i] == null)
                            couleurSommet[i] = "bleu";
                        if (!CouleurAppartient("rouge", j) && couleurSommet[i] == null)
                            couleurSommet[i] = "rouge";
                        if (!CouleurAppartient("vert", j) && couleurSommet[i] == null)
                            couleurSommet[i] = "vert";
                    }
                }
                if (couleurSommet[i] == null)
                    couleurSommet[i] = "rouge";
            }
        }

        public bool CouleurAppartient(string couleur, int index)
        {
            bool rep = false;

            for (int i = 0; i < taille; i++)
            {
                if (couleurSommet[liaison[i, index]] == couleur)
                    rep = true;
            }
            return rep;
        }

    }
}
