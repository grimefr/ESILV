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
using System.Windows.Navigation;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.ObjectModel;
using Renci.SshNet.Messages;

namespace Projet_BDI
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public ObservableCollection<Plat> plats = new ObservableCollection<Plat>();
        public ObservableCollection<Plat> panier = new ObservableCollection<Plat>();
        int prix_total = 0;
        Compte compte_utilise = new Compte();

        public MenuPrincipal(Compte compte)
        {
            string[] platsdispo = Compte.getdata("select NomR from recette;").Split(',');


            compte_utilise = compte;
            for (int i = 0; i < platsdispo.Length; i++)
            {
                if (platsdispo[i] != null && platsdispo[i] != "")
                    plats.Add(new Plat(platsdispo[i].Trim(' ', ',')));
            }
            this.DataContext = this;
            InitializeComponent();
            plats_list.ItemsSource = plats;
            Idconnecte.Text = "Id : " + compte.Identifiant + " | Connection : " + compte.Connecte;
            solde_cook.Text = "" + compte.PointCooking;
            prixdupanier.Text = "0";
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Page_Accueil w10 = new Page_Accueil();
            w10.Show();
            this.Close();

        }
        private void Commander(object sender, RoutedEventArgs e)
        {

            if (liste_panier.Items.Count != 0)
            {
                bool quantiteok = true;
                List<Produit> produit_stock = new List<Produit>();
                foreach (Plat o in liste_panier.Items)
                {
                    if (o != null)
                    {
                        List<Produit> produitaajouter = Produit.ProduitUtiliserDansPlat(o);
                        foreach (Produit pro in produitaajouter)
                        {
                            if (o != null)
                            {
                                produit_stock.Add(pro);
                            }
                        }
                    }
                }
                List<Produit> produit_utilise = Produit.FusionListeProduit(produit_stock);
                for (int i = 0; i < produit_utilise.Count; i++)
                {
                    if (Compte.GetDataInt("select stock from produit where IDproduit like'" + produit_utilise[i].IDproduit + "';") < produit_utilise[i].Stock)
                        quantiteok = false;
                }
                int diffprix = compte_utilise.PointCooking - prix_total;
                if (diffprix >= 0 && quantiteok)
                {
                    for (int i = 0; i < produit_utilise.Count; i++)
                        Compte.lirecommande("update produit set stock='" + (Compte.GetDataInt("select stock from produit where IDproduit like '" + produit_utilise[i].IDproduit + "';") - produit_utilise[i].Stock) + "' where IDproduit like '" + produit_utilise[i].IDproduit + "';");

                    MessageBox.Show("Commande réussie ! \nNous procédons à la livraison de suite !", "Etat de la commande");
                    foreach (Plat o in liste_panier.Items)
                    {
                        if (o != null)
                        {
                            
                            payement(compte_utilise.Identifiant, o.NomR);
                            miseajournbrecette(compte_utilise.Identifiant, o.NomR);
                            o.Renomme = Compte.getdata("select Nombreplatcommande from recette where NomR like '" + o.NomR + "';").Trim(',');
                            o.Prix = Compte.GetDataInt("select Prixplat from recette where NomR like '" + o.NomR + "';");
                            remunerationcdr(o.NomR);
                            if (Compte.getdata("select IDcreateur from recette where NomR like '" + o.NomR + "';").Trim(',') == compte_utilise.Identifiant)
                                compte_utilise.PointCooking = Compte.GetDataInt("select soldeclient from client where IDclient like '"+compte_utilise.Identifiant+"'; ");
                            Compte.lirecommande("INSERT INTO `cooking`.`commande` (`IDcommande`,`Datec`,`Quantitec`,`prixc`,`IDclient`,`NomR`) VALUES ('" + +(Compte.GetDataInt("select count(IDcommande)from commande;") + 1) + "','" + DateTime.Today.ToString().Remove(10) + "','1','" + o.Prix + "','" + compte_utilise.Identifiant + "','" + o.NomR + "');");
                        }

                    }

                    compte_utilise.PointCooking = diffprix;
                    solde_cook.Text = "" + compte_utilise.PointCooking;


                    ViderPanier(sender, e);

                }
                else
                {
                    if (diffprix < 0)
                        MessageBox.Show("Vous ne disposez pas assez de crédit cooking pour effectuer l'achat.");
                    else
                        MessageBox.Show("Rupture de stock d'un ou plusieur aliments compris dans vos plats.\nDésolé pour la gêne occasionné, veuillez recommencer plus tard svp.");

                }
            }
            else
                MessageBox.Show("Votre panier est vide");

        }
        private void ajouter_panier(object sender, RoutedEventArgs e)
        {
            liste_panier.Items.Add(plats_list.SelectedItem);
            string loc = "";
            foreach (object o in plats_list.SelectedItems)
            {
                prix_total += Convert.ToInt32(((Plat)o).Prix);
            }

            prixdupanier.Text = loc = " " + prix_total;
        }
        private void Supprimer_item_panier(object sender, RoutedEventArgs e)
        {

            foreach (object o in liste_panier.SelectedItems)
            {
                prix_total -= Convert.ToInt32(((Plat)o).Prix);
            }

            prixdupanier.Text = "" + prix_total;
            liste_panier.Items.Remove(liste_panier.SelectedItem);
        }
        private void ViderPanier(object sender, RoutedEventArgs e)
        {
            liste_panier.SelectAll();
            prix_total = 0;

            int nb = liste_panier.SelectedItems.Count;
            for (int i = 0; i < nb; i++)
            {
                liste_panier.SelectAll();
                liste_panier.Items.Remove(liste_panier.SelectedItem);
                liste_panier.UnselectAll();
            }


            prixdupanier.Text = "" + prix_total;

        }

        private void RechargerSolde(object sender, RoutedEventArgs e)
        {
            Recharge_Cook w5 = new Recharge_Cook(compte_utilise);
            w5.Show();
            this.Close();

        }

        private void liste_panier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void plats_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreerRecette(object sender, RoutedEventArgs e)
        {
            if (compte_utilise.Connecte)
            {
                FenetreCDR w7 = new FenetreCDR(compte_utilise);
                w7.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Il faut être connecté pour pouvoir creer une recette. \nVeuillez vous connecter pour continuer.\n\nVous pouvez également vous créer un compte si vou n'en possédais pas un.", "Compte client");
                Page_Accueil w2 = new Page_Accueil();
                w2.Show();
                this.Close();
            }
        }



        //COMMANDE


        static void payement(string idclient, string NomR) //id du client et nom de la recette 
        {
            int prixr = Compte.GetDataInt("select Prixplat from recette where NomR like '" + NomR + "';");
            int soldclient = Compte.GetDataInt("select soldeclient from client where IDclient like '" + idclient + "';");
            int diffprix = soldclient - prixr;

            Compte.lirecommande("update client set soldeclient='" + diffprix + "' where IDclient like'" + idclient + "';"); // on change le montant d'argent du client


        }
        static void miseajournbrecette(string id, string NomR) // permet de mettre à jour le nombre de plat commandé par recette et la renum en cook && renum cook
        {
            int nbcommandé = Compte.GetDataInt("select Nombreplatcommande from recette where NomR like '" + NomR + "';");
            nbcommandé += 1; // on augmente de 1 car une commande à été faite 
            Compte.lirecommande("update recette set Nombreplatcommande='" + nbcommandé + "' where NomR like '" + NomR + "';");
            if (nbcommandé == 10)
            {
                int prixr = Compte.GetDataInt("select Prixplat from recette where NomR like '" + NomR + "';");
                prixr += 2; //on augmente le prix de la recette de 2 cook si elle a été commandé plus de 10 fois
                Compte.lirecommande("update recette set Prixplat='" + prixr + "' where NomR like '" + NomR + "';");
            }
            if (nbcommandé == 50)
            {
                int prixr = Compte.GetDataInt("select Prixplat from recette where NomR like '" + NomR + "';");
                prixr += 3; //on augmente le prix de la recette de 3 cook si elle a été commandé plus de 50 fois
                Compte.lirecommande("update recette set Prixplat='" + prixr + "' where NomR like '" + NomR + "';");
                int remuncdr = 4; //on augmente la renum du cdr à 4 cook si elle a été commandé plus de 50 fois
                Compte.lirecommande("update recette set Remunerationcdr='" + remuncdr + "' where NomR like '" + NomR + "';");
            }
        }
        static void remunerationcdr(string NomR) // fonction qui rémunère un cdr lorsque ça recette est commandé 
        {
            string idcdr = Compte.getdata("select IDcreateur from recette where NomR like '" + NomR + "';").Trim(',');

            int montantcdr = Compte.GetDataInt("select Remunerationcdr from recette where NomR like '" + NomR + "';");
            int soldcdr = Compte.GetDataInt("select distinct soldeclient from client natural join recette where recette.IDcreateur=client.IDclient and recette.NomR like '" + NomR + "'; ");

            soldcdr += montantcdr;
            Compte.lirecommande("update client set soldeclient='" + soldcdr + "' where IDclient like '" + idcdr + "';");


        }



        
    }
}
