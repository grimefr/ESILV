using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet_BDI
{
    /// <summary>
    /// Logique d'interaction pour Fenetre_test.xaml
    /// </summary>
    public partial class Fenetre_test : Window
    {
        public Fenetre_test()
        {
            InitializeComponent();
            string rep = "";
            rep += "\n\n- Nombre de client : select count(IDclient) from client   :" + Compte.getdata("select count(IDclient) from client;");

            rep += "\n\n- Nombre de CDR :  select count(IDcreateur)from recette    :" + Compte.getdata("select count(IDcreateur)from recette;");
            rep += "\n\n- Noms des CdR et pour chaque CdR : nombre total de ses recettes commandée  :  \n\n select Nom,Prenom,sum(R.Nombreplatcommande) from client C join recette R on C.IDclient = R.IDcreateur group by C.IDclient:\n" + Compte.getdata("select Nom,Prenom,sum(R.Nombreplatcommande) from client C join recette R on C.IDclient = R.IDcreateur group by C.IDclient; ");

            rep += "\n\n- Nombre de recettes  :  select count(NomR) from recette    :" + Compte.getdata("select count(NomR) from recette;");

            rep += "\n\n- Liste des produits ayant une quantité en stock <= 2 * leur quantité minimal  :  \n\nselect IDproduit from produit where stock <= 2* stockmini    :" + Compte.getdata("select IDproduit from produit where stock <= 2* stockmini;");

           
            rep += "\n\n- Saisie au clavier (par l'évaluateur) d'un des produits de la liste précédente puis affichage de la liste des recettes(leur nom) utilisant ce produit et de la quantité utilisée dans cette recette :\n\nselect NomR, quantitep from utilise where IDproduit like 'nom de la recette entré'     : " ;
            preuve.Text = rep;
        }

        private void Valider(object sender, RoutedEventArgs e)
        {
            if (Compte.getdata("select NomR, quantitep from utilise where IDproduit like '" + Compte.Verifcommande(entre.Text) + "';") != null)
                EntreTexte.Text = "\n\n" + Compte.getdata("select NomR, quantitep from utilise where IDproduit like '" + entre.Text + "';");
            else
                EntreTexte.Text = "Veuillez rentrer un aliment présent dans la base de donnée svp.";
        }
    }
}
