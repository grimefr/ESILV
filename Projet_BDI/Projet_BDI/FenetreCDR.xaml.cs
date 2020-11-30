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
using System.Collections.ObjectModel;


namespace Projet_BDI
{
    /// <summary>
    /// Logique d'interaction pour FenetreCDR.xaml
    /// </summary>
    public partial class FenetreCDR : Window
    {
        public ObservableCollection<Produit> produits = new ObservableCollection<Produit>();
        public ObservableCollection<Produit> liste_produits_recette = new ObservableCollection<Produit>();
        Compte compte_utilise = new Compte();
        public FenetreCDR(Compte c)
        {

            string[] produitdispo = Compte.getdata("select IDproduit from produit;").Split(',');


            compte_utilise = c;
            for (int i = 0; i < produitdispo.Length; i++)
            {
                if (produitdispo[i] != null && produitdispo[i] != "")
                    produits.Add(new Produit(produitdispo[i].Trim(' ', ',')));
            }
            this.DataContext = this;
            InitializeComponent();
            liste_aliment.ItemsSource = produits;
            Information_client.Text = "Client : " + compte_utilise.Identifiant + " solde cook : " + compte_utilise.PointCooking;


        }

        private void Liste_aliment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreerRecette(object sender, RoutedEventArgs e)
        {
            if (liste_aliment_recette.Items.Count != 0)
            {
                bool rep = true;
                for (int i = 0; i < PrixVente.Text.Length; i++)
                {
                    if (!char.IsDigit(PrixVente.Text[i]))
                        rep = false;
                }
                if (rep && NomRecette.Text != "" && NomRecette.Text != null && PrixVente.Text != "" && PrixVente.Text != null && DescriptionProduit.Text != "" && DescriptionProduit.Text != null&& DescriptionProduit.Text.Length<=256&&Compte.GetDataInt("select count(NomR) from recette where NomR like '"+ Compte.Verifcommande(NomRecette.Text)+"';") ==0)
                {
                    if (!produitexist(Compte.Verifcommande(NomRecette.Text)))
                    {
                        if (Convert.ToInt32(PrixVente.Text) >= 10 && Convert.ToInt32(PrixVente.Text) <= 40)
                        {
                            Compte.lirecommande("INSERT INTO `cooking`.`recette` (`NomR`,`Type`,`Descriptif`,`Prixplat`,`Remunerationcdr`,`Nombreplatcommande`,`IDcreateur`) VALUES('" + Compte.Verifcommande(NomRecette.Text) + "', '" + Compte.Verifcommande(typedeplat.Text) + "', '" + Compte.Verifcommande(DescriptionProduit.Text) + "', " + PrixVente.Text + ", '2', '0', '" + Compte.Verifcommande(compte_utilise.Identifiant) + "');"); //RECETTE CREER

                            foreach (Produit p in liste_aliment_recette.Items)
                            {
                                if (p != null)
                                {
                                    p.Stockmini = (p.Stockmini / 2) + (3 * p.Quantite);
                                    p.Stockmaxi += (2 * p.Quantite);
                                    Compte.lirecommande("INSERT INTO `cooking`.`utilise` (`keyutilise`,`quantitep`,`IDproduit`,`NomR`) VALUES ('" + p.IDproduit + Compte.Verifcommande(NomRecette.Text) + "','" + p.Quantite + "','" + p.IDproduit + "','" + Compte.Verifcommande(NomRecette.Text) + "');");
                                    Compte.lirecommande("update produit set stockmini='" + p.Stockmini + "' where IDproduit like '" + p.IDproduit + "';");
                                    Compte.lirecommande("update produit set stockmaxi='" + p.Stockmaxi + "' where IDproduit like '" + p.IDproduit + "';");
                                }

                            }
                            MessageBox.Show("Félicitation, vous venez de créer votre recette !\n\nResume :Nom de la recette :" + Compte.Verifcommande(NomRecette.Text) + "\nProduit utilise avec quantite : " + Compte.getdata("select IDproduit,quantitep from utilise where NomR like '" + Compte.Verifcommande(NomRecette.Text) + "'; ") + "\nType de produit : " + Compte.Verifcommande(typedeplat.Text) + "\n Prix :" + PrixVente.Text);
                            ViderRecette(sender, e);


                        }
                        else
                            MessageBox.Show("Le prix de vente n'est pas correcte, veuillez recommencer");
                    }
                    else
                        MessageBox.Show("Ce produit existe déjà veuillez changer de nom.");
                }
                else
                    MessageBox.Show("Erreur, veuillez compléter correctement toutes les données demandés svp.");
            }
            else
                MessageBox.Show("Veuillez ajouter des produits à votre recette.");
        }
        private void AjouterRecette(object sender, RoutedEventArgs e)
        {
            if (!liste_aliment_recette.Items.Contains(liste_aliment.SelectedItem))
                liste_aliment_recette.Items.Add(liste_aliment.SelectedItem);
            else
            {
                foreach (object o in liste_aliment.SelectedItems)
                {
                    ((Produit)o).Quantite += 1;
                }
            }


        }
        private void SupprimerRecette_Click(object sender, RoutedEventArgs e)
        {
            bool supr = false;
            foreach (object o in liste_aliment_recette.SelectedItems)
            {
                if (((Produit)o).Quantite == 1)
                    supr = true;
                else
                    ((Produit)o).Quantite -= 1;

            }
            if (supr)
                liste_aliment_recette.Items.Remove(liste_aliment.SelectedItem);



        }
        private void AjouterRecetteFinal_Click(object sender, RoutedEventArgs e)
        {
            foreach (object o in liste_aliment_recette.SelectedItems)
            {
                ((Produit)o).Quantite += 1;
            }
        }
        private void ViderRecette(object sender, RoutedEventArgs e)
        {
            liste_aliment_recette.SelectAll();

            int nb = liste_aliment_recette.SelectedItems.Count;
            for (int i = 0; i < nb; i++)
            {
                if (((Produit)liste_aliment_recette.SelectedItem)!=null)
                ((Produit)liste_aliment_recette.SelectedItem).Quantite = 1;
                liste_aliment_recette.SelectAll();
                liste_aliment_recette.Items.Remove(liste_aliment_recette.SelectedItem);
                liste_aliment_recette.UnselectAll();
            }




        }
        private void RetourMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal w8 = new MenuPrincipal(compte_utilise);
            w8.Show();
            this.Close();
        }
        private void RecetteCrees(object o, RoutedEventArgs e)
        {
            string rep = "Votre solde cook est de : " + Compte.GetDataInt("select soldeclient from client where IDclient like '" + compte_utilise.Identifiant + "';");
            rep += "\nNombre de recette crée : " + Compte.GetDataInt("select count(IDcreateur) from recette where IDcreateur like '" + compte_utilise.Identifiant + "';") + "\n\nVoici la liste des recttes crées ainsi que le nombre de fois où ces dernière ont été commandé :";
            string[] recettes = Compte.getdata("select NomR from recette R, client C where C.IDclient = R.IDcreateur and C.IDclient like '" + compte_utilise.Identifiant + "';").Split(',');

            for (int i = 0; i < Compte.GetDataInt("select count(IDcreateur) from recette where IDcreateur like '" + compte_utilise.Identifiant + "';"); i++)
            {
                rep += "\n- Nom : " + recettes[i] + " commandé : " + Compte.GetDataInt("select Nombreplatcommande from recette where NomR like '" + recettes[i].Trim(',') + "';") + " fois.";
            }
            MessageBox.Show(rep, "Information CDR");
        }


        /// <summary>
        /// vérifie que le produit dont l'id est pris en entré existe ou non dans la bdd, retourne le résultat de sa présence 
        /// </summary>
        /// <param name="produit"></param>
        /// <returns></returns>
        public static bool produitexist(string produit)
        {
            bool presence = false;
            string mot = "select IDproduit from produit where IDproduit like '" + produit + "';";
            string prescemot = Compte.getdata(mot);
            produit = produit + ", ";
            if (prescemot == null) { presence = false; }
            if (prescemot == produit) { presence = true; }
            return presence;
        }


    }
}
