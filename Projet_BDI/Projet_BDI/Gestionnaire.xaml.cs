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
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

using System.Diagnostics;



namespace Projet_BDI
{

    public partial class Gestionnaire : Window
    {
        public ObservableCollection<Plat> TopRecetteSemaine = new ObservableCollection<Plat>();
        public ObservableCollection<Plat> TopRecetteCDROR = new ObservableCollection<Plat>();
        public ObservableCollection<Produit> ProduitReapro = new ObservableCollection<Produit>();
        public ObservableCollection<string> RecapCommande = new ObservableCollection<string>();

        public Gestionnaire()
        {
            List<Plat> platCdrOr = Plat.TriePlat(TopRecetteCdrOr());
            List<Plat> topplatsemaine = TopPlatSemaine();
            List<Produit> produitinssuf = Produit.ProduitStockInsuffiasant();
            List<string> listecommande = listecommandefournisseur();

            for (int i = 0; i < 5 && i < topplatsemaine.Count; i++)
            {
                if (topplatsemaine[i] != null)
                {
                    TopRecetteSemaine.Add(topplatsemaine[i]);
                }
            }
            for (int i = 0; i < 5 && i < platCdrOr.Count; i++)
            {
                if (platCdrOr[i] != null)
                {
                    TopRecetteCDROR.Add(platCdrOr[i]);
                }
            }
            for (int i = 0; i < produitinssuf.Count; i++)
            {
                if (produitinssuf[i] != null)
                {
                    ProduitReapro.Add(produitinssuf[i]);
                }
            }
            for (int i = 0; i < listecommande.Count; i++)
            {
                if (listecommande[i] != null)
                    RecapCommande.Add(listecommande[i]);

            }

            this.DataContext = this;
            InitializeComponent();

            CDRor.Text = "CDR d'or : " + TopCDR();
            CDRsemaine.Text = "CDR de la semaine : " + TopCDRSemaine();
            ListeProduitRapro.ItemsSource = ProduitReapro;
            recette_list.ItemsSource = TopRecetteSemaine;
            ListRecetteCDROr.ItemsSource = TopRecetteCDROR;

            ListeCommande.ItemsSource = RecapCommande;

        }

        private void Reapprovisionnement_Click(object sender, RoutedEventArgs e)
        {
            string nomfichier = "Commande_" + DateTime.Today.ToString().Remove(10).Replace('/', '.') + ".xml";
            for (int i = 0; i < ProduitReapro.Count; i++)
            {
                if (ProduitReapro[i] != null)
                {
                    int quantiteACommander = ProduitReapro[i].Stockmaxi - ProduitReapro[i].Stock;

                    Compte.lirecommande("update produit set stock='" + ProduitReapro[i].Stockmaxi + "' where IDproduit like '" + ProduitReapro[i].IDproduit + "';");
                    ProduitReapro[i].Stock = ProduitReapro[i].Stockmaxi;
                    Compte.lirecommande("INSERT INTO `cooking`.`fournis` (`numerocommande`,`Reffournisseur`,`IDproduit`,`quantitefournis`,`DateF`) VALUES ('" + (Compte.GetDataInt("select count(numerocommande) from fournis;") + 1) + "','" + ProduitReapro[i].Reffournisseur + "','" + ProduitReapro[i].IDproduit + "','" + quantiteACommander + "','" + DateTime.Today.ToString().Remove(10) + "');");


                    MessageBox.Show("reaprovisionement de " + ProduitReapro[i].Nomproduit + " effectue !");
                    ProduitReapro.Remove(ProduitReapro[i]);
                }
            }

            commandefournisseur("select * from fournis where DateF like '" + DateTime.Today.ToString().Remove(10) + "';", nomfichier);
            RecapCommande.Add(nomfichier);
            ListeProduitRapro.ItemsSource = ProduitReapro;
            int temp = 0;
            for (int i = 0; i < RecapCommande.Count; i++)
            {
                if (nomfichier == RecapCommande[i])
                {
                    temp++;
                    if (temp > 1)
                        RecapCommande.Remove(RecapCommande[i]);

                }
            }
            ListeCommande.ItemsSource = RecapCommande;
        }

        private void SupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            string[] recetteClient = Compte.getdata("select NomR from recette where IDcreateur like'" + Compte.Verifcommande(ClientSuppr.Text) + "';").Split(',');
            if (recetteClient != null)
            {
                for (int i = 0; i < recetteClient.Length; i++)
                {
                    string[] produitRecette = Compte.getdata("select keyutilise from utilise where NomR like'" + recetteClient[i].Trim(',', ' ') + "';").Split(',');

                    if (produitRecette != null)
                    {
                        for (int j = 0; j < produitRecette.Length; j++)
                        {

                            if (produitRecette[j] != null && produitRecette[j] != "")
                                Compte.lirecommande("delete from utilise where keyutilise like '" + produitRecette[j].Trim(',', ' ') + "';");
                        }
                        Compte.lirecommande("delete from recette where NomR like '" + recetteClient[i] + "';");
                    }
                }
            }
            Compte.lirecommande("delete from client where IDclient like '" + Compte.Verifcommande(ClientSuppr.Text) + "';");
            MessageBox.Show("Le compte a bien été supprimé ainsi ces recettes associé.");


        }
        private void SupprimerRecette_Click(object sender, RoutedEventArgs e)
        {

            string[] produitRecette = Compte.getdata("select keyutilise from utilise where NomR like'" + Compte.Verifcommande(RecetteSuppr.Text) + "';").Split(',');

            if (produitRecette != null)
            {
                for (int j = 0; j < produitRecette.Length; j++)
                {

                    if (produitRecette[j] != null && produitRecette[j] != "")
                        Compte.lirecommande("delete from utilise where keyutilise like '" + produitRecette[j].Trim(',', ' ') + "';");
                }
                Compte.lirecommande("delete from recette where NomR like '" + Compte.Verifcommande(RecetteSuppr.Text) + "';");
                MessageBox.Show("La recette a bien été supprimé.");
            }
        }


        private void OuvrirFichier_Click(object sender, RoutedEventArgs e)
        {
            if (ListeCommande.SelectedItem != null)
                Process.Start("C:\\Users\\François\\source\\repos\\Projet_BDI\\Projet_BDI\\bin\\Debug\\" + ListeCommande.SelectedItem.ToString());

        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Page_Accueil w10 = new Page_Accueil();
            w10.Show();
            this.Close();

        }
        private void MiseAjourStock_Click(object sender, RoutedEventArgs e)
        {
            List<Commande> listcommande = Commande.commandeSurDuree(30);
            List<Plat> listplat = Commande.ListPlatDansListCommande(listcommande);
            List<Produit> listproduit = Produit.ProduitUtiliserDansPlat(listplat);
            string[] toutproduit = Compte.getdata("select distinct nomproduit from produit;").Split(',');
            for (int i = 0; i < toutproduit.Length; i++)
            {
                if (toutproduit[i] != null && toutproduit[i] != "")
                {
                    Produit temp = new Produit(toutproduit[i]);
                    if (!temp.Contenue(listproduit))
                    {
                        Compte.lirecommande("update produit set stockmini='" + temp.Stockmini / 2 + "' where  IDproduit like'" + temp.IDproduit + "';");
                        Compte.lirecommande("update produit set stockmaxi='" + temp.Stockmaxi / 2 + "' where  IDproduit like'" + temp.IDproduit + "';");
                    }
                }
            }
            MessageBox.Show("Les stocks max et min des produits non utilisé ont été divisé par 2.");
        }

        private void Liste_aliment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListeProduitRapro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListeCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListRecetteSemaine_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        // fonction utile

        /// <summary>
        /// fonction qui pemrmet d'eectuer la commande donné en entré et également d'enregistrer le flux de donnée en xml sous le fichier donné un entré
        /// </summary>
        /// <param name="commande"></param>
        /// <param name="nomfichier"></param>
        public static void commandefournisseur(string commande, string nomfichier)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=azerty91;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commande;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            DataTable datatab = new DataTable(nomfichier);
            datatab.Load(reader);
            datatab.WriteXml(nomfichier);
            connection.Close();

        }
        /// <summary>
        /// retourne la liste des top plats de la semaine 
        /// </summary>
        /// <returns></returns>
        public List<Plat> TopPlatSemaine()
        {
            List<Commande> commande_semaine = Commande.commandeSurDuree(7);
            List<Plat> platCommandeSemaine = Commande.ListPlatDansListCommande(commande_semaine);
            platCommandeSemaine = Plat.TriePlat(platCommandeSemaine);
            return platCommandeSemaine;
        }

        /// <summary>
        /// retourne la list des tpo recette du cdr or
        /// </summary>
        /// <returns></returns>
        public List<Plat> TopRecetteCdrOr()
        {
            string idcreateur = TopCDR();
            string[] plats = Compte.getdata("select NomR from recette where IDcreateur like '" + idcreateur + "';").Split(',');
            List<Plat> listplats = new List<Plat>();
            for (int i = 0; i < plats.Length; i++)
            {
                if (plats[i] != null && plats[i] != "")
                    listplats.Add(new Plat(plats[i]));
            }
            return listplats;
        }

        /// <summary>
        /// retourne l'id du cdr d'or
        /// </summary>
        /// <returns></returns>
        public string TopCDR()
        {
            string[] ToutCDR = Compte.getdata("select * from client;").Split(',');
            string[] topcdr = new string[2] { ToutCDR[0].Trim(','), "" + 0 };
            int commandeMax = 0;
            for (int i = 0; i < ToutCDR.Length; i++)
            {
                if (ToutCDR[i] != null)
                {
                    commandeMax = 0;
                    string[] ToutRecetteCDR = Compte.getdata("select NomR from recette where IDcreateur like '" + ToutCDR[i].Trim(',') + "';").Split(',');
                    for (int j = 0; j < ToutRecetteCDR.Length; j++)
                    {
                        if (ToutRecetteCDR[j] != null && ToutRecetteCDR[j] != "")
                            commandeMax += Compte.GetDataInt("select Nombreplatcommande from recette where NomR like '" + ToutRecetteCDR[j].Trim(',') + "';");
                    }
                    if (Convert.ToInt32(topcdr[1]) < commandeMax)
                    {
                        topcdr[0] = ToutCDR[i];
                        topcdr[1] = "" + commandeMax;
                    }
                }
            }
            return topcdr[0];
        }
        /// <summary>
        /// retourne l'id du cdr de la semaine
        /// </summary>
        /// <returns></returns>
        public string TopCDRSemaine()
        {
            List<Commande> commande_semaine = Commande.commandeSurDuree(7);
            List<Plat> platCommandeSemaine = Commande.ListPlatDansListCommande(commande_semaine);
            Plat temp = new Plat();
            for (int i = 0; i < platCommandeSemaine.Count; i++)
            {
                if (i == 0 && platCommandeSemaine[i] != null)
                    temp = platCommandeSemaine[i];
                if (platCommandeSemaine[i].Quantite > temp.Quantite)
                {
                    temp.NomR = platCommandeSemaine[i].NomR;
                    temp.Quantite = platCommandeSemaine[i].Quantite;
                }
            }
            return Compte.getdata("select IDcreateur from recette where NomR like'" + temp.NomR + "';").Trim(',');
        }

        /// <summary>
        /// retourne la liste des nom de fichier.xml des commandes passé auprès des fournisseurs
        /// </summary>
        /// <returns></returns>
        public List<string> listecommandefournisseur()
        {
            List<string> rep = new List<string>();

            string[] datecommande = Compte.getdata("select distinct DateF from fournis;").Split(',');
            for (int i = 0; i < datecommande.Length; i++)
            {
                if (datecommande[i] != "" && datecommande[i] != "25/02/1999")
                    rep.Add("Commande_" + datecommande[i].Trim(',').Replace('/', '.') + ".xml");
            }


            return rep;
        }
    }
}
